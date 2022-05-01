using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeInDeath
{
    public class GameEngine
    {
        public uint CurrentGeneration { get; private set; }
        private Cell[,] field;
        private readonly int rows;
        private readonly int cols;
        

        public GameEngine(int rows, int cols, int density)
        {
            this.rows = rows;
            this.cols = cols;
            field = new Cell[cols, rows];

            Random random = new Random();
            int x = 1, 
                y = 1;

            for (int i = 0; i < field.GetLength(0); i++)
            {
                for (int j = 0; j < field.GetLength(1); j++)
                {
                    var isAlive = random.Next(density) == 0;
                    field[i, j] = new Cell(i,j, isAlive);
                    field[i, j].fracrion = random.Next(1, 3);
                }
            }
        }

        private int CountNeighbours(Cell cell)
        {
            int countAliveNeighbours = 0;
            int countAliveEnemiesAround = 0;
            int countAliveAllyAround = 0;

            for (int i = -1; i < 2; i++)
            {
                for (int j = -1; j < 2; j++)
                {
                    int neighbourCellCol = (cell.xPos + i + cols) % cols; 
                    int neighbourCellRow = (cell.yPos + j + rows) % rows;

                    Cell neighbour = field[neighbourCellCol, neighbourCellRow];

                    bool isSelfChecking = cell.checkingForOwnCoordinates(neighbour);

                    if (neighbour.isAlife && !isSelfChecking)
                        countAliveNeighbours++;

                    bool isNeighborEnemy = neighbour.fracrion != cell.fracrion;

                    if (isNeighborEnemy && !isSelfChecking && neighbour.isAlife)
                        countAliveEnemiesAround++;

                    bool isNeighborAlly = neighbour.fracrion == cell.fracrion;

                    if (isNeighborAlly && !isSelfChecking && neighbour.isAlife)
                        countAliveAllyAround++;
                }
            }

            if (countAliveEnemiesAround > countAliveAllyAround)
            {
                if (cell.fracrion == 1)
                {
                    cell.fracrion = 2;
                }
                else
                {
                    cell.fracrion = 1;
                }
            }

            return countAliveNeighbours;
        }

        public void NextGenegation()
        {
            var newField = field;

            foreach (var cell in newField)
            {
                var neighboursCount = CountNeighbours(cell);
                var hasLife = cell.isAlife;

                if (!hasLife && neighboursCount == 3)
                {
                    cell.isAlife = true;
                }
                else if (hasLife && neighboursCount < 2 || neighboursCount > 3)
                {
                    cell.isAlife = false;
                }
            }

            field = newField;
            CurrentGeneration++;
        }

        public Cell[,] GetCurrentGeneration()
        {
            var result = new Cell[cols, rows];
            
            Array.Copy(field, result, cols * rows);
            
            return result;
        }

        private bool ValidateCellPosition(Cell cell)
        {
            return cell.xPos >= 0 && cell.yPos >= 0 && cell.xPos < cols && cell.yPos < rows;
        }

        private void UpdateCell(Cell cell, bool state)
        {
            if (ValidateCellPosition(cell))
            {
                cell.isAlife = state;
            }
        }

        public void AddCell(int x, int y)
        {
            var cell = field[x,y];
            UpdateCell(cell, state: true);
        }

        public void RemoveCell(int x, int y)
        {
            var cell = field[x, y];
            UpdateCell(cell, state: false);
        }
    }


}
