using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WiiTest
{
    class Coordinate
    {
        private StringBuilder sb;
        public Coordinate(int inc, decimal x, decimal y, decimal z)
        {
            sb = new StringBuilder();
            sb.Append("C" + inc);
            sb.Append(x).Append(",").Append(y).Append(",").Append("0").Append("0").Append("0").Append(z);
        }
        public StringBuilder toString()
        {
            return (sb);
        }
    }
}
