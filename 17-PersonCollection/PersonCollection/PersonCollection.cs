using System.Collections.Generic;
using System.Linq;

public class PersonCollection : IPersonCollection
{
	// underlying data structures
	// O(1)
	private readonly Dictionary<string, Person> _personsByEmail = new Dictionary<string, Person>();
	private readonly Dictionary<string, SortedSet<Person>> _personsByEmailDomain = new Dictionary<string, SortedSet<Person>>();
	private readonly Dictionary<string, SortedSet<Person>> _personsByNameAndTown = new Dictionary<string, SortedSet<Person>>();
	//// O(log n)
	private readonly SortedDictionary<int, SortedSet<Person>> _personsByAge = new SortedDictionary<int, SortedSet<Person>>();
	private readonly Dictionary<string, SortedDictionary<int, SortedSet<Person>>> _personsByTownByAge 
		= new Dictionary<string, SortedDictionary<int, SortedSet<Person>>>();

	public int Count => _personsByEmail.Count;

    public bool AddPerson(string email, string name, int age, string town)
    {
	    if (_personsByEmail.ContainsKey(email))
	    {
		    return false;
	    }

		var person = new Person(email, name, age, town);

		_personsByEmail[email] = person;
		AddByEmailDomain(person);
		AddByNameAndTown(person);
		AddByAge(person);
		AddByTownAndAge(person);

		return true;
    }

    public Person FindPerson(string email) =>
	    email != null && _personsByEmail.ContainsKey(email)
		    ? _personsByEmail[email]
		    : null;

    public bool DeletePerson(string email)
    {
	    var person = FindPerson(email);
	    if (person == null)
	    {
		    return false;
	    }

	    _personsByEmail.Remove(email);
	    RemoveByEmailDomain(person);
	    RemoveByNameAndTown(person);
	    RemoveByAge(person);
	    RemoveByTownAndAge(person);

	    return true;
	}

    public IEnumerable<Person> FindPersons(string emailDomain) =>
	    emailDomain != null &&
	    _personsByEmailDomain.ContainsKey(emailDomain)
		    ? new List<Person>(_personsByEmailDomain[emailDomain])
		    : new List<Person>();

    public IEnumerable<Person> FindPersons(string name, string town)
    {
	    var nameTown = GetNameTown(name, town);
	    return nameTown != null &&
	           _personsByNameAndTown.ContainsKey(nameTown)
		    ? new List<Person>(_personsByNameAndTown[nameTown])
		    : new List<Person>();
    }

    public IEnumerable<Person> FindPersons(int startAge, int endAge)
    {
		var result = new List<Person>();

		_personsByAge
			.Keys
			.Where(age => startAge <= age &&
			              age <= endAge)
			.ToList()
			.ForEach(age => result.AddRange(_personsByAge[age]));

		return result.AsReadOnly();
    }

    public IEnumerable<Person> FindPersons(
        int startAge, int endAge, string town)
    {
		var result = new List<Person>();

		if (town == null || 
		    !_personsByTownByAge.ContainsKey(town))
		{
			return result;
		}

		_personsByTownByAge[town]
			.Keys
			.Where(age => startAge <= age &&
			              age <= endAge)
			.ToList()
			.ForEach(age => result.AddRange(_personsByTownByAge[town][age]));

		return result.AsReadOnly();
    }

	// Helper methods

	private void AddByTownAndAge(Person person)
	{
		var town = person.Town;
		var age = person.Age;
		if (town == null)
		{
			return;
		}

		if (!_personsByTownByAge.ContainsKey(town))
		{
			_personsByTownByAge[town] = new SortedDictionary<int, SortedSet<Person>>();
		}

		if (!_personsByTownByAge[town].ContainsKey(age))
		{
			_personsByTownByAge[town][age] = new SortedSet<Person>();
		}

		_personsByTownByAge[town][age].Add(person);
	}

	private void AddByAge(Person person)
	{
		if (!_personsByAge.ContainsKey(person.Age))
		{
			_personsByAge[person.Age] = new SortedSet<Person>();
		}

		_personsByAge[person.Age].Add(person);
	}

	private void AddByNameAndTown(Person person)
	{
		var nameTown = GetNameTown(person.Name, person.Town);
		if (nameTown == null)
		{
			return;
		}

		if (_personsByNameAndTown.ContainsKey(nameTown))
		{
			_personsByNameAndTown[nameTown] = new SortedSet<Person>();
		}

		_personsByNameAndTown[nameTown].Add(person);
	}

	private static string GetNameTown(string name, string town) => $"{name}&{town}";


	private void AddByEmailDomain(Person person)
	{
		var emailDomain = GetEmailDomain(person.Email);
		if (emailDomain == null)
		{
			return;
		}

		if (!_personsByEmailDomain.ContainsKey(emailDomain))
		{
			_personsByEmailDomain[emailDomain] = new SortedSet<Person>();
		}

		_personsByEmailDomain[emailDomain].Add(person);
	}

	private static string GetEmailDomain(string email)
	{
		var tokens = email.Split('@');
		return tokens.Length > 1 ? tokens[ 1 ] : null;
	}

	private void RemoveByTownAndAge(Person person)
	{
		var town = person.Town;
		var age = person.Age;

		if (town != null &&
		    _personsByTownByAge.ContainsKey(town) &&
		    _personsByTownByAge[town].ContainsKey(age))
		{
			_personsByTownByAge[town][age].Remove(person);
		}
	}

	private void RemoveByAge(Person person)
	{
		var age = person.Age;
		if(_personsByAge.ContainsKey(age))
		{
			_personsByAge[ age ].Remove(person);
		}
	}

	private void RemoveByNameAndTown(Person person)
	{
		var nameTown = GetNameTown(person.Name, person.Town);
		if (nameTown != null &&
		    _personsByNameAndTown.ContainsKey(nameTown))
		{
			_personsByNameAndTown[nameTown].Remove(person);
		}
	}

	private void RemoveByEmailDomain(Person person)
	{
		var emailDomain = GetEmailDomain(person.Email);
		if(emailDomain != null &&
		   _personsByEmailDomain.ContainsKey(emailDomain))
		{
			_personsByEmailDomain[ emailDomain ].Remove(person);
		}
	}

}
