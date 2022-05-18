using System.Windows.Forms.VisualStyles;

namespace Chess.Model
{
    public class ChessKnight : ChessFigure
    {
        public ChessKnight(Color color, ChessBoard board, bool isFirst) : base(color)
        {
            if (color == Color.WHITE)
            {
                CurrentField = isFirst ?
                    board.OccupyField(1, 'B', Color.WHITE) : 
                    board.OccupyField(1, 'G', Color.WHITE);
            }
            else
            {
                CurrentField = isFirst ?
                    board.OccupyField(8, 'B', Color.BLACK) : 
                    board.OccupyField(8, 'G', Color.BLACK);
            }
        }
        
        public override char GetSymbol()
        {
            return Color == Color.WHITE ? '\u2658' : '\u265e';
        }

        public override void FindFieldsToMove(ChessBoard board)
        {
            if (IsKilled) return;
            FieldsToMove.Clear();
            FieldStatus ownStatus = Color == Color.WHITE
                ? FieldStatus.OCCUPIED_WITH_WHITE
                : FieldStatus.OCCUPIED_WITH_BLACK;
            int row;
            char column = '\0';
            for (int i = 0; i < 8; i++)
            {
                if (i < 2) row = CurrentField.Row + 2;
                else if (i < 4) row = CurrentField.Row + 1;
                else if (i < 6) row = CurrentField.Row - 1;
                else row = CurrentField.Row - 2;
                if (i == 2 || i == 4) column = (char) (CurrentField.Column - 2);
                else if (i == 0 || i == 6) column = (char) (CurrentField.Column - 1);
                else if (i == 1 || i == 7) column = (char) (CurrentField.Column + 1);
                else if (i == 3 || i == 5) column = (char) (CurrentField.Column + 2);
                if (row < 1 || row > 8 || column < 'A' || column > 'H') continue;

                ChessField field = board.GetField(row, column);
                if (field.Status != ownStatus) FieldsToMove.Add(field);
            }
        }
    }
}
