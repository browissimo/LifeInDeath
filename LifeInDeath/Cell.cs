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

        public Cell(int x, int y, bool willBeAlive)
        {
            xPos = x;
            yPos = y;
            this.isAlife = willBeAlive;
        }

        public bool checkingForOwnCoordinates(Cell cell)
        {
            return this.xPos == cell.xPos && this.yPos == cell.yPos;
        }
    }
}
