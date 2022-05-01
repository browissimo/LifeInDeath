using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeInDeath
{
    public class FieldСharacteristics
    {
        public int rows;
        public int columns;
        public int density;

        public FieldСharacteristics(int rows, int columns, int density)
        {
            this.rows = rows;
            this.columns = columns;
            this.density = density;
        }
    }
}
