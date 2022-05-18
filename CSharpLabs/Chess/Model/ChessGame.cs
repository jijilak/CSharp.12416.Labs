using System;
using System.Collections.Generic;
using System.Linq;

namespace Chess.Model
{
    public class ChessGame
    {
        public ChessBoard Board { get; private set; }
        public ChessPlayer WhitePlayer { get; private set; }
        public ChessPlayer BlackPlayer { get; private set; }
        public int CurrentMoveNumber { get; private set; }
        public Result Result { get; private set; }
        public ChessPlayer CurrentPlayer { get; private set; }

        public ChessGame()
        {
            Board = new ChessBoard();
            WhitePlayer = new ChessPlayer(Color.WHITE, Board);
            BlackPlayer = new ChessPlayer(Color.BLACK, Board);
            Result = Result.UNFINISHED;
            CurrentMoveNumber = 1;
            CurrentPlayer = WhitePlayer;
        }

        public bool IsFieldUnderAttack(Color playerColor, int row, char column)
        {
            ChessPlayer attackingPlayer = playerColor == Color.WHITE ? BlackPlayer : WhitePlayer;
            foreach (ChessFigure figure in attackingPlayer.Figures)
            {
                foreach (ChessField field in figure.FieldsToMove)
                {
                    if (field.Row == row && field.Column == column) return true;
                }
            }
            return false;
        }

        public void ChangePlayer()
        {
            if (CurrentPlayer == WhitePlayer) CurrentPlayer = BlackPlayer;
            else
            {
                CurrentMoveNumber++;
                CurrentPlayer = WhitePlayer;
            }
        }

        public bool Check()
        {
            ChessPlayer enemy = CurrentPlayer == WhitePlayer ? BlackPlayer : WhitePlayer;
            Color enemyColor = CurrentPlayer == WhitePlayer ? Color.BLACK : Color.WHITE;
            ChessKing enemyKing = enemy.Figures.OfType<ChessKing>().First();
            ChessField enemyKingField = enemyKing.CurrentField;
            if (IsFieldUnderAttack(enemyColor, enemyKingField.Row, enemyKingField.Column))
            {
                return true;
            }
            return false;
        }

        public bool CheckMate()
        {
            bool isMate = false;
            ChessPlayer enemy = CurrentPlayer == WhitePlayer ? BlackPlayer : WhitePlayer;
            Color enemyColor = CurrentPlayer == WhitePlayer ? Color.BLACK : Color.WHITE;
            ChessKing enemyKing = enemy.Figures.OfType<ChessKing>().First();
            ChessField enemyKingField = enemyKing.CurrentField;
            if (IsFieldUnderAttack(enemyColor, enemyKingField.Row, enemyKingField.Column))
            {
                isMate = true;
                //1. King can move to other field
                List<ChessField> kingToMoveFields = enemyKing.FieldsToMove;
                foreach (ChessField field in kingToMoveFields)
                {
                    if (!IsFieldUnderAttack(enemyColor, field.Row, field.Column))
                    {
                        isMate = false;
                        break;
                    }
                }

                //2. Attacking figures can be killed
                List<ChessFigure> currentFigures = CurrentPlayer.Figures.Where(f => !f.IsKilled).ToList();
                List<ChessFigure> attackingFigures = new List<ChessFigure>();
                foreach (ChessFigure figure in currentFigures)
                {
                    if (figure.FieldsToMove.Contains(enemyKingField)) attackingFigures.Add(figure);
                }

                List<ChessFigure> attackingToKill = new List<ChessFigure>();
                foreach (ChessFigure attackingFigure in attackingFigures)
                {
                    foreach (ChessFigure defendingFigure in enemy.Figures.Where(f => !f.IsKilled))
                    {
                        if (defendingFigure.FieldsToMove.Contains(attackingFigure.CurrentField))
                            attackingToKill.Add(attackingFigure);
                    }
                }

                foreach (ChessFigure figure in attackingToKill)
                {
                    attackingFigures.Remove(figure);
                }


                //3. It's possible to move figure between attacking figure and king
                foreach (ChessFigure attackingFigure in attackingFigures)
                {
                    List<ChessField> fieldsBetween = new List<ChessField>();
                    if (attackingFigure is ChessRook || attackingFigure is ChessQueen)
                    {
                        if (enemyKingField.Column == attackingFigure.CurrentField.Column)
                        {
                            int minRow = Math.Min(enemyKingField.Row, attackingFigure.CurrentField.Row);
                            int maxRow = Math.Max(enemyKingField.Row, attackingFigure.CurrentField.Row);
                            for (int i = minRow + 1; i < maxRow; i++)
                            {
                                fieldsBetween.Add(Board.GetField(i, enemyKingField.Column));
                            }
                        }
                        else if (enemyKingField.Row == attackingFigure.CurrentField.Row)
                        {
                            char minColumn = (char)Math.Min(enemyKingField.Column, attackingFigure.CurrentField.Column);
                            char maxColumn = (char)Math.Max(enemyKingField.Column, attackingFigure.CurrentField.Column);
                            for (int i = minColumn + 1; i < maxColumn; i++)
                            {
                                fieldsBetween.Add(Board.GetField(enemyKingField.Row, (char)i));
                            }
                        }
                    }
                    if (attackingFigure is ChessBishop || attackingFigure is ChessQueen)
                    {
                        int minRow = Math.Min(enemyKingField.Row, attackingFigure.CurrentField.Row);
                        int maxRow = Math.Max(enemyKingField.Row, attackingFigure.CurrentField.Row);
                        char minColumn = (char)Math.Min(enemyKingField.Column, attackingFigure.CurrentField.Column);
                        char maxColumn = (char)Math.Max(enemyKingField.Column, attackingFigure.CurrentField.Column);
                        if ((enemyKingField.Row < attackingFigure.CurrentField.Row &&
                             enemyKingField.Column < attackingFigure.CurrentField.Column)
                            || (enemyKingField.Row > attackingFigure.CurrentField.Row &&
                                enemyKingField.Column > attackingFigure.CurrentField.Column))
                        {
                            for (int i = minRow + 1, j = minColumn + 1; i < maxRow && j < maxColumn; i++, j++)
                            {
                                fieldsBetween.Add(Board.GetField(i, (char)j));
                            }
                        }
                        else
                        {
                            for (int i = maxRow - 1, j = minColumn + 1; i > minRow && j < maxColumn; i--, j++)
                            {
                                fieldsBetween.Add(Board.GetField(i, (char)j));
                            }
                        }
                    }
                    List<ChessFigure> enemyFigures = enemy.Figures.Where(f => f != enemyKing).ToList();
                    foreach (ChessField field in fieldsBetween)
                    {
                        foreach (var figure in enemyFigures)
                        {
                            if (figure.FieldsToMove.Contains(field))
                            {
                                attackingFigures.Remove(attackingFigure);
                            }
                        }
                    }
                }
                if (attackingFigures.Count < 1) isMate = false;
            }
            return isMate;
        }
    }
}
