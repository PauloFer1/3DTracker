using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Windows.Media.Media3D;

namespace WiiTest
{
    class Coordinate
    {
        private StringBuilder sb;
        private int inc;
        private decimal x;
        private decimal y;
        private decimal z;
        private decimal time;

        public Coordinate(int inc, decimal x, decimal y, decimal z, decimal time)
        {
            this.inc = inc;
            this.x = x;
            this.y = y;
            this.time = time;

            sb = new StringBuilder();
            sb.Append("C" + inc).Append("=");
            sb.Append(x.ToString(CultureInfo.GetCultureInfo("en-GB"))).Append(",").Append(y.ToString(CultureInfo.GetCultureInfo("en-GB"))).Append(",").Append("0,").Append("0,").Append("0,").Append(z.ToString(CultureInfo.GetCultureInfo("en-GB")));
            sb.AppendLine();
        }
        public StringBuilder toString()
        {
            return (sb);
        }
        public void calculateVelocity(Coordinate lastPoint)
        {

        }

        //»»»»»»»»»»»»»»»»»»»»»»»»»»»» GETS
        public decimal getX()
        {
            return(this.x);
        }
        public decimal getY()
        {
            return (this.y);
        }
        public decimal getZ()
        {
            return (this.z);
        }
        public decimal getTime()
        {
            return (this.time);
        }
        public Vector3D getVector(Coordinate last)
        {
            Vector3D v = new Vector3D();
            v.X = (double)this.x;
            v.Y = (double)this.y;
            v.Z = (double)this.z;

            return(v);
        }
    }
}
