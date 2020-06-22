using System.Text;

namespace HashSpeedTest
{
    // Public Domain hash as described here
    // https://en.wikipedia.org/wiki/Fowler%E2%80%93Noll%E2%80%93Vo_hash_function
    public static class Fnv1aHash
    {
        private static readonly long OffsetBasis64Bit = unchecked((long)0xcbf29ce484222325);
        private static readonly long Prime64Bit = 0x00000100000001B3;

        public static long Hash64(string value)
        {
            return Hash64(Encoding.UTF8.GetBytes(value));
        }

        public static long Hash64(params string[] values)
        {
            long hash = 0;
            for (int i = 0; i < values.Length; i++)
            {
                if (i == 0)
                {
                    hash = Hash64(values[i]);
                }
                else
                {
                    hash ^= Hash64(values[i]);
                }
            }

            return hash;
        }

        public static long Hash64(byte[] dataValues)
        {
            long hash = OffsetBasis64Bit;
            foreach (byte data in dataValues)
            {
                hash ^= data;
                hash *= Prime64Bit;
            }

            return hash;
        }
    }
}
