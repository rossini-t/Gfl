using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gfl
{
    struct YCC
    {
        private double m_y;

        internal double Y
        {
            get { return m_y; }            
        }

        private double m_cb;

        internal double Cb
        {
            get { return m_cb; }            
        }
        private double m_cr;

        internal  double Cr
        {
            get { return m_cr; }            
        }

        public YCC(System.Drawing.Color color)
        {
            m_y     =   0   + (0.299 * color.R)     + (0.587 * color.G)     +   (0.114 * color.B);
            m_cb    =   128 - (0.168736 * color.R)  - (0.331264 * color.G)  +   (0.5 * color.B);
            m_cr    =   128 + (0.5 * color.R)       - (0.418688 * color.G)  -    (0.081312 * color.B);
        }

        public YCC(int r, int g, int b)
        {
            m_y = 0 + (0.299 * r) + (0.587 * g) + (0.114 * b);
            m_cb = 128 - (0.168736 * r) - (0.331264 * g) + (0.5 * b);
            m_cr = 128 + (0.5 * r) - (0.418688 * g) - (0.081312 * b);
        }

        public override string ToString()
        {
            return string.Format("Y:{0} Cb:{1} Cr:{2}", new string[] { m_y.ToString(), m_cb.ToString(), m_cr.ToString() });
        }
    }
}
