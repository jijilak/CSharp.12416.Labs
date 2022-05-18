using System.Windows.Forms;

namespace Chess.Model
{
    public class ChessPawn : ChessFigure
    {
        public bool EnPassant { get; private set; } = false;
        public int EnPassantMove = 0;
        public ChessPawn(Color color, ChessBoard board, char position) : base(color)
        {
            CurrentField = color == Color.WHITE ? 
                board.OccupyField(2, position, Color.WHITE) : 
                board.OccupyField(7, position, Color.BLACK);
        }

        public override char GetSymbol()
        {
            return Color == Color.WHITE ? '\u2659' : '\u265f';
        }

        public override void FindFieldsToMove(ChessBoard board)
        {
            if (IsKilled) return;
            FieldsToMove.Clear();
            FieldStatus enemyStatus = Color == Color.WHITE
                ? FieldStatus.OCCUPIED_WITH_BLACK
                : FieldStatus.OCCUPIED_WITH_WHITE;
            int row = Color == Color.WHITE ? CurrentField.Row + 1 : CurrentField.Row - 1;
            if (row < 1 || row > 8) return;
            ChessField field = board.GetField(row, CurrentField.Column);
            if (field.Status == FieldStatus.EMPTY) FieldsToMove.Add(field);
            
            char column = (char)(CurrentField.Column - 1);
            field = board.GetField(row, column);
            if (field.Status == enemyStatus) FieldsToMove.Add(field);
            column = (char)(CurrentField.Column + 1);
            field = board.GetField(row, column);
            if (field.Status == enemyStatus) FieldsToMove.Add(field);

            if (PreviousMoves.Count == 0)
            {
                row = Color == Color.WHITE ? CurrentField.Row + 2 : CurrentField.Row - 2;
                field = board.GetField(row, CurrentField.Column);
                if (field.Status == FieldStatus.EMPTY) FieldsToMove.Add(field);
            }
        }

        public override void Move(ChessField field, ChessPlayer enemy, ChessBoard board, int moveNumber, ChessFigure[] otherFigures)
        {
            if (PreviousMoves.Count == 0)
            {
                if ((Color == Color.WHITE && field.Row == 4) || (Color == Color.BLACK && field.Row == 5))
                {
                    EnPassant = true;
                    EnPassantMove = moveNumber;
                }
            }
            else
            {
                EnPassant = false;
            }

            base.Move(field, enemy, board, moveNumber, otherFigures);
            if ((Color == Color.WHITE && field.Row == 8) || (Color == Color.BLACK && field.Row == 1))
            {
                int pawnIndex = 16;
                for (var i = 0; i < otherFigures.Length; i++)
                {
                    var figure = otherFigures[i];
                    if (this == figure) pawnIndex = i;
                }

                ChessFigure newFigure;
                DialogResult result = MessageBox.Show("Do you want to turn into Queen?", "Pawn To Queen",
                    MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes) newFigure = new ChessQueen(Color, board);
                else
                {
                    result = MessageBox.Show("Do you want to turn into Rook?", "Pawn To Rook",
                        MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes) newFigure = new ChessRook(Color, board, false);
                    else
                    {
                        result = MessageBox.Show("Do you want to turn into Knight?", "Pawn To Knight",
                            MessageBoxButtons.YesNo);
                        if (result == DialogResult.Yes) newFigure = new ChessKnight(Color, board, false);
                        else newFigure = new ChessBishop(Color, board, false);
                    }
                }
                newFigure.CurrentField = field;
                newFigure.PreviousMoves = PreviousMoves;
                newFigure.FindFieldsToMove(board);
                otherFigures[pawnIndex] = newFigure;
            }
        }
    }
}
