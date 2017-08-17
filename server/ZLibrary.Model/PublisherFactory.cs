using System.Collections.Generic;

namespace ZLibrary.Model
{
    public class PublisherFactory
    {
        public static IList<Publisher> CreatePublishers()
        {
            var publishers = new List<Publisher>();
            publishers.Add(new Publisher("Prentice Hall"));
            publishers.Add(new Publisher("O`Reilly"));
            publishers.Add(new Publisher("Cambridge "));
            publishers.Add(new Publisher("Addison - Wesley"));
            publishers.Add(new Publisher("Apress"));
            publishers.Add(new Publisher("Chris Massey"));
            publishers.Add(new Publisher("SAMS"));
            publishers.Add(new Publisher("OCDE"));
            publishers.Add(new Publisher("Mc Graw Hill"));
            publishers.Add(new Publisher("Microsoft"));
            publishers.Add(new Publisher("PH PTR"));
            publishers.Add(new Publisher("VHP"));
            publishers.Add(new Publisher("Little Brown"));
            publishers.Add(new Publisher("Editora Futura"));
            publishers.Add(new Publisher("Intrínseca"));
            publishers.Add(new Publisher("Campus"));
            publishers.Add(new Publisher("Prantice Hall"));
            publishers.Add(new Publisher("Editora Best Seller"));
            publishers.Add(new Publisher("Instituto Ethos"));
            publishers.Add(new Publisher("Random House"));
            publishers.Add(new Publisher("Thomas Nelson Brasil"));
            publishers.Add(new Publisher("Perseus Publishing"));
            publishers.Add(new Publisher("Uma Editora "));
            publishers.Add(new Publisher("Editora Saraiva"));
            publishers.Add(new Publisher("Simon 7 Schuster "));
            publishers.Add(new Publisher("Wiley"));
            publishers.Add(new Publisher("Makron Books"));
            publishers.Add(new Publisher("WROK"));
            publishers.Add(new Publisher("ITRev"));
            return publishers;
        }
    }
}