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
        

        public GameEngine(FieldСharacteristics fieldСharacteristics)
        {
            rows = fieldСharacteristics.rows;
            cols = fieldСharacteristics.columns;
            field = new Cell[cols, rows];
            Random random = new Random();

            for (int i = 0; i < field.GetLength(0); i++)
            {
                for (int j = 0; j < field.GetLength(1); j++)
                {
                    var isAlive = random.Next(fieldСharacteristics.density) == 0;
                    field[i, j] = new Cell(i,j, isAlive);
                    field[i, j].fracrion = random.Next(1, 3);
                }
            }
        }

        private int CountNeighboursByFraction(Cell cell)
        {
            int countAliveEnemiesAround = 0;
            int countAliveAllyAround = 0;

            for (int i = -1; i < 2; i++)
            {
                for (int j = -1; j < 2; j++)
                {
                    int neighbourCellColumn = (cell.xPos + i + cols) % cols; 
                    int neighbourCellRow = (cell.yPos + j + rows) % rows;

                    Cell neighbour = field[neighbourCellColumn, neighbourCellRow];

                    bool isSelfChecking = cell.checkingForOwnCoordinates(neighbour);

                    if (neighbour.fracrion != cell.fracrion && !isSelfChecking && neighbour.isAlife)
                        countAliveEnemiesAround++;
                    else if (neighbour.fracrion == cell.fracrion && !isSelfChecking && neighbour.isAlife)
                        countAliveAllyAround++;
                }
            }

            cell.ReactionOnNeighbours(countAliveAllyAround, countAliveEnemiesAround);

            return countAliveAllyAround + countAliveEnemiesAround;
        }



        public void NextGenegation()
        {
            foreach (var cell in field)
            {
                var neighboursCount = CountNeighboursByFraction(cell);
                if (!cell.isAlife && neighboursCount == 3)
                    cell.isAlife = true;
                else if (cell.isAlife && neighboursCount < 2 || neighboursCount > 3)
                    cell.isAlife = false;
            }
            CurrentGeneration++;
        }

        public Cell[,] GetCurrentGeneration()
        {
            return field;
        }

        public void AddCell(int x, int y)
        {
            try
            {
                field[x, y].isAlife = true;
            }
            catch
            {
                return;
            }
        }

        public void RemoveCell(int x, int y)
        {
            try
            {
                field[x, y].isAlife = false;
            }
            catch (Exception)
            {
                return;
            }
        }
    }


}
