using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Chess.Model;
using ChessColor = Chess.Model.Color;

namespace Chess
{
    public partial class Form1 : Form
    {
        private ChessGame Game { get; set; }
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            GameBegins();
        }

        private void GameBegins()
        {
            Game = new ChessGame();
            InitBoard();
            DrawFigures();
            PrepareFigures();
        }

        private void InitBoard()
        {
            dgvBoard.RowCount = 8;
            dgvBoard.ColumnCount = 8;
            dgvBoard.RowHeadersWidth = 70;
            dgvBoard.ColumnHeadersHeight = 70;
            Font letterFont = new Font("Arial", 24, FontStyle.Bold);
            Font chessFont = new Font("Arial", 36, FontStyle.Bold);

            for (int i = 0; i < dgvBoard.RowCount; i++)
            {
                dgvBoard.Rows[i].Height = 70;
                dgvBoard.Rows[i].HeaderCell.Style.Font = letterFont;
                dgvBoard.Rows[i].HeaderCell.Value = (8 - i).ToString();
            }
            for (int j = 0; j < dgvBoard.ColumnCount; j++)
            {
                dgvBoard.Columns[j].Width = 70;
                dgvBoard.Columns[j].HeaderCell.Style.Font = letterFont;
                dgvBoard.Columns[j].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                int aCharNumber = (int) 'A';
                dgvBoard.Columns[j].HeaderCell.Value = ((char)(aCharNumber + j)).ToString();
            }
            for (int i = 0; i < dgvBoard.RowCount; i++)
            {
                for (int j = 0; j < dgvBoard.ColumnCount; j++)
                {
                    ChessColor chessColor = Game.Board.GetFieldColor(i, j);
                    dgvBoard.Rows[i].Cells[j].Style.BackColor = chessColor == ChessColor.BLACK
                        ? System.Drawing.Color.DarkGray
                        : System.Drawing.Color.WhiteSmoke;
                    dgvBoard.Rows[i].Cells[j].Style.Font = chessFont;
                }
            }
        }

        private void DrawFigures()
        {
            labelWhite.Visible = Game.CurrentPlayer.Color == ChessColor.WHITE;
            labelBlack.Visible = Game.CurrentPlayer.Color == ChessColor.BLACK;
            for (int i = 0; i < dgvBoard.RowCount; i++)
            {
                for (int j = 0; j < dgvBoard.ColumnCount; j++)
                {
                    dgvBoard.Rows[i].Cells[j].Value = string.Empty;
                }
            }
            List<ChessFigure> whiteFigures = Game.WhitePlayer.Figures.Where(f => !f.IsKilled).ToList();
            List<ChessFigure> blackFigures = Game.BlackPlayer.Figures.Where(f => !f.IsKilled).ToList();
            foreach (var whiteFigure in whiteFigures)
            {
                ChessField field = whiteFigure.CurrentField;
                Tuple<int, int> indexes = field.GetIndexes();
                string symbol = whiteFigure.GetSymbol().ToString();
                dgvBoard.Rows[7 - indexes.Item1].Cells[indexes.Item2].Value = symbol;
            }
            foreach (var blackFigure in blackFigures)
            {
                ChessField field = blackFigure.CurrentField;
                Tuple<int, int> indexes = field.GetIndexes();
                string symbol = blackFigure.GetSymbol().ToString();
                dgvBoard.Rows[7 - indexes.Item1].Cells[indexes.Item2].Value = symbol;
            }
        }

