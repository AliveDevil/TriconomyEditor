using System;

namespace RuleSetEditor
{
    public delegate void RefAction<T>(ref T obj);

    public static class Extensions
    {
        public static T _<T>(this T t, Action<T> a)
        {
            a(t);
            return t;
        }

        public static T _<T>(this T t, RefAction<T> a)
        {
            a(ref t);
            return t;
        }
    }
}
