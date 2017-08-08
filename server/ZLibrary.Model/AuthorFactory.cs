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
            return authors;
        }
    }
}
