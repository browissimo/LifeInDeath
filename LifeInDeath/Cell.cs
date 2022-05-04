using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeInDeath
{
    public class Cell
    {
        public int xPos;
        public int yPos;
        public bool isAlife = false;
        public int fracrion;
        private int allyNeighbourCount;
        private int enemiesNeighbourCount;

        public Cell(int x, int y, bool willBeAlive)
        {
            xPos = x;
            yPos = y;
            isAlife = willBeAlive;
        }

        public bool checkingForOwnCoordinates(Cell cell)
        {
            return xPos == cell.xPos && yPos == cell.yPos;
        }

        public void ReactionOnNeighbours(int allyCellCount, int enimesCellCount)
        {
            if (enimesCellCount > allyCellCount)
            {
                if (fracrion == 1)
                {
                    fracrion = 2;
                }
                else
                {
                    fracrion = 1;
                }
            }
        }
    }
}
