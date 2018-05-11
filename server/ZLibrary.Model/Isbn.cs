using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace ZLibrary.Model
{
    public class Isbn
    {
        public static Isbn FromValue(string value) 
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
                throw new IsbnException($"ISBN-13 Inválido: {value}");
            }

            return new Isbn(value);
        }

        private static bool CheckValue(string value) 
        {
            const int WeightType1 = 1;
            const int WeightType2 = 3;

            var isbn = value.Select(s => int.Parse(s.ToString())).ToArray();
            var productor = new int[isbn.Length];
            for (int i = 0; i < isbn.Length; i++)
            {
                if (i % 2 == 0)
                {
                    productor[i] = isbn[i] * WeightType1;
                }
                else
                {
                    productor[i] = isbn[i] * WeightType2;
                }
            }

            return productor.Sum() % 10 == 0;   
        }

        public static bool CheckFormatString(string value)
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