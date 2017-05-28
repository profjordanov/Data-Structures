public class Program
{
    public static void Main(string[] args)
    {
        ArrayList<int> list = new ArrayList<int>();
        list.Add(5);
        list[0] = list[0] + 1;
        int element = list.RemoveAt(0);
    }
}
