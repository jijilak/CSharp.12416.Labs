namespace Chess.Model
{
    public class ChessBoard
    {
        private ChessField[,] Fields { get; set; }

        public ChessBoard()
        {
            Fields = new ChessField[8,8];
            for (int i = 0; i < 8; i++)     //Идем по линиям
            {
                for (int j = 0; j < 8; j++) //Идем по колонкам
                {
                    ChessField field = new ChessField(i, j);
                    field.Status = FieldStatus.EMPTY;
                    Fields[i, j] = field;
                }
            }
        }

        public ChessField OccupyField(int row, char column, Color color)
        {
            ChessField field = GetField(row, column);
            field.Status = color == Color.WHITE ? FieldStatus.OCCUPIED_WITH_WHITE : FieldStatus.OCCUPIED_WITH_BLACK;
            return field;
        }

        public void EmptyField(ChessField field)
        {
            field.Status = FieldStatus.EMPTY;
        }
        
        public ChessField GetField(int row, char column)
        {
            int rowIndex = row - 1;
            int columnIndex = 0;
            switch (column)
            {
                case 'A': columnIndex = 0; break;
                case 'B': columnIndex = 1; break;
                case 'C': columnIndex = 2; break;
                case 'D': columnIndex = 3; break;
                case 'E': columnIndex = 4; break;
                case 'F': columnIndex = 5; break;
                case 'G': columnIndex = 6; break;
                case 'H': columnIndex = 7; break;
            }
            return Fields[rowIndex, columnIndex];
        }

        public Color GetFieldColor(int rowIndex, int columnIndex)
        {
            return Fields[rowIndex, columnIndex].Color;
        }
    }
}
