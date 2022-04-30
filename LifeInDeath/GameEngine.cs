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
        private Cell[] field;
        private readonly int rows;
        private readonly int cols;
        

        public GameEngine(int rows, int cols, int density)
        {
            this.rows = rows;
            this.cols = cols;
            field = new Cell[cols * rows];

            Random random = new Random();
            int x = 1, 
                y = 1;

            for (int i = 0; i < field.Length; i++)
            {
                if (x > cols)
                {
                    x = 1;
                    y++;
                }

                field[i] = new Cell();

                field[i].xPos = x;
                field[i].yPos = y;
                x++;

                field[i].isAlife = random.Next(density) == 0;
                field[i].fracrion = random.Next(1, 3);
            }
        }

        private int CountNeighbours(Cell cell)
        {
            int count = 0;

            for (int i = -1; i < 2; i++)
            {
                for (int j = -1; j < 2; j++)
                {
                    int cellCol = (cell.xPos + i + cols) % cols;
                    int cellRow = (cell.yPos + j + rows) % rows;

                    bool isSelfChecking = cellCol == cell.xPos && cellRow == cell.yPos;

                    var neighbor = field.FirstOrDefault(c => c.xPos == cellCol && c.yPos == cellRow);

                    if (neighbor != null)
                    {
                        if (neighbor.isAlife && !isSelfChecking)
                        {
                            count++;
                        }
                    }
                }
            }

            return count;
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

        public Cell[] GetCurrentGeneration()
        {
            var result = new Cell[cols * rows];
            
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

        //public void AddCell(int x, int y)
        //{
        //    UpdateCell(x, y, state:true);
        //}

        //public void RemoveCell(int x, int y)
        //{
        //    UpdateCell(x, y, state: false);
        //}
    }


}
