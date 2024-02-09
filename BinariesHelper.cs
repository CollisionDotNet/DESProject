namespace DESProject
{
    public static class BinariesHelper
    {
        // Returns power of two using binary shifts for better performance
        public static ulong GetPowerOfTwo(int power)
        {
            ulong res = 1;
            res <<= power;
            return res;
        }
        // Returns bit of 64-bit number by its index and total bits amount
        public static int GetBitFromBinary(ulong binary, int index, int bitsCount) => (int)((binary >> (bitsCount - index)) & 1);
        // Returns <count> lower bits of a number
        public static ulong GetLowerBits(ulong binary, int count) => binary & (GetPowerOfTwo(count) - 1);
        // Permutates number according to permutation table, first bit become table[0]th bit and so on. 
        public static ulong ApplyPermutation(ulong toPermutate, int[] table, int toPermutateBitsCount)
        {
            ulong result = 0;
            int weight = 0;
            // Permutation applies from end to start
            for (int i = table.Length - 1; i >= 0; i--) 
            {
                // Getting bit number i
                int toPermutateCurrentBit = GetBitFromBinary(toPermutate, table[i], toPermutateBitsCount); 
                if (toPermutateCurrentBit == 1)
                {
                    // If it is 1 - put it on a new position by adding power of two
                    result += GetPowerOfTwo(weight); 
                }
                weight++;
            }
            return result;
        }
        // Returns 64-bit number from byte array with 8 elements
        public static ulong GetUInt64FromBytes(byte[] bytes)
        {
            if (bytes.Length != 8)
                throw new ArgumentException();
            ulong res = 0;
            ulong weight = 1;
            for (int i = bytes.Length - 1; i >= 0; i--)
            {
                res += bytes[i] * weight;
                weight <<= 8;
            }
            return res;
        }
        // Returns byte array from 64-bit number
        public static byte[] GetBytesFromUInt64(ulong binary)
        {
            byte[] res = new byte[8];
            for (int i = res.Length - 1; i >= 0; i--)
            {
                res[i] = (byte)GetLowerBits(binary, 8);
                binary >>= 8;
            }
            return res;
        }
    }
}
