using System;

namespace RuleSet
{
    public static class Extensions
    {
        public static void With<T>(this T _, Action<T> a)
        {
            a(_);
        }

        public static T WithRef<T>(this T _, Action<T> a)
        {
            _.With(a);
            return _;
        }
    }
}
