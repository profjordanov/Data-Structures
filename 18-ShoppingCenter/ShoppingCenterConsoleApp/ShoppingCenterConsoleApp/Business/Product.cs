using System;
using ShoppingCenterConsoleApp.Core;

namespace ShoppingCenterConsoleApp.Business
{
    public class Product : IProduct, IComparable<Product>
    {
	    public Product(string name, decimal price, string producer)
	    {
		    Name = name;
		    Price = price;
		    Producer = producer;
	    }

		public string Name { get; }
	    public decimal Price { get; }
	    public string Producer { get; }

	    public int CompareTo(Product other)
	    {
		    var compare = string.Compare(Name, other.Name, StringComparison.Ordinal);
		    if (compare != 0)
		    {
			    return compare;
		    }
		    compare = string.Compare(Producer, other.Producer, StringComparison.Ordinal);
		    if (compare == 0)
		    {
			    compare = Price.CompareTo(other.Price);
		    }

		    return compare;
	    }

	    public override string ToString()
		    => "{" + $"{Name};{Producer};{Price:F2}" + "}";
	}
}
