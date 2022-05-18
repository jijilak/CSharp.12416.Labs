using System.Collections.Generic;

namespace Chess.Model
{
    public class ChessKing : ChessFigure
    {
        public ChessKing(Color color, ChessBoard board) : base(color)
        {
            CurrentField = color == Color.WHITE ? 
                board.OccupyField(1, 'E', Color.WHITE) : 
                board.OccupyField(8, 'E', Color.BLACK);
        }

        public override char GetSymbol()
        {
            return Color == Color.WHITE ? '\u2654' : '\u265a';
        }

        public override void FindFieldsToMove(ChessBoard board)
        {
            FieldsToMove.Clear();
            FieldStatus ownStatus = Color == Color.WHITE
                ? FieldStatus.OCCUPIED_WITH_WHITE
                : FieldStatus.OCCUPIED_WITH_BLACK;

            List<int> potentialRows = new List<int>();
            if (CurrentField.Row != 1) potentialRows.Add(CurrentField.Row - 1);
            potentialRows.Add(CurrentField.Row);
            if (CurrentField.Row != 8) potentialRows.Add(CurrentField.Row + 1);
            List<char> potentialColumns = new List<char>();
            if (CurrentField.Column != 'A') potentialColumns.Add((char)((int)CurrentField.Column - 1));
            potentialColumns.Add(CurrentField.Column);
            if (CurrentField.Column != 'H') potentialColumns.Add((char)((int)CurrentField.Column + 1));
            
            foreach (int row in potentialRows)
            {
                foreach (char column in potentialColumns)
                {
                    ChessField field = board.GetField(row, column);
                    if (field.Status != ownStatus) FieldsToMove.Add(field);
                }
            }
        }

        public bool IsRoquePossible()
        {
            if (PreviousMoves.Count == 0) return true;
            return false;
        }
    }
}
