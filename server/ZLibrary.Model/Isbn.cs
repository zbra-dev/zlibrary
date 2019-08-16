using System;
using System.Linq;

namespace ZLibrary.Model
{
    public class Isbn
    {
        const int EvenWeight = 1;
        const int OddWeight = 3;
        const int OldInitialWeight = 10;
        const int IsbnModernLength = 13;
        const int IsbnOldLength = 10;
        const int IsbnOldDivisor = 11;
        const int IsbnModernDivisor = 10;

        public static Isbn FromString(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new IsbnException($"O parâmetro {nameof(value)} não pode ser nulo ou vazio.");
            }

            if (!CheckFormatString(value))
            {
                throw new IsbnException($"Formato inválido de ISBN: {value}");
            }

            if (!CheckValue(value))
            {
                throw new IsbnException($"ISBN Inválido: {value}");
            }

            return new Isbn(value);
        }

        private static bool CheckValue(string value)
        {
            var isbn = value.Select(s => int.Parse(s.ToString())).ToArray();

            if (isbn.Length.Equals(IsbnModernLength))
            {
                return ValidateModernIsbn(isbn);
            }
            else if (isbn.Length.Equals(IsbnOldLength))
            {
                return ValidateOldIsbn(isbn);
            }

            return false;
        }

        private static bool ValidateModernIsbn(int[] isbn)
        {
            var sum = 0;
            for (var i = 0; i < isbn.Length; i++)
            {
                if (i % 2 == 0)
                {
                    sum += isbn[i] * EvenWeight;
                }
                else
                {
                    sum += isbn[i] * OddWeight;
                }
            }

            return sum % IsbnModernDivisor == 0;
        }

        private static bool ValidateOldIsbn(int[] isbn)
        {
            var weight = OldInitialWeight;
            var sum = 0;

            for (var i = 0; i < isbn.Length; i++, weight--)
            {
                sum += isbn[i] * weight;
            }

            return sum % IsbnOldDivisor == 0;
        }

        private static bool CheckFormatString(string value)
        {
            return value.ToCharArray().All(c => Char.IsNumber(c));
        }

        public string Value { get; set; }
        public long Id { get; set; }

        protected Isbn()
        {
        }

        protected Isbn(string value)
        {
            Value = value;
        }
    }
}