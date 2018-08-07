using System;

namespace InterfacesAndValueType
{
    public struct Point : IComparable
    {
        private readonly Int32 m_x, m_y;
        // Constructor to easily initialize the fields
        public Point(int x , int y)
        {
            m_x = x;
            m_y = y;
        }
        // Override ToString method inherited from System.ValueType
        public override string ToString()
        {
            // Return the point as a string. Note: calling ToString prevents boxing
            return string.Format("({0}, {1})" , m_x.ToString() , m_y.ToString());
        }
        // Implementation of type-safe CompareTo method
        public Int32 CompareTo(Point other)
        {
            // Use the Pythagorean Theorem to calculate
            // which point is farther from the origin (0, 0)
            return Math.Sign(Math.Sqrt(m_x * m_x + m_y * m_y)
                             - Math.Sqrt(other.m_x * other.m_x + other.m_y * other.m_y));
        }

        public int CompareTo(object o)
        {
            if(GetType() != o.GetType())
            {
                throw new ArgumentException("o is not a Point");
            }
            // Call type-safe CompareTo method
            return CompareTo((Point)o);
        }
    }
}