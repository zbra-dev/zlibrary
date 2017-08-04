using System;

namespace ZLibrary.Model
{

    public class Author
    {

        public string Name { get; set; }
        public long Id { get; set; }

        public Author()
        {

        }

        public Author(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(name));
            }
            Name = name;
        }

    }

}