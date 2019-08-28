using System;
using System.Linq;
using ZLibrary.Model;

namespace ZLibrary.Web.Validators
{
    public class IsbnValidator : IValidator<string>
    {
        const int EvenWeight = 1;
        const int OddWeight = 3;
        const int OldInitialWeight = 10;
        const int IsbnModernLength = 13;
        const int IsbnOldLength = 10;
        const int IsbnOldDivisor = 11;
        const int IsbnModernDivisor = 10;

        public ValidationResult Validate(string value)
        {
            var result = new ValidationResult();

            if (string.IsNullOrWhiteSpace(value))
            {
                result.ErrorMessage = "ISBN obrigatório";
                return result;
            }

            if (!CheckFormatString(value))
            {
                result.ErrorMessage = "ISBN deve conter somente números";
                return result;
            }

            if (!CheckValue(value))
            {
                result.ErrorMessage = "ISBN inválido";
                return result;
            }

            return result;
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

    }
}