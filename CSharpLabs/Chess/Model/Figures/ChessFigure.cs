using System.Collections.Generic;

namespace Chess.Model
{
    public abstract class ChessFigure
    {
        public Color Color { get; private set; }
        public ChessField CurrentField { get; set; }
        public List<ChessField> FieldsToMove { get; private set; }
        public List<ChessMove> PreviousMoves { get; set; }
        public bool IsKilled { get; private set; }

        protected ChessFigure(Color color)
        {
            Color = color;
            FieldsToMove = new List<ChessField>();
            PreviousMoves = new List<ChessMove>();
            IsKilled = false;
        }

        public void FigureKilled()
        {
            IsKilled = true;
            CurrentField = null;
            FieldsToMove = null;
        }

        public virtual void Move(ChessField field, ChessPlayer enemy, ChessBoard board, int moveNumber, ChessFigure[] otherFigures)
        {
            FieldStatus enemyStatus =
                Color == Color.WHITE ? FieldStatus.OCCUPIED_WITH_BLACK : FieldStatus.OCCUPIED_WITH_WHITE;
            ChessFigure killedFigure = null;
            if (field.Status == enemyStatus)
            {
                foreach (ChessFigure enemyFigure in enemy.Figures)
                {
                    if (enemyFigure.CurrentField == field)
                    {
                        enemyFigure.FigureKilled();
                        killedFigure = enemyFigure;
                        break;
                    }

                    if (enemyFigure is ChessPawn && ((ChessPawn) enemyFigure).EnPassant 
                                                 && enemyFigure.CurrentField.Column == field.Column)
                    {
                        if ((Color == Color.WHITE && field.Row == 6 && ((ChessPawn) enemyFigure).EnPassantMove == moveNumber - 1) 
                            || (Color == Color.BLACK && field.Row == 3 && ((ChessPawn) enemyFigure).EnPassantMove == moveNumber))
                        {
                            enemyFigure.FigureKilled();
                            killedFigure = enemyFigure;
                            break;
                        }
                    }
                }
            }
            ChessMove move = new ChessMove(Color, moveNumber, this, killedFigure, CurrentField, field);
            PreviousMoves.Add(move);
            board.EmptyField(CurrentField);
            board.OccupyField(field.Row, field.Column, Color);
            CurrentField = field;
            FindFieldsToMove(board);
        }

        public abstract char GetSymbol();
        public virtual void FindFieldsToMove(ChessBoard board) {}

        public override string ToString()
        {
            return GetSymbol() + ": " + CurrentField.Column + CurrentField.Row;
        }

        #region Logic for Bishops, Rooks and Queens
        protected void FindFieldsInColumn(ChessBoard board, FieldStatus ownStatus)
        {
            char column = CurrentField.Column;
            int row = CurrentField.Row - 1;
            while (row > 0)
            {
                ChessField field = board.GetField(row, column);
                if (field.Status != ownStatus) FieldsToMove.Add(field);
                if (field.Status != FieldStatus.EMPTY) break;
                row--;
            }
            row = CurrentField.Row + 1;
            while (row <= 8)
            {
                ChessField field = board.GetField(row, column);
                if (field.Status != ownStatus) FieldsToMove.Add(field);
                if (field.Status != FieldStatus.EMPTY) break;
                row++;
            }
        }

        protected void FindFieldsInRow(ChessBoard board, FieldStatus ownStatus)
        {
            int row = CurrentField.Row;
            char column = (char)((int)CurrentField.Column - 1);
            while ((int)column >= (int)'A')
            {
                ChessField field = board.GetField(row, column);
                if (field.Status != ownStatus) FieldsToMove.Add(field);
                if (field.Status != FieldStatus.EMPTY) break;
                column--;
            }
            column = (char)((int)CurrentField.Column + 1);
            while ((int)column <= (int)'H')
            {
                ChessField field = board.GetField(row, column);
                if (field.Status != ownStatus) FieldsToMove.Add(field);
                if (field.Status != FieldStatus.EMPTY) break;
                column++;
            }
        }

        protected void FindFieldsUpwards(ChessBoard board, FieldStatus ownStatus)
        {
            int row = CurrentField.Row + 1;
            char column = (char)((int)CurrentField.Column - 1);
            while (row <= 8 && (int)column >= (int)'A')
            {
                ChessField field = board.GetField(row, column);
                if (field.Status != ownStatus) FieldsToMove.Add(field);
                if (field.Status != FieldStatus.EMPTY) break;
                row++;
                column--;
            }
            row = CurrentField.Row + 1;
            column = (char)((int)CurrentField.Column + 1);
            while (row <= 8 && (int)column <= (int)'H')
            {
                ChessField field = board.GetField(row, column);
                if (field.Status != ownStatus) FieldsToMove.Add(field);
                if (field.Status != FieldStatus.EMPTY) break;
                row++;
                column++;
            }
        }

        protected void FindFieldsDownwards(ChessBoard board, FieldStatus ownStatus)
        {
            int row = CurrentField.Row - 1;
            char column = (char)((int)CurrentField.Column - 1);
            while (row > 0 && (int)column >= (int)'A')
            {
                ChessField field = board.GetField(row, column);
                if (field.Status != ownStatus) FieldsToMove.Add(field);
                if (field.Status != FieldStatus.EMPTY) break;
                row--;
                column--;
            }
            row = CurrentField.Row - 1;
            column = (char)((int)CurrentField.Column + 1);
            while (row > 0 && (int)column <= (int)'H')
            {
                ChessField field = board.GetField(row, column);
                if (field.Status != ownStatus) FieldsToMove.Add(field);
                if (field.Status != FieldStatus.EMPTY) break;
                row--;
                column++;
            }
        }
        #endregion
    }
}