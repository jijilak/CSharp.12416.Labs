namespace Chess.Model
{
    public class ChessRook : ChessFigure
    {
        public ChessRook(Color color, ChessBoard board, bool isFirst) : base(color)
        {
            if (color == Color.WHITE)
            {
                CurrentField = isFirst ?
                    board.OccupyField(1, 'A', Color.WHITE) : 
                    board.OccupyField(1, 'H', Color.WHITE);
            }
            else
            {
                CurrentField = isFirst ?
                    board.OccupyField(8, 'A', Color.BLACK) : 
                    board.OccupyField(8, 'H', Color.BLACK);
            }
        }
        
        public override char GetSymbol()
        {
            return Color == Color.WHITE ? '\u2656' : '\u265c';
        }

        public override void FindFieldsToMove(ChessBoard board)
        {
            if (IsKilled) return;
            FieldsToMove.Clear();
            FieldStatus ownStatus = Color == Color.WHITE
                ? FieldStatus.OCCUPIED_WITH_WHITE
                : FieldStatus.OCCUPIED_WITH_BLACK;
            FindFieldsInColumn(board, ownStatus);
            FindFieldsInRow(board, ownStatus);
        }
        
        public bool IsRoquePossible()
        {
            if (PreviousMoves.Count == 0 && !IsKilled) return true;
            return false;
        }

        public void MakeRoque(ChessBoard board, ChessFigure king, int moveNumber)
        {
            ChessField kingNewField, rookNewField;
            if (Color == Color.WHITE)
            {
                kingNewField = CurrentField.Column == 'A' ? board.GetField(1, 'C') : board.GetField(1, 'G');
                rookNewField = CurrentField.Column == 'A' ? board.GetField(1, 'D') : board.GetField(1, 'F');
            }
            else
            {
                kingNewField = CurrentField.Column == 'A' ? board.GetField(8, 'C') : board.GetField(8, 'G');
                rookNewField = CurrentField.Column == 'A' ? board.GetField(8, 'D') : board.GetField(8, 'F');
            }
            
            ChessMove kingMove = new ChessMove(Color, moveNumber, king, null, king.CurrentField, kingNewField, true);
            king.PreviousMoves.Add(kingMove);
            ChessMove move = new ChessMove(Color, moveNumber, this, null, CurrentField, rookNewField, true);
            PreviousMoves.Add(move);

            board.EmptyField(king.CurrentField);
            board.EmptyField(CurrentField);
            board.OccupyField(kingNewField.Row, kingNewField.Column, Color);
            board.OccupyField(rookNewField.Row, rookNewField.Column, Color);
            king.CurrentField = kingNewField;
            CurrentField = rookNewField;
            king.FindFieldsToMove(board);
            FindFieldsToMove(board);
        }
    }
}
