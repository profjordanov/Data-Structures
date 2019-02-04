using System;

public class Node : IComparable<Node>
{
    public Node(int row, int col)
    {
        Row = row;
        Col = col;
    }

    public int Row { get; set; }
    public int Col { get; set; }
    public int F { get; set; }

    public int CompareTo(Node other)
    {
        return F.CompareTo(other.F);
    }

    public override bool Equals(object obj)
    {
        var other = (Node)obj;
        return other != null && (Col == other.Col && Row == other.Row);
    }

    public override int GetHashCode()
    {
        var hash = 17;
        hash = 31 * hash + Row.GetHashCode();
        hash = 31 * hash + Col.GetHashCode();
        return hash;
    }

    public override string ToString()
    {
        return Row + " " + Col;
    }
}
