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

        public Fraction Fraction { get; set; }


        public Cell(int x, int y, bool willBeAlive)
        {
            xPos = x;
            yPos = y;
            this.isAlife = willBeAlive;
        }
    }
}
