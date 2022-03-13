using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

namespace Albert.Common
{
    public static class CommonUtils
    {
        [UsedImplicitly]
        public static void Deconstruct<TKey, TVal>(this KeyValuePair<TKey, TVal> kvp, out TKey key, out TVal val)
        {
            key = kvp.Key;
            val = kvp.Value;
        }

        /// <summary>
        /// Identity function. Returns input as-is.
        /// </summary>
        public static T Identity<T>(T x) => x;

        /// <summary>
        /// Orders <paramref name="source"/> using an identity function.
        /// </summary>
        public static IEnumerable<T> OrderByIdentity<T>(this IEnumerable<T> source) => source.OrderBy(Identity);

        /// <summary>
        /// Returns the portion the <paramref name="source"/> before the last occurence of <paramref name="phrase"/>.
        /// </summary>
        public static string BeforeLast(this string source, string phrase)
            => source.Substring(0, source.LastIndexOf(phrase, StringComparison.Ordinal));

        /// <summary>
        /// For <paramref name="source"/> of <c>"a.b.c"</c> and <paramref name="separator"/> of <c>"."</c> returns:<br/>
        /// - <c>"a.b.c"</c><br/>
        /// - <c>"a.b"</c><br/>
        /// - <c>"a"</c>
        /// </summary>
        public static IEnumerable<string> PeelRight(this string source, string separator)
        {
            while (true)
            {
                yield return source;

                var lastSepInd = source.LastIndexOf(separator, StringComparison.Ordinal);

                if (lastSepInd == -1)
                {
                    yield break;
                }

                source = source.BeforeLast(separator);
            }
        }

        /// <summary>
        /// Returns a new string that is <paramref name="source"/> repeated <paramref name="count"/> times.
        /// </summary>
        public static string Repeat(this string source, int count) => string.Concat(Enumerable.Repeat(source, count));

        public static bool IsPowerOfTwo(this int num)
        {
            return num != 0 && (num & (num - 1)) == 0;
        }
    }
}
