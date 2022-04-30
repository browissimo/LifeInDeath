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

        //public Fraction Fraction { get; set; }
        public int fracrion;

        public Cell(int x=0, int y=0, bool willBeAlive=false)
        {
            xPos = x;
            yPos = y;
            this.isAlife = willBeAlive;
        }
    }
}
