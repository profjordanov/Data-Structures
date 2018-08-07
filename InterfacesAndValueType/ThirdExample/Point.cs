using System;

namespace ThirdExample
{
    // Point is a value type. 
    public struct Point : IChangeBoxedPoint
    {
        private Int32 m_x, m_y;
        public Point(Int32 x , Int32 y)
        {
            m_x = x;
            m_y = y;
        }
        public void Change(Int32 x , Int32 y)
        {
            m_x = x; m_y = y;
        }
        public override String ToString()
        {
            return String.Format("({0}, {1})" , m_x.ToString() , m_y.ToString());
        }
    }
}