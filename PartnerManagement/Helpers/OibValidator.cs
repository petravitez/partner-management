using System.Text.RegularExpressions;

namespace PartnerManagement.Helpers
{
    public static class OibValidator
    {
        public static bool IsValid(string? oib)
        {
            if (string.IsNullOrWhiteSpace(oib) || !Regex.IsMatch(oib, @"^\d{11}$"))
                return false;

            int remainder = 10;
            for (int i = 0; i < 10; i++)
            {
                int digit = oib[i] - '0';
                remainder = (remainder + digit) % 10;
                if (remainder == 0) remainder = 10;
                remainder *= 2;
                remainder %= 11;
            }

            int controlDigit = (11 - remainder) % 10;
            return controlDigit == (oib[10] - '0');
        }
    }

}
