namespace Lists
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var list = new ArrayList<int>();
			list.Add(5);
			list[0] = list[0] + 1;
			var element = list.RemoveAt(0);
		}
	}
}
