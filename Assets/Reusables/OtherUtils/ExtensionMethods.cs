using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InApp.Utils
{
    public static class ExtensionMethods
    {
        #region Referances
        private static System.Random rng = new System.Random();
        #endregion

        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;

            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        public static T GetRandomEnum<T>()
        {
            System.Array A = System.Enum.GetValues(typeof(T));
            T V = (T)A.GetValue(Random.Range(0, A.Length));
            return V;
        }

        public static List<T> GetClone<T>(this List<T> source)
        {
            return source.GetRange(0, source.Count);
        }

    }
}