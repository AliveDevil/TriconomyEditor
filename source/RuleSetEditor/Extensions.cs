using System;

namespace RuleSetEditor
{
    public delegate void RefAction<T>(ref T obj);

    public static class Extensions
    {
        public static T _<T>(this T t, Action<T> a)
        {
            a?.Invoke(t);
            return t;
        }

        public static T _<T>(this T t, RefAction<T> a)
        {
            a?.Invoke(ref t);
            return t;
        }
    }
}