        private void PrepareFigures()
        {
            List<string> fieldStrings = new List<string>();
            List<ChessFigure> figures = Game.CurrentPlayer.Figures.Where(f => !f.IsKilled).ToList();
            Game.CurrentPlayer.FindFieldsToMove(Game.Board);
            foreach (ChessFigure figure in figures)
            {
                if (figure.FieldsToMove.Count > 0) 
                    fieldStrings.Add(figure.ToString());
            }
            if (Game.CurrentPlayer.IsRoquePossible(true, Game)) fieldStrings.Add("Long Roque");
            if (Game.CurrentPlayer.IsRoquePossible(false, Game)) fieldStrings.Add("Short Roque");
            fieldStrings.Add("Decision: Offer a Draw");
            fieldStrings.Add("Decision: Give In");
            comboFrom.DataSource = fieldStrings;
            comboFrom.SelectedIndex = 0;
        }

        private void PrepareMoves()
        {
            List<string> fieldStrings = new List<string>();
            ChessFigure figureToMove = GetFigure();
            if (figureToMove == null) return;
            
            foreach (ChessField moveTo in figureToMove.FieldsToMove)
            {
                fieldStrings.Add(moveTo.Column.ToString() + moveTo.Row);
            }
            comboTo.DataSource = fieldStrings;
            comboTo.SelectedIndex = 0;
        }

        private void comboFrom_SelectedIndexChanged(object sender, EventArgs e)
        {
            PrepareMoves();
        }

        private void buttonMove_Click(object sender, EventArgs e)
        {
            ChessFigure figureToMove = GetFigure();
            if (figureToMove == null)
            {
                if (comboFrom.Text.Contains("Give In"))
                {
                    MessageBox.Show("You lose!");
                    GameBegins();
                    return;
                }
                if (comboFrom.Text.Contains("Draw"))
                {
                    DialogResult result = MessageBox.Show("Other player, do you want to draw?", "Draw proposal",
                        MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        MessageBox.Show("Draw!");
                        GameBegins();
                    }
                    return;
                }

                ChessKing king = Game.CurrentPlayer.Figures.OfType<ChessKing>().First();
                List<ChessRook> rooks = Game.CurrentPlayer.Figures.OfType<ChessRook>().ToList();
                ChessRook rook = null;
                if (comboFrom.Text == "Long Roque")
                {
                    rook = rooks.First(r => r.CurrentField.Column == 'A');
                }
                if (comboFrom.Text == "Short Roque")
                {
                    rook = rooks.First(r => r.CurrentField.Column == 'H');
                }
                if (rook == null) return;
                rook.MakeRoque(Game.Board, king, Game.CurrentMoveNumber);
            }
            else
            {
                char[] columnAndRow = comboTo.Text.ToCharArray();
                ChessField field = Game.Board.GetField(int.Parse(columnAndRow[1].ToString()), columnAndRow[0]);
                ChessPlayer enemy = Game.CurrentPlayer.Color == ChessColor.WHITE ? Game.BlackPlayer : Game.WhitePlayer;
                figureToMove.Move(field, enemy, Game.Board, Game.CurrentMoveNumber, Game.CurrentPlayer.Figures);
            }

            bool check = Check();
            bool mate = CheckMate();
            Game.ChangePlayer();
            DrawFigures();
            PrepareFigures();
            if (mate)
            {
                MessageBox.Show("MATE!");
                GameBegins();
            } else if (check)
            {
                MessageBox.Show("CHECK!");
            }
        }

        private bool Check()
        {
            return Game.Check();
        }

        private bool CheckMate()
        {
            if (!Game.Check()) return false;
            return Game.CheckMate();
        }

        private ChessFigure GetFigure()
        {
            string selectedText = comboFrom.Text;
            if (selectedText.Contains("Roque") || selectedText.Contains("Decision")) return null;
            
            int row = int.Parse(selectedText.Substring(selectedText.Length - 1));
            char column = selectedText.ToCharArray()[selectedText.Length - 2];
            ChessField field = Game.Board.GetField(row, column);
            ChessFigure figureToMove = null;
            foreach (var figure in Game.CurrentPlayer.Figures)
            {
                if (figure.CurrentField == field)
                {
                    figureToMove = figure;
                    break;
                }
            }
            return figureToMove;
        }
    }
}
