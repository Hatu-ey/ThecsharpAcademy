namespace MathGame.Classes
{
    internal static class GameHelper
    {

        private static readonly Random rng = new();
        /// <summary>
        /// Generate divident and divisor that result in integers only 
        /// </summary>
        /// <param name="maxValue"></param>
        /// <returns>Touple</returns>
        public static (int, int) GenerateDivisionNumbers(int maxValue)
        {
            int dividend, divisor;
            dividend = rng.Next(0, maxValue);

            divisor = GetDivisor(dividend);

            return (dividend, divisor);
        }

        /// <summary>
        /// get non negative divisor, when dividend equals 0, return random number from 1-100
        /// </summary>
        /// <param name="dividend"></param>
        /// <returns>int</returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static int GetDivisor(int dividend)
        {
            if (dividend < 0)
            {
                throw new ArgumentOutOfRangeException("Dividend must be non negative");
            }

            if (dividend == 0)
            {
                return rng.Next(1, 101);
            }

            List<int> divisors = [1, dividend];
            int numberLength = (int)Math.Sqrt(dividend) + 1;

            for (int i = 2; i < numberLength; i++)
            {
                if (dividend % i == 0)
                {
                    divisors.Add(dividend / i);
                    if (i != dividend / i)
                    {
                        divisors.Add(dividend / i);
                    }
                }
            }

            return divisors[rng.Next(divisors.Count())];
        }
    }
}
