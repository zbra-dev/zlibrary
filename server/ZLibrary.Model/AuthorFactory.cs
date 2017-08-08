using System.Collections.Generic;

namespace ZLibrary.Model
{
    public class AuthorFactory
    {
        public IList<Author> CreateAuthors()
        {
            var authors = new List<Author>();
            authors.Add(new Author("Bruce Eckel"));
            authors.Add(new Author("Brett McLaughlin"));
            authors.Add(new Author("Scott W. Ambler "));
            authors.Add(new Author("Peter Eeles Kelli"));
            authors.Add(new Author("Kelli Houston"));
            authors.Add(new Author("Wojtek Kozaczynski"));
            authors.Add(new Author("Jaroslav Tulach"));
            authors.Add(new Author("Andrew Troelsen"));
            authors.Add(new Author("Chris Farrell"));
            authors.Add(new Author("Nick Harrison"));
            authors.Add(new Author("Simon Sarris"));
            authors.Add(new Author("Olivier Isnard"));
            authors.Add(new Author("Jack Phillips"));
            authors.Add(new Author("Erich Gamma"));
            authors.Add(new Author("Richard Helm"));
            authors.Add(new Author("Ralph Johnson"));
            authors.Add(new Author("Steve Fenton"));
            authors.Add(new Author("Mike Cohn"));
            authors.Add(new Author("Martin Fowler"));
            authors.Add(new Author("Sam Newman"));
            authors.Add(new Author("Karl Matthias"));
            authors.Add(new Author("Craig Skibo"));
            authors.Add(new Author("Marc Young"));
            authors.Add(new Author("Brian Johnson"));
            authors.Add(new Author("Khawar Zaman Ahmed "));
            authors.Add(new Author("Cary E. Umrysh"));
            authors.Add(new Author("Brett D. McLaughlin"));
            authors.Add(new Author("Gary Pollice"));
            authors.Add(new Author("David West"));
            authors.Add(new Author("Andrew Josey"));
            authors.Add(new Author("Kevin Mitnick"));
            authors.Add(new Author("Simon Franco"));
            authors.Add(new Author("Ben Mezrich"));
            authors.Add(new Author("Richard Monson"));
            authors.Add(new Author("Haefel"));
            authors.Add(new Author("Joel Spolsky"));
            authors.Add(new Author("Rafael C. Gonzalez"));
            authors.Add(new Author("Richard E. Woods"));
            authors.Add(new Author("Peter M. Senge"));
            authors.Add(new Author("Instituto Ethos"));
            authors.Add(new Author("Gustavo Cerbasi"));
            authors.Add(new Author("Dan Burke"));
            authors.Add(new Author("Frederick P. Brooks,JR"));
            authors.Add(new Author("Estado do Mundo"));
            authors.Add(new Author("Joshua Marinacci"));
            authors.Add(new Author("Chris Adamson"));
            authors.Add(new Author("Marcelo Piazza"));
            authors.Add(new Author("Walter Isaacson"));
            authors.Add(new Author("Eric Evans"));
            authors.Add(new Author("Maximiliano Firtman"));
            authors.Add(new Author("Alan Cooper"));
            authors.Add(new Author("Roger S. Pressman"));
            authors.Add(new Author("Ivar Jacobson"));
            authors.Add(new Author("Grady Booch"));
            authors.Add(new Author("James Rumbaugh"));
            authors.Add(new Author("Simon Haykin"));
            authors.Add(new Author("Jack Shirazi"));
            authors.Add(new Author("Dnny Ayers"));
            authors.Add(new Author("John Bell"));
            authors.Add(new Author("Carl Calvert"));
            authors.Add(new Author("Thomas Bishop"));
            authors.Add(new Author("Yedidyah Langsam"));
            authors.Add(new Author("Moshe J.Augenstein"));
            authors.Add(new Author("Steve Mc Connell"));
            authors.Add(new Author("Andrew Hunt"));
            authors.Add(new Author("David Thomas"));
            authors.Add(new Author("Joshua Bloch"));
            return authors;
        }
    }
}
