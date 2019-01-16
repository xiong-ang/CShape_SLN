using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICloneableDemo
{
    class CloneHelper
    {
        public static IList<T> CloneList<T>(IList<T> original) where T : ICloneable
        {
            return original.Select(item => (T)item.Clone()).ToList();
        }

        public static Dictionary<TKey, TValue> CloneDictionary<TKey, TValue>(Dictionary<TKey, TValue> original) where TValue : ICloneable
        {
            Dictionary<TKey, TValue> ret = new Dictionary<TKey, TValue>(original.Count, original.Comparer);
            foreach (KeyValuePair<TKey, TValue> entry in original)
            {
                ret.Add(entry.Key, (TValue)entry.Value.Clone());
            }
            return ret;
        }
    }
}
