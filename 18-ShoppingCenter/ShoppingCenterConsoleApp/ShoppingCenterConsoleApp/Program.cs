using System;
using System.Collections.Generic;
using System.Linq;
using ShoppingCenterConsoleApp.Business;
using ShoppingCenterConsoleApp.Core;

namespace ShoppingCenterConsoleApp
{
	internal class Program
    {
	    private static void Main()
        {
	        var shoppingCenter = new ShoppingCenter();
	        var linesCount = int.Parse(Console.ReadLine());
	        for (var i = 0; i < linesCount; i++)
	        {
				var line = Console.ReadLine(); // {command} {arg1};{arg2};{...}
				var tokens = line?.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

				if(tokens?.Length < 2) // {command} {args}
				{
					continue;
				}

				var command = tokens[ 0 ];
				var args = GetCommandArgs(line);

				switch(command)
				{
					case "AddProduct":
						AddProduct(shoppingCenter, args);
						break;
					case "DeleteProducts":
						DeleteProducts(shoppingCenter, args);
						break;
					case "FindProductsByName":
						FindProductsByName(shoppingCenter, args);
						break;
					case "FindProductsByProducer":
						FindProductsByProducer(shoppingCenter, args);
						break;
					case "FindProductsByPriceRange":
						FindProductsByPriceRange(shoppingCenter, args);
						break;
				}
			}
		}

		// Commands
	    private static void FindProductsByPriceRange(IShoppingCenter shoppingCenter, IReadOnlyList<string> args)
	    {
		    if (args.Count < 2)
		    {
			    return;
		    }

			var startPrice = decimal.Parse(args[ 0 ]);
			var endPrice = decimal.Parse(args[ 1 ]);
			var products = shoppingCenter.FindProductsByPriceRange(startPrice, endPrice);
			PrintSearchResult(products.ToArray());
		}

	    private static void FindProductsByProducer(IShoppingCenter shoppingCenter, IReadOnlyList<string> args)
	    {
		    if (!args.Any())
		    {
			    return;
		    }

			var result = shoppingCenter.FindProductsByProducer(args[ 0 ]);
			PrintSearchResult(result.ToArray());
		}

	    private static void FindProductsByName(IShoppingCenter shoppingCenter, IReadOnlyList<string> args)
	    {
		    if (!args.Any())
		    {
			    return;
		    }

		    var result = shoppingCenter.FindProductsByName(args[ 0 ]);
		    PrintSearchResult(result.ToArray());
		}

	    private static void DeleteProducts(IShoppingCenter shoppingCenter, IReadOnlyList<string> args)
	    {
		    if (!args.Any())
		    {
			    return;
		    }

		    var count = 0;
		    if(args.Count > 1)
		    {
			    var name = args[ 0 ];
			    var producer = args[ 1 ];
			    count = shoppingCenter.DeleteProductsByNameAndProducer(name, producer);
		    }
		    else
		    {
			    count = shoppingCenter.DeleteProductsByProducer(args[ 0 ]);
		    }

		    PrintDeletionResult(count);
		}

	    private static void AddProduct(IShoppingCenter shoppingCenter, IReadOnlyList<string> args)
	    {
		    if(args.Count < 3)
		    {
			    return;
		    }

		    var name = args[ 0 ];
		    var price = decimal.Parse(args[ 1 ]);
		    var producer = args[ 2 ];
		    var product = new Product(name, price, producer);
		    shoppingCenter.AddProduct(product);
		    Console.WriteLine("Product added");
	    }

		// Helper methods
		private static string[] GetCommandArgs(string line)
	    {
			var args = line.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
			// Strip command from arg1: {command} {arg1} => {arg1}
			var firstArg = args[ 0 ];
			args[ 0 ] = firstArg.Substring(firstArg.IndexOf(' ') + 1).TrimStart();
			return args;
		}

		private static void PrintDeletionResult(int count) =>
			Console.WriteLine(count != 0
				? $"{count} products deleted"
				: "No products found");

		private static void PrintSearchResult(IReadOnlyCollection<Product> products)
			=> Console.WriteLine(products.Any()
				? string.Join(Environment.NewLine, products)
				: "No products found");
	}
}
