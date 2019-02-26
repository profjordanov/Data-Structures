using System.Collections.Generic;
using ShoppingCenterConsoleApp.Business;

namespace ShoppingCenterConsoleApp.Core
{
	public interface IShoppingCenter
	{
		void AddProduct(Product product);

		int DeleteProductsByProducer(string producer);

		int DeleteProductsByNameAndProducer(string name, string producer);

		IEnumerable<Product> FindProductsByName(string name);

		IEnumerable<Product> FindProductsByProducer(string producer);

		IEnumerable<Product> FindProductsByPriceRange(decimal startPrice, decimal endPrice);
	}
}