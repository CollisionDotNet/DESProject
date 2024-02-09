using System.Linq;
using System.Text;

namespace DESProject
{
    public class DESEncryptor
    {
        public enum EncryptionMode
        {
            Encrypt,
            Decrypt
        }
        #region numericConstants
        public const int DESRepeatsCount = 16;
        #endregion
        #region permutationTables
        private static int[] startPermutationTable =
        {
            58, 50, 42, 34, 26, 18, 10, 2,  60, 52, 44, 36, 28, 20, 12, 4,
            62, 54, 46, 38, 30, 22, 14, 6,  64, 56, 48, 40, 32, 24, 16, 8,
            57, 49, 41, 33, 25, 17, 9,  1,  59, 51, 43, 35, 27, 19, 11, 3,
            61, 53, 45, 37, 29, 21, 13, 5,  63, 55, 47, 39, 31, 23, 15, 7,
        };

        private static int[] endPermutationTable =
        {
            40, 8,  48, 16, 56, 24, 64, 32, 39, 7,  47, 15, 55, 23, 63, 31,
            38, 6,  46, 14, 54, 22, 62, 30, 37, 5,  45, 13, 53, 21, 61, 29,
            36, 4,  44, 12, 52, 20, 60, 28, 35, 3,  43, 11, 51, 19, 59, 27,
            34, 2,  42, 10, 50, 18, 58, 26, 33, 1,  41, 9,  49, 17, 57, 25,
        };

        private static int[] extendPermutationTable =
        {
            32, 1,  2,  3,  4,  5,
            4,  5,  6,  7,  8,  9,
            8,  9,  10, 11, 12, 13,
            12, 13, 14, 15, 16, 17,
            16, 17, 18, 19, 20, 21,
            20, 21, 22, 23, 24, 25,
            24, 25, 26, 27, 28, 29,
            28, 29, 30, 31, 32, 1,
        };

        private static int[] PPermutationTable =
        {
            16, 7,  20, 21, 29, 12, 28, 17,
            1,  15, 23, 26, 5,  18, 31, 10,
            2,  8,  24, 14, 32, 27, 3,  9,
            19, 13, 30, 6,  22, 11, 4,  25
        };

        private static int[] StartKeyPermutationTable =
        {
            57, 49, 41, 33, 25, 17, 9,
            1,  58, 50, 42, 34, 26, 18,
            10, 2,  59, 51, 43, 35, 27,
            19, 11, 3,  60, 52, 44, 36,
            63, 55, 47, 39, 31, 23, 15,
            7,  62, 54, 46, 38, 30, 22,
            14, 6,  61, 53, 45, 37, 29,
            21, 13, 5,  28, 20, 12, 4
        };

        private static int[] EndKeyPermutationTable =
        {
            14, 17, 11, 24, 1,  5,
            3,  28, 15, 6,  21, 10,
            23, 19, 12, 4,  26, 8,
            16, 7,  27, 20, 13, 2,
            41, 52, 31, 37, 47, 55,
            30, 40, 51, 45, 33, 48,
            44, 49, 39, 56, 34, 53,
            46, 42, 50, 36, 29, 32
        };
        #endregion
        #region otherTransformationTables
        private static int[,,] STransformationTables =
        {
            {
                { 14, 4,  13, 1,  2,  15, 11, 8,  3,  10, 6,  12, 5,  9,  0,  7  },
                { 0,  15, 7,  4,  14, 2,  13, 1,  10, 6,  12, 11, 9,  5,  3,  8  },
                { 4,  1,  14, 8,  13, 6,  2,  11, 15, 12, 9,  7,  3,  10, 5,  0  },
                { 15, 12, 8,  2,  4,  9,  1,  7,  5,  11, 3,  14, 10, 0,  6,  13 },
            },
            {
                { 15, 1,  8,  14, 6,  11, 3,  4,  9,  7,  2,  13, 12, 0,  5,  10 },
                { 3,  13, 4,  7,  15, 2,  8,  14, 12, 0,  1,  10, 6,  9,  11, 5  },
                { 0,  14, 7,  11, 10, 4,  13, 1,  5,  8,  12, 6,  9,  3,  2,  15 },
                { 13, 8,  10, 1,  3,  15, 4,  2,  11, 6,  7,  12, 0,  5,  14, 9  },
            },
            {
                { 10, 0,  9,  14, 6,  3,  15, 5,  1,  13, 12, 7,  11, 4,  2,  8  },
                { 13, 7,  0,  9,  3,  4,  6,  10, 2,  8,  5,  14, 12, 11, 15, 1  },
                { 13, 6,  4,  9,  8,  15, 3,  0,  11, 1,  2,  12, 5,  10, 14, 7  },
                { 1,  10, 13, 0,  6,  9,  8,  7,  4,  15, 14, 3,  11, 5,  2,  12 },
            },
            {
                { 7,  13, 14, 3,  0,  6,  9,  10, 1,  2,  8,  5,  11, 12, 4,  15 },
                { 13, 8,  11, 5,  6,  15, 0,  3,  4,  7,  2,  12, 1,  10, 14, 9  },
                { 10, 6,  9,  0,  12, 11, 7,  13, 15, 1,  3,  14, 5,  2,  8,  4  },
                { 3,  15, 0,  6,  10, 1,  13, 8,  9,  4,  5,  11, 12, 7,  2,  14 },
            },
            {
                { 2,  12, 4,  1,  7,  10, 11, 6,  8,  5,  3,  15, 13, 0,  14, 9  },
                { 14, 11, 2,  12, 4,  7,  13, 1,  5,  0,  15, 10, 3,  9,  8,  6  },
                { 4,  2,  1,  11, 10, 13, 7,  8,  15, 9,  12, 5,  6,  3,  0,  14 },
                { 11, 8,  12, 7,  1,  14, 2,  13, 6,  15, 0,  9,  10, 4,  5,  3  },
            },
            {
                { 12, 1,  10, 15, 9,  2,  6,  8,  0,  13, 3,  4,  14, 7,  5,  11 },
                { 10, 15, 4,  2,  7,  12, 9,  5,  6,  1,  13, 14, 0,  11, 3,  8  },
                { 9,  14, 15, 5,  2,  8,  12, 3,  7,  0,  4,  10, 1,  13, 11, 6  },
                { 4,  3,  2,  12, 9,  5,  15, 10, 11, 14, 1,  7,  6,  0,  8,  13 },
            },
            {
                { 4,  11, 2,  14, 15, 0,  8,  13, 3,  12, 9,  7,  5,  10, 6,  1  },
                { 13, 0,  11, 7,  4,  9,  1,  10, 14, 3,  5,  12, 2,  15, 8,  6  },
                { 1,  4,  11, 13, 12, 3,  7,  14, 10, 15, 6,  8,  0,  5,  9,  2  },
                { 6,  11, 13, 8,  1,  4,  10, 7,  9,  5,  0,  15, 14, 2,  3,  12 },
            },
            {
                { 13, 2,  8,  4,  6,  15, 11, 1,  10, 9,  3,  14, 5,  0,  12, 7  },
                { 1,  15, 13, 8,  10, 3,  7,  4,  12, 5,  6,  11, 0,  14, 9,  2  },
                { 7,  11, 4,  1,  9,  12, 14, 2,  0,  6,  10, 13, 15, 3,  5,  8  },
                { 2,  1,  14, 7,  4,  10, 8,  13, 15, 12, 9,  0,  3,  5,  6,  11 },
            },
        };

        private static int[] KeyComputingShiftsTable = { 1, 1, 2, 2, 2, 2, 2, 2, 1, 2, 2, 2, 2, 2, 2, 1 };
        #endregion


        public byte[] Apply(byte[] bytes, ulong key, EncryptionMode mode)
        {
            // If bytes to encrypt are not divisable by 8 - add zeroes to the end.
            if(bytes.Length % 8 != 0)
            {
                int newSize = ((bytes.Length / 8) + 1) * 8;
                Array.Resize(ref bytes, newSize);
            }
            // Left byte if a current block position
            int bytePos = 0;
            IEnumerable<byte> encryptedBytes = Enumerable.Empty<byte>();
            // Go through all the sequence
            while (bytePos < bytes.Length)
            {
                // Take next 8 bytes
                IEnumerable<byte> currentBlock = bytes.Skip(bytePos).Take(8);
                // Convert to ulong
                ulong binaryToEncrypt = BinariesHelper.GetUInt64FromBytes(currentBlock.ToArray());
                ulong encryptedBinary;
                // Encode it according to selected encryption mode
                if (mode == EncryptionMode.Encrypt)
                {
                    encryptedBinary = EncryptBlock(binaryToEncrypt, key);
                }
                else
                {
                    encryptedBinary = DecryptBlock(binaryToEncrypt, key);
                }
                // Convert the result back to byte sequence and concatenate it with already computed.
                byte[] converted = BinariesHelper.GetBytesFromUInt64(encryptedBinary);               
                encryptedBytes = encryptedBytes.Concat(converted);
                // Shift block position
                bytePos += 8;                
            }
            return encryptedBytes.ToArray();
        }
        // Returns DES-encrypted block with a specified key
        private ulong EncryptBlock(ulong toEncryptBlock, ulong key)
        {
            // Compute keys
            ulong[] keys = ComputeKeys(key);
            // Start permutation
            toEncryptBlock = BinariesHelper.ApplyPermutation(toEncryptBlock, startPermutationTable, 64);
            // Split T_0 into L_0 and R_0
            ulong prevLeft = toEncryptBlock >> 32; 
            ulong prevRight = BinariesHelper.GetLowerBits(toEncryptBlock, 32);
            // Feistel cycles
            for (int i = 0; i < DESRepeatsCount; i++) 
            {
                ulong curLeft = prevRight;
                // Apply XOR with Feistel function
                ulong curRight = prevLeft ^ ApplyFeistel(prevRight, keys[i]); 
                prevRight = curRight;
                prevLeft = curLeft;
            }
            // Concatenation T_16 = R_16L_16 (backwards)
            ulong result = (prevRight << 32) + prevLeft;
            // Final permutation
            return BinariesHelper.ApplyPermutation(result, endPermutationTable, 64); 
        }
        // Returns DES-decrypted block with a specified key
        private ulong DecryptBlock(ulong toDecryptBlock, ulong key)
        {
            // Compute keys
            ulong[] keys = ComputeKeys(key);
            // Start permutation
            toDecryptBlock = BinariesHelper.ApplyPermutation(toDecryptBlock, startPermutationTable, 64);
            // Split T_0 into L_0 and R_0
            ulong prevLeft = toDecryptBlock >> 32;
            ulong prevRight = BinariesHelper.GetLowerBits(toDecryptBlock, 32);
            // Feistel cycles, keys are applied backwards
            for (int i = DESRepeatsCount - 1; i >= 0; i--)
            {
                // Changed Feistel transformation
                ulong curLeft = prevRight;
                ulong curRight = prevLeft ^ ApplyFeistel(prevRight, keys[i]);
                prevRight = curRight;
                prevLeft = curLeft;
            }
            // Concatenation T_16 = R_16L_16 (backwards)
            ulong result = (prevRight << 32) + prevLeft;
            // Final permutation
            return BinariesHelper.ApplyPermutation(result, endPermutationTable, 64);

        }
        private ulong[] ComputeKeys(ulong key)
        {
            ulong[] keys = new ulong[DESRepeatsCount];
            //Start permutation. Original 64 bits are converted into 56.
            key = BinariesHelper.ApplyPermutation(key, StartKeyPermutationTable, 64);
            // Split 56 bits into left and right parts
            ulong leftKeyPart = key >> 28;
            ulong rightKeyPart = BinariesHelper.GetLowerBits(key, 28);
            // Keys computing iterations
            for (int i = 0; i < DESRepeatsCount; i++)
            {
                // Left cyclic shift by 1 
                if (KeyComputingShiftsTable[i] == 1)
                {
                    leftKeyPart = ((leftKeyPart << 1) + (leftKeyPart >> 27));
                    leftKeyPart = BinariesHelper.GetLowerBits(leftKeyPart, 28);
                    rightKeyPart = ((rightKeyPart << 1) + (rightKeyPart >> 27));
                    rightKeyPart = BinariesHelper.GetLowerBits(rightKeyPart, 28);
                }
                // Left cyclic shift by 2 
                else
                {
                    leftKeyPart = ((leftKeyPart << 2) + (leftKeyPart >> 26));
                    leftKeyPart = BinariesHelper.GetLowerBits(leftKeyPart, 28);
                    rightKeyPart = ((rightKeyPart << 2) + (rightKeyPart >> 26));
                    rightKeyPart = BinariesHelper.GetLowerBits(rightKeyPart, 28);
                }
                // Concatenate left and right parts and apply permutation
                ulong computedKey = (leftKeyPart << 28) + rightKeyPart;
                keys[i] = BinariesHelper.ApplyPermutation(computedKey, EndKeyPermutationTable, 56);
            }
            return keys;
        }

        private ulong ApplyFeistel(ulong prevRight, ulong key)
        {
            // Expand R_{i-1} up to 48 bits
            ulong extended = BinariesHelper.ApplyPermutation(prevRight, extendPermutationTable, 32); 
            extended ^= key; // XOR with k_i key

            ulong[] blocks = new ulong[8]; //Split extended R_{i-1} into 8 blocks with 6 bits in each
            for (int i = 0; i < 8; i++)
            {
                blocks[7 - i] = BinariesHelper.GetLowerBits(extended, 6); // Take 6 lower bits
                extended >>= 6; // And shift it to the right
            }
            for (int i = 0; i < 8; i++) // Find S_i for every block
            {
                ulong curBlockValue = blocks[i];
                // Compute row number as first and last bits concatenation
                int rowNum = 2 * BinariesHelper.GetBitFromBinary(curBlockValue, 1, 6) 
                    + BinariesHelper.GetBitFromBinary(curBlockValue, 6, 6);
                // Compute column number as four middle bits concatenation
                int columnNum = 
                    8 * BinariesHelper.GetBitFromBinary(curBlockValue, 2, 6) 
                    + 4 * BinariesHelper.GetBitFromBinary(curBlockValue, 3, 6) 
                    + 2 * BinariesHelper.GetBitFromBinary(curBlockValue, 4, 6) 
                    + BinariesHelper.GetBitFromBinary(curBlockValue, 5, 6);
                // New 4-bit value can be found according to the table
                blocks[i] = (ulong)STransformationTables[i, rowNum, columnNum]; 
            }
            ulong result = 0;
            ulong weight = 1;
            for (int i = 7; i >= 0; i--)
            {
                result += blocks[i] * weight; // Concatenate S(B_i)
                weight <<= 4;
            }
            // Apply P-permutation
            return BinariesHelper.ApplyPermutation(result, PPermutationTable, 32);
        }        
    }
}
