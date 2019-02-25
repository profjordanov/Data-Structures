using System;
using System.Collections.Generic;
using System.Linq;

public class PersonCollectionSlow : IPersonCollection
{
    // underlying data structures

	private List<Person> people = new List<Person>();

	// creates a new person and appends it to the underlying list
	public bool AddPerson(string email, string name, int age, string town)
    {
		var person = new Person(email, name, age, town);
		people.Add(person);
		return true;
    }

	public int Count => people.Count;

	public Person FindPerson(string email) =>
		people.FirstOrDefault(person => person.Email == email);

	public bool DeletePerson(string email) =>
		people.Remove(FindPerson(email));

	// finds persons by email domain
	public IEnumerable<Person> FindPersons(string emailDomain) =>
		people
			.Where(person => person.Email.EndsWith("@" + emailDomain))
			.OrderBy(person => person.Email);

	// finds persons by name and town
	public IEnumerable<Person> FindPersons(string name, string town) =>
		people
			.Where(person => person.Name == name &&
			                 person.Town == town)
			.OrderBy(person => person.Email);

	// finds by age range
	public IEnumerable<Person> FindPersons(int startAge, int endAge) =>
		people.Where(person => person.Age >= startAge &&
		                       person.Age <= endAge)
			.OrderBy(person => person.Age)
			.ThenBy(person => person.Email);

    public IEnumerable<Person> FindPersons(
        int startAge, 
        int endAge, 
        string town) =>
	    people.Where(person => (person.Age >= startAge &&
	                            person.Age <= endAge) &&
	                           person.Town == town)
		    .OrderBy(person => person.Age)
		    .ThenBy(person => person.Email);
}
