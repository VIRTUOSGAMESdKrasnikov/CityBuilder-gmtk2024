using System;
using System.Collections.Generic;
using System.Linq;

namespace CityBuilder.Utils
{
    public static class EnumerableExtensions
    {
        public static T Random<T>(this IEnumerable<T> collection)
        {
            int count = collection.Count();
            var random = new Random();

            int randomIndex = random.Next(0, count);

            int i = 0;
            foreach (T element in collection)
            {
                if (i == randomIndex)
                {
                    return element;
                }

                i++;
            }

            throw new ArgumentOutOfRangeException("randomIndex", "random index greater than collection size");
        }
    }
}