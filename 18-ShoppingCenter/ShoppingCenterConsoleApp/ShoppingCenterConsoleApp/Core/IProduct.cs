namespace ShoppingCenterConsoleApp.Core
{
	public interface IProduct
	{
		string Name { get; }

		decimal Price { get; }

		string Producer { get; }
	}
}
