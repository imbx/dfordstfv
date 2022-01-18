
using System.Collections.Generic;
using System.Collections;
using System;

namespace BoxScripts
{
    public class Dictionary<TKey1,TKey2,TValue> :  Dictionary<Tuple<TKey1, TKey2>, TValue>, IDictionary<Tuple<TKey1, TKey2>, TValue>
    {

        public TValue this[TKey1 key1, TKey2 key2]
        {
            get { return base[Tuple.Create(key1, key2)]; }
            set { base[Tuple.Create(key1, key2)] = value; }
        }

        public void Add(TKey1 key1, TKey2 key2, TValue value)
        {
            base.Add(Tuple.Create(key1, key2), value);
        }

        public bool ContainsKey(TKey1 key1, TKey2 key2)
        {
            return base.ContainsKey(Tuple.Create(key1, key2));
        }
    }

    public struct MultiKeyArr<T1, T2>
    {
        public readonly T1 Item1;
        public readonly T2 Item2;

        public MultiKeyArr(T1 item1, T2 item2) { Item1 = item1; Item2 = item2; }
    }
    
    public static class MultiKeyArr
    {
        public static MultiKeyArr<T1, T2> Create<T1, T2> (T1 item1, T2 item2)
        {
            return new MultiKeyArr<T1, T2> (item1, item2);
        }
    }
}
