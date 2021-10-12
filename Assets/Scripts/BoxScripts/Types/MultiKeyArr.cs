
namespace BoxScripts
{
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
