using System;
using System.Collections.Generic;

namespace ZLibrary.Model
{
    public class Author
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public List<BookAuthor> Books { get; set; }

        public Author()
        {
        }

        public Author(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException($"O parâmetro {nameof(name)} não pode ser nulo ou vazio.");
            }
            Name = name;
        }
    }
}