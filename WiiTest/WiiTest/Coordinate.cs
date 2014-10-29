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
        private StringBuilder jcrSb;
        private int inc;
        private decimal x;
        private decimal y;
        private decimal z;
        private decimal time;
        private int isNew;


        public Coordinate(int inc, decimal x, decimal y, decimal z, decimal time, int isNew)
        {
            this.inc = inc;
            this.x = x;
            this.y = y;
            this.time = time;
            this.isNew = isNew;

            sb = new StringBuilder();
            sb.Append("C" + inc).Append("=");
            sb.Append(x.ToString(CultureInfo.GetCultureInfo("en-GB"))).Append(",").Append(y.ToString(CultureInfo.GetCultureInfo("en-GB"))).Append(",").Append("0,").Append("0,").Append("0,").Append(z.ToString(CultureInfo.GetCultureInfo("en-GB")));
            sb.AppendLine();
            string suffix = "CP_P,";
            if (isNew == 1)
                suffix = "CP_S,";
            if (isNew == 2)
                suffix = "CP_E,";
            jcrSb = new StringBuilder();
            jcrSb.Append(suffix);
            jcrSb.Append(x.ToString(CultureInfo.GetCultureInfo("en-GB"))).Append(",").Append(z.ToString(CultureInfo.GetCultureInfo("en-GB"))).Append(",").Append(y.ToString(CultureInfo.GetCultureInfo("en-GB"))).Append(",").Append("0.000") ;
            jcrSb.AppendLine();
        }
        public StringBuilder toString()
        {
            return (sb);
        }
        public StringBuilder toJCRString()
        {
            return(jcrSb);
        }
        public void calculateVelocity(Coordinate lastPoint)
        {

        }

        //»»»»»»»»»»»»»»»»»»»»»»»»»»»» GETS
        public int getInc()
        {
            return(this.inc);
        }
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
        //»»»»»»»»»»»»»»»»»»»»»SETS
        public void setIsNew(int isNew)
        {
            this.isNew = isNew;
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
