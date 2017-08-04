using System;

namespace ZLibrary.Model
{

    public class Publisher
    {

        public string Name { get; set; }

        public long Id { get; set; }

        public Publisher()
        {

        }
        public Publisher(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(name));
            }
            Name = name;
        }

    }

}