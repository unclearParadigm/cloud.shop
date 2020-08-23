using System;
using System.Collections.Generic;
using System.Linq;

namespace CloudShop.BL.Extensions
{
    public static class LinqExtensions
    {
        private static readonly Random PseudoRandomNumberGenerator = new Random();

        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> list) {
            return list
                .OrderBy(e => Guid.NewGuid());
        }
    }
}