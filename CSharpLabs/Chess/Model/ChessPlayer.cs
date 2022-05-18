using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Model
{
    public class ChessPlayer
    {
        public Color Color { get; set; }
        public ChessFigure[] Figures { get; private set; }

        public ChessPlayer(Color color, ChessBoard board)
        {
            Color = color;
            Figures = new ChessFigure[16];
            Figures[0] = new ChessKing(color, board);
            Figures[1] = new ChessQueen(color, board);
            Figures[2] = new ChessBishop(color, board, true);
            Figures[3] = new ChessBishop(color, board, false);
            Figures[4] = new ChessKnight(color, board, true);
            Figures[5] = new ChessKnight(color, board, false);
            Figures[6] = new ChessRook(color, board, true);
            Figures[7] = new ChessRook(color, board, false);
            Figures[8] = new ChessPawn(color, board, 'A');
            Figures[9] = new ChessPawn(color, board, 'B');
            Figures[10] = new ChessPawn(color, board, 'C');
            Figures[11] = new ChessPawn(color, board, 'D');
            Figures[12] = new ChessPawn(color, board, 'E');
            Figures[13] = new ChessPawn(color, board, 'F');
            Figures[14] = new ChessPawn(color, board, 'G');
            Figures[15] = new ChessPawn(color, board, 'H');
        }

        public void FindFieldsToMove(ChessBoard board)
        {
            foreach (var figure in Figures)
            {
                figure.FindFieldsToMove(board);
            }
        }
        
        public bool IsRoquePossible(bool firstRook, ChessGame game)
        {
            ChessKing king = Figures.OfType<ChessKing>().First();
            if (!king.IsRoquePossible()) return false;
            List<ChessRook> rooks = Figures.OfType<ChessRook>().ToList();
            ChessRook rook = firstRook
                ? rooks.FirstOrDefault(r => r.CurrentField.Column == 'A')
                : rooks.FirstOrDefault(r => r.CurrentField.Column == 'H');
            if (rook == null) return false;
            if (!rook.IsRoquePossible()) return false;
            
            int row = Color == Color.WHITE ? 1 : 8;
            if (game.IsFieldUnderAttack(Color, row, 'E')) return false;

            List<ChessField> fieldsBetween = new List<ChessField>();
            if (firstRook)
            {
                fieldsBetween.Add(game.Board.GetField(row, 'B'));
                fieldsBetween.Add(game.Board.GetField(row, 'C'));
                fieldsBetween.Add(game.Board.GetField(row, 'D'));
            }
            else
            {
                fieldsBetween.Add(game.Board.GetField(row, 'F'));
                fieldsBetween.Add(game.Board.GetField(row, 'G'));
            }
            foreach (ChessField field in fieldsBetween)
            {
                if (field.Status != FieldStatus.EMPTY) return false;
                if (game.IsFieldUnderAttack(Color, field.Row, field.Column)) return false;
            }

            return true;
        }
    }
}
