using System;

public class Point2D : IComparable<Point2D>
{
    public Point2D(double x, double y)
    {
        X = x;
        Y = y;
    }

    public double X { get; set; }
    public double Y { get; set; }

    public override string ToString()
    {
        return $"({X}, {Y})";
    }

    public override bool Equals(object obj)
    {
        if (obj == this) return true;
        if (obj == null) return false;
        if (obj.GetType() != GetType()) return false; 
        var that = (Point2D)obj;
        return X == that.X && Y == that.Y;
    }

    public override int GetHashCode()
    {
        var hashX = X.GetHashCode();
        var hashY = Y.GetHashCode();
        return 31 * hashX + hashY;
    }

    public int CompareTo(Point2D that)
    {
        if (Y < that.Y) return -1;
        if (Y > that.Y) return +1;
        if (X < that.X) return -1;
        if (X > that.X) return +1;
        return 0;
    }
}
