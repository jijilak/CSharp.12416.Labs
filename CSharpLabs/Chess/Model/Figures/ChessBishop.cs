namespace Chess.Model
{
    public class ChessBishop : ChessFigure
    {
        public ChessBishop(Color color, ChessBoard board, bool isFirst) : base(color)
        {
            if (color == Color.WHITE)
            {
                CurrentField = isFirst ?
                    board.OccupyField(1, 'C', Color.WHITE) : 
                    board.OccupyField(1, 'F', Color.WHITE);
            }
            else
            {
                CurrentField = isFirst ?
                    board.OccupyField(8, 'C', Color.BLACK) : 
                    board.OccupyField(8, 'F', Color.BLACK);
            }
        }

        public override char GetSymbol()
        {
            return Color == Color.WHITE ? '\u2657' : '\u265d';
        }

        public override void FindFieldsToMove(ChessBoard board)
        {
            if (IsKilled) return;
            FieldsToMove.Clear();
            FieldStatus ownStatus = Color == Color.WHITE
                ? FieldStatus.OCCUPIED_WITH_WHITE
                : FieldStatus.OCCUPIED_WITH_BLACK;
            FindFieldsUpwards(board, ownStatus);
            FindFieldsDownwards(board, ownStatus);
        }
    }
}
