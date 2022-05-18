using System;

namespace Chess.Model
{
    public class ChessField
    {
        public int Row { get; private set; }
        public char Column { get; private set; }
        public Color Color { get; private set; }
        public FieldStatus Status { get; set; }

        public ChessField(int rowIndex, int columnIndex)
        {
            Row = rowIndex + 1;
            switch (columnIndex)
            {
                case 0: Column = 'A'; break;
                case 1: Column = 'B'; break;
                case 2: Column = 'C'; break;
                case 3: Column = 'D'; break;
                case 4: Column = 'E'; break;
                case 5: Column = 'F'; break;
                case 6: Column = 'G'; break;
                case 7: Column = 'H'; break;
            }
            if (rowIndex % 2 == 0)         //Для линий, начинающихся с белой клетки
            {
                Color = columnIndex % 2 == 0 ? Color.BLACK : Color.WHITE;
            }
            else
            {
                Color = columnIndex % 2 == 0 ? Color.WHITE : Color.BLACK;
            }
        }

        public Tuple<int, int> GetIndexes()
        {
            int rowIndex = Row - 1;
            int columnIndex = 0;
            switch (Column)
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
            return new Tuple<int, int>(rowIndex, columnIndex);
        }
    }
}
