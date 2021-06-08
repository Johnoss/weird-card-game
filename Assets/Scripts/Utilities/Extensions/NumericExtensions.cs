using System;

namespace Utilities.Extensions
{
    public static class NumericExtensions
    {
        public static bool IsWithinRange<T>(this T number, float min, float max) where T : IComparable
        {
            return number.CompareTo(min) >= 0 && number.CompareTo(max) <= 0;
        }

        public static bool IsGreaterThan<T>(this T value, T other) where T : IComparable
        {
            return value.CompareTo(other) > 0;
        }

        public static bool IsLessThan<T>(this T value, T other) where T : IComparable
        {
            return value.CompareTo(other) < 0;
        }
    }
}