namespace Chess.Model
{
    public class ChessMove
    {
        public Color Color { get; private set; }
        public int MoveNumber { get; private set; }
        public ChessFigure MovingFigure { get; private set; }
        public ChessFigure KilledFigure { get; private set; }
        public ChessField InitialField { get; private set; }
        public ChessField ResultField { get; private set; }
        public bool IsRoque { get; private set; }
        
        public ChessMove(Color color, int moveNumber, ChessFigure figure, ChessFigure killed, ChessField initial,
            ChessField result, bool isRoque = false)
        {
            Color = color;
            MoveNumber = moveNumber;
            MovingFigure = figure;
            KilledFigure = killed;
            InitialField = initial;
            ResultField = result;
            IsRoque = isRoque;
        }
    }
}
