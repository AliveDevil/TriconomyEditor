using System;
using ProtoBuf.Meta;

namespace RuleSet
{
    public static class Extensions
    {
        public static void With<T>(this T _, Action<T> a)
        {
            a(_);
        }

        public static void With<TOut, T>(this TOut _, Action<T> a)
            where T : TOut
        {
            a((T)_);
        }

        public static void AddSubType(this MetaType model, Type type)
        {
            model.AddSubType(type.Name.GetHashCode() & 0x3FFF, type);
        }

        public static T WithRef<T>(this T _, Action<T> a)
        {
            _.With<T>(a);
            return _;
        }

        public static TOut WithRef<TOut, T>(this TOut _, Action<T> a)
            where T : TOut
        {
            _.With(a);
            return _;
        }
    }
}
