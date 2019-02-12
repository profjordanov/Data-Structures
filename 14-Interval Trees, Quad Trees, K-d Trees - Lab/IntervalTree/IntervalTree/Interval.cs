using System;

/// <summary>
/// Implementation of set of real numbers
/// with the property that any number that
/// lies between two numbers in the set is also included in the set.
/// </summary>
public class Interval
{
    public double Lo { get; set; }
    public double Hi { get; set; }

    public Interval(double lo, double hi)
    {
        ValidateInterval(lo, hi);
        Lo = lo;
        Hi = hi;
    }

    public bool Intersects(double lo, double hi)
    {
        ValidateInterval(lo, hi);
        return Lo < hi && Hi > lo;
    }

    public override bool Equals(object obj)
    {
        var other = (Interval)obj;
        return Lo == other.Lo && Hi == other.Hi;
    }

    public override string ToString()
    {
        return $"({Lo}, {Hi})";
    }

    private static void ValidateInterval(double lo, double hi)
    {
        if (hi < lo)
        {
            throw new ArgumentException($"Low value {lo} cannot be higher than {hi}!");
        }
    }
}
