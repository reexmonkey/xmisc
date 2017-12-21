using System;
using System.Collections.Generic;
using System.Text;

namespace reexmonkey.xmisc.core.text.extensions
{
    public static class FixtureExtensions
    {

        public static TSource[] Extract<TSource>(this TSource[]source, int offset, int count)
        {
            var result = new TSource[count];
            Buffer.BlockCopy(source, offset, result, 0, count);
            return result;
        }

        public static TSource[] Combine<TSource>(this TSource[]source, TSource[] other)
        {
            var total = source.Length + other.Length;
            var result = new TSource[total];
            Buffer.BlockCopy(source, 0, result, 0, source.Length);
            Buffer.BlockCopy(other, 0, result, source.Length, other.Length);
            return result;
        }
    }
}
