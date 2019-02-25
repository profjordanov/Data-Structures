using System.Collections.Generic;
using System.Linq;
using ShoppingCenterConsoleApp.Core;
using Wintellect.PowerCollections;

namespace ShoppingCenterConsoleApp.Business
{
	/// <summary>
	/// Data structure holding the <see cref="Product"/>.
	/// </summary>
	public class ShoppingCenter : IShoppingCenter
	{
		private readonly Dictionary<string, OrderedBag<Product>> _byProducer 
			= new Dictionary<string, OrderedBag<Product>>();

		private readonly Dictionary<string, OrderedBag<Product>> _byName 
			= new Dictionary<string, OrderedBag<Product>>();

		private readonly Dictionary<string, OrderedBag<Product>> _byNameAndProducer
			= new Dictionary<string, OrderedBag<Product>>();

		private readonly SortedDictionary<decimal, OrderedBag<Product>> _byPrice 
			= new SortedDictionary<decimal, OrderedBag<Product>>();

		/// <summary>
		/// Adds a product by given name, price and producer.
		/// If a product with the same name / producer/ price already exists,
		/// the newly added product does not affect the existing ones (duplicates are allowed). 
		/// </summary>
		/// <param name="product"></param>
		public void AddProduct(Product product)
		{
			if (product == null)
			{
				return;
			}

			AddToDictionary(_byProducer, product.Producer, product);
			AddToDictionary(_byName, product.Name, product);
			AddToDictionary(_byNameAndProducer, GetNameProducer(product.Name,product.Producer), product);
			AddToDictionary(_byPrice, product.Price, product);
		}

		/// <summary>
		/// Deletes all products matching given producer. 
		/// </summary>
		/// <param name="producer"></param>
		/// <returns>Count of deleted items.</returns>
		public int DeleteProductsByProducer(string producer)
		{
			if (!_byProducer.ContainsKey(producer))
			{
				return 0;
			}

			var count = 0;
			_byProducer[producer]
				.ToList()
				.ForEach(product =>
				{
					_byProducer[producer].Remove(product);
					_byName[product.Name].Remove(product);
					_byNameAndProducer[GetNameProducer(product.Name, producer)].Remove(product);
					_byPrice[product.Price].Remove(product);
					count++;
				});

			return count;
		}

		/// <summary>
		/// Deletes all products matching given product name and producer. 
		/// </summary>
		/// <param name="name"></param>
		/// <param name="producer"></param>
		/// <returns></returns>
		public int DeleteProductsByNameAndProducer(string name, string producer)
		{
			var nameProducer = GetNameProducer(name, producer);
			if(!_byNameAndProducer.ContainsKey(nameProducer))
			{
				return 0;
			}

			var count = 0;
			_byNameAndProducer[ nameProducer ]
				.ToList()
				.ForEach(p =>
				{
					_byName[ name ].Remove(p);
					_byProducer[ producer ].Remove(p);
					_byNameAndProducer[ nameProducer ].Remove(p);
					_byPrice[ p.Price ].Remove(p);
					count++;
				});

			return count;
		}

		/// <param name="name"></param>
		/// <returns>All products by given product name.</returns>
		public IEnumerable<Product> FindProductsByName(string name) =>
			!string.IsNullOrEmpty(name) && _byName.ContainsKey(name)
				? _byName[name].ToList()
				: new List<Product>();

		/// <param name="producer"></param>
		/// <returns>All products by given producer.</returns>
		public IEnumerable<Product> FindProductsByProducer(string producer) =>
			!string.IsNullOrEmpty(producer) && _byProducer.ContainsKey(producer)
				? _byProducer[producer].ToList()
				: new List<Product>();

		/// <param name="startPrice"></param>
		/// <param name="endPrice"></param>
		/// <returns>All products whose price is greater or equal than fromPrice and less or equal than toPrice</returns>
		public IEnumerable<Product> FindProductsByPriceRange(decimal startPrice, decimal endPrice)
		{
			var result = new OrderedBag<Product>();

			_byPrice
				.Keys
				.Where(price => startPrice <= price && price <= endPrice)
				.ToList()
				.ForEach(price => result.AddMany(_byPrice[ price ]));

			return result;
		}

		// Helper methods
		private static void AddToDictionary<T>(IDictionary<T, OrderedBag<Product>> dictionary, T key, Product product)
		{
			if (!dictionary.ContainsKey(key))
			{
				dictionary[key] = new OrderedBag<Product>();
			}

			dictionary[key].Add(product);
		}

		private static string GetNameProducer(string name, string producer) => $"{name}&&{producer}";
	}
}