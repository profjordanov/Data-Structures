namespace QuadTree.Core
{
	/// <summary>
	/// Implemented by classes which can be stored in the <see cref="QuadTree{T}"/>.
	/// </summary>
	public interface IBoundable
	{
		Rectangle Bounds { get; set; }
	}
}
