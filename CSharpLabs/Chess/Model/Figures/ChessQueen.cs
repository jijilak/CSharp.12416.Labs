namespace Chess.Model
{
    public class ChessQueen : ChessFigure
    {
        public ChessQueen(Color color, ChessBoard board) : base(color)
        {
            CurrentField = color == Color.WHITE ? 
                board.OccupyField(1, 'D', Color.WHITE) : 
                board.OccupyField(8, 'D', Color.BLACK);
        }
        
        public override char GetSymbol()
        {
            return Color == Color.WHITE ? '\u2655' : '\u265b';
        }

        public override void FindFieldsToMove(ChessBoard board)
        {
            if (IsKilled) return;
            FieldsToMove.Clear();
            FieldStatus ownStatus = Color == Color.WHITE
                ? FieldStatus.OCCUPIED_WITH_WHITE
                : FieldStatus.OCCUPIED_WITH_BLACK;
            FindFieldsInRow(board, ownStatus);
            FindFieldsInColumn(board, ownStatus);
            FindFieldsUpwards(board, ownStatus);
            FindFieldsDownwards(board, ownStatus);
        }
    }
}
