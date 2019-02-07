using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleTextEditor
{
	/// <summary>
	/// Implements a text editor that will be used by multiple users at a time.
	/// Each user will have its own text, which he should be able to edit.
	/// Also, we should be able to see all of the users that are using our application.
	/// </summary>
	public class TextEditor : ITextEditor
	{
		private readonly IDictionary<string, StringBuilder> _usersCurrent = new Dictionary<string, StringBuilder>();
		private readonly IDictionary<string, Stack<StringBuilder>> _usersCache = new Dictionary<string, Stack<StringBuilder>>();

		/// <summary>
		/// Given user can start executing commands.
		/// </summary>
		/// <param name="username"></param>
		public void Login(string username)
		{
			if (!IsValid(username))
			{
				return;
			}

			UpdateCurrent(username, new StringBuilder());
			_usersCache[username] = new Stack<StringBuilder>();
		}

		/// <summary>
		/// Given user can no longer edit his string.
		/// </summary>
		/// <param name="username"></param>
		public void Logout(string username)
		{
			if (!IsLoggedIn(username))
			{
				return;
			}

			RemoveCurrent(username);
			_usersCache.Remove(username);
		}

		/// <summary>
		/// Inserts the given string in the beginning of the string.
		/// </summary>
		/// <param name="username"></param>
		/// <param name="str"></param>
		public void Prepend(string username, string str)
		{
			if (!IsLoggedIn(username))
			{
				return;
			}

			Cache(username);
			GetCurrent(username).Insert(0, str);
		}

		/// <summary>
		/// Inserts the given string in the given position.
		/// </summary>
		/// <param name="username"></param>
		/// <param name="index"></param>
		/// <param name="str"></param>
		public void Insert(string username, int index, string str)
		{
			if (IsLoggedIn(username))
			{
				return;
			}

			Cache(username);
			GetCurrent(username).Insert(index, str);
		}

		/// <summary>
		/// Replaces the user string with a substring from it.
		/// </summary>
		/// <param name="username"></param>
		/// <param name="startIndex"></param>
		/// <param name="length"></param>
		public void Substring(string username, int startIndex, int length)
		{
			if(IsLoggedIn(username))
			{
				return;
			}

			var current = GetCurrent(username);
			var substring = GetSubstring(current, startIndex, length);
			UpdateCurrent(username, substring);
		}

		/// <summary>
		/// Removes part of the user string.
		/// </summary>
		/// <param name="username"></param>
		/// <param name="startIndex"></param>
		/// <param name="length"></param>
		public void Delete(string username, int startIndex, int length)
		{
			if(IsLoggedIn(username))
			{
				return;
			}
			Cache(username);
			GetCurrent(username).Insert(startIndex, length);
		}

		/// <summary>
		/// Deletes the user string.
		/// </summary>
		/// <param name="username"></param>
		public void Clear(string username)
		{
			if(IsLoggedIn(username))
			{
				return;
			}
			Cache(username);
			GetCurrent(username).Clear();
		}

		/// <returns>The length of the user string</returns>
		/// <param name="username"></param>
		public int Length(string username) =>
			IsLoggedIn(username) ? GetCurrent(username).Length : 0;

		/// <returns>The user string</returns>
		/// <param name="username"></param>
		public string Print(string username) =>
			IsLoggedIn(username) ? GetCurrent(username).ToString() : string.Empty;

		/// <summary>
		/// Reverts the last operations on the user string.
		/// Can be used multiple times.
		/// </summary>
		/// <param name="username"></param>
		public void Undo(string username)
		{
			if(IsLoggedIn(username))
			{
				return;
			}

			var cacheStack = _usersCache[username];
			if (!cacheStack.Any())
			{
				return;
			}
			var cache = cacheStack.Pop();
			UpdateCurrent(username, cache);
		}

		/// <returns>All users currently logged in.</returns>
		/// <param name="prefix">Users starting with the given prefix.</param>
		public IEnumerable<string> Users(string prefix = "") =>
			prefix == string.Empty 
				? _usersCache.Keys 
				: _usersCache.Keys.Where(usr => usr.StartsWith(prefix));

		// Helper Methods
		private static bool IsValid(string username) => !string.IsNullOrWhiteSpace(username);

		private void UpdateCurrent(string username, StringBuilder value) => _usersCurrent[username] = value;

		private bool CurrentContains(string username) => _usersCurrent.ContainsKey(username);

		private bool IsLoggedIn(string username) =>
			_usersCache.ContainsKey(username) &&
			CurrentContains(username);

		private void RemoveCurrent(string username) => _usersCurrent.Remove(username);

		private StringBuilder GetCurrent(string username) => _usersCurrent[username];

		private void Cache(string username)
		{
			var current = GetCurrent(username);
			var cache = new StringBuilder().Append(current);
			_usersCache[username].Push(cache);
		}

		private static StringBuilder GetSubstring(StringBuilder current, int startIndex, int length)
		{
			var builder = new StringBuilder();

			for (var i = startIndex; i < length; i++)
			{
				builder.Append(current[i]);
			}

			return builder;
		}
	}
}