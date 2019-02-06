using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace SimpleTextEditor
{
	internal class Program
    {
	    private static void Main()
        {
	        var textEditor = new TextEditor();
	        var regex = new Regex("\"(.*)\"");
	        while (true)
	        {
				var line = Console.ReadLine();

				// stops the program
				if(line == "end")
				{
					break;
				}

				var match = regex.Match(line ?? throw new InvalidOperationException("Input cannot be null!"));
				string str;

				if (match.Success)
				{
					str = match.Groups[1].Value;
					line = line.Substring(0, line.IndexOf(str, StringComparison.Ordinal) - 1);
				}

				var commandArgs = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
				var command = commandArgs[ 0 ];
				switch (command)
				{
					// {command} {username} {params}
					case "login":
						ProcessLogin(textEditor,commandArgs);
						break;
					case "logout":
						ProcessLogout(textEditor, commandArgs);
						break;
					case "users":
						ProcessUsers(textEditor, commandArgs);
						break;
					default:
						break;
				}
			}
		}

	    private static void ProcessLogin(ITextEditor textEditor, IReadOnlyList<string> commandArgs)
	    {
		    if (textEditor == null)
		    {
			    throw new ArgumentNullException(nameof(textEditor));
		    }

		    if (commandArgs == null)
		    {
			    throw new ArgumentNullException(nameof(commandArgs));
		    }

		    if (commandArgs.Count <= 1)
		    {
			    return;
		    }

		    var username = commandArgs[ 1 ];
		    textEditor.Login(username);
	    }

	    private static void ProcessLogout(ITextEditor textEditor, IReadOnlyList<string> commandArgs)
	    {
		    if (textEditor == null)
		    {
			    throw new ArgumentNullException(nameof(textEditor));
		    }

		    if (commandArgs == null)
		    {
			    throw new ArgumentNullException(nameof(commandArgs));
		    }

		    if(commandArgs.Count <= 1)
		    {
			    return;
		    }

		    var username = commandArgs[ 1 ];
		    textEditor.Logout(username);
	    }

	    private static void ProcessUsers(ITextEditor textEditor, IReadOnlyList<string> commandArgs)
	    {
		    var prefix = commandArgs.Count > 1 ? commandArgs[ 1 ] : string.Empty;
		    var users = textEditor.Users(prefix);
		    foreach (var user in users)
		    {
			    Console.WriteLine(user);
		    }
	    }

	    private static void ProcessInsert(ITextEditor textEditor, string username, IReadOnlyList<string> commandArgs, string str)
	    {
		    if (textEditor == null)
		    {
			    throw new ArgumentNullException(nameof(textEditor));
		    }

		    if (username == null)
		    {
			    throw new ArgumentNullException(nameof(username));
		    }

		    if (commandArgs == null)
		    {
			    throw new ArgumentNullException(nameof(commandArgs));
		    }

		    if (str == null)
		    {
			    throw new ArgumentNullException(nameof(str));
		    }

		    if (commandArgs.Count <= 0)
		    {
			    return;
		    }

		    var index = int.Parse(commandArgs[ 0 ]);
		    textEditor.Insert(username, index, str);
	    }

	    private static void ProcessSubstring(ITextEditor textEditor, string username, IReadOnlyList<string> commandArgs)
	    {
		    if (textEditor == null)
		    {
			    throw new ArgumentNullException(nameof(textEditor));
		    }

		    if (username == null)
		    {
			    throw new ArgumentNullException(nameof(username));
		    }

		    if (commandArgs == null)
		    {
			    throw new ArgumentNullException(nameof(commandArgs));
		    }

		    if (commandArgs.Count <= 1)
		    {
			    return;
		    }

		    var index = int.Parse(commandArgs[ 0 ]);
		    var length = int.Parse(commandArgs[ 1 ]);
		    textEditor.Substring(username, index, length);
	    }

	    private static void ProcessDelete(ITextEditor textEditor, string username, IReadOnlyList<string> commandArgs)
	    {
		    if (textEditor == null)
		    {
			    throw new ArgumentNullException(nameof(textEditor));
		    }

		    if (username == null)
		    {
			    throw new ArgumentNullException(nameof(username));
		    }

		    if (commandArgs == null)
		    {
			    throw new ArgumentNullException(nameof(commandArgs));
		    }

		    if (commandArgs.Count <= 1)
		    {
			    return;
		    }

		    var index = int.Parse(commandArgs[ 0 ]);
		    var length = int.Parse(commandArgs[ 1 ]);
		    textEditor.Delete(username, index, length);
	    }
	}
}
