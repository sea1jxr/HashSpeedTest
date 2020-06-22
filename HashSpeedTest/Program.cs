using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.ServiceFabric.Actors;

namespace HashSpeedTest
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> testValuePart1s = new List<string>();
            List<string> testValuePart2s = new List<string>();
            for (int i = 0; i < 10000000; i++)
            {
                testValuePart1s.Add(Guid.NewGuid().ToString());
                testValuePart2s.Add(Guid.NewGuid().ToString());
            }

            Console.WriteLine($"Iterations: {testValuePart1s.Count}");
            TimeAlgorithm(testValuePart1s, testValuePart2s, ComputeCrc32, "CRC32");
            TimeAlgorithm(testValuePart1s, testValuePart2s, ComputeFnv, "Fnv1a");
            TimeAlgorithm(testValuePart1s, testValuePart2s, ComputeGetHashCode, "GetHashCode");
            TimeAlgorithm(testValuePart1s, testValuePart2s, ComputeGetHashCode32, "GetHashCode32");
            TimeAlgorithm(testValuePart1s, testValuePart2s, DotNetCoreStableStringHash, "DotNetCoreStableStringHash");
            TimeAlgorithm(testValuePart1s, testValuePart2s, (value1, value2) => RandomKey(), "RandomKey");
            TimeAlgorithm(testValuePart1s, testValuePart2s, (value1, value2) => GuidBasedKey(), "GuidBasedKey");
        }

        private static void RecordBucketHit(List<ulong> buckHitLog, int numBuckets, long value)
        {
            ulong bucketSize = ulong.MaxValue / (ulong)numBuckets;
            for (int i = 1; i <= numBuckets; i++)
            {
                int bucketIndex = numBuckets - i;

                ulong bucketMinimum = ulong.MaxValue - ((ulong)i) * bucketSize;
                if ((ulong)value >= bucketMinimum ||
                    bucketIndex == 0)
                {
                    buckHitLog[bucketIndex] += 1;
                    break;
                }
            }
        }

        private static void TimeAlgorithm(List<string> testValuePart1s, List<string> testValuePart2s, Func<string, string, long> algorithm, string algorithmName)
        {
            const int numBuckets = 5;
            List<ulong> bucketHits = new List<ulong>(numBuckets);
            for (int i = 0; i < numBuckets; i++)
            {
                bucketHits.Add(0);
            }

            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            for (int i = 0; i < testValuePart1s.Count; i++)
            {
                long hashValue = algorithm(testValuePart1s[i], testValuePart2s[i]);
                RecordBucketHit(bucketHits, numBuckets, hashValue);
            }
            stopWatch.Stop();

            Console.WriteLine();
            Console.WriteLine($"{algorithmName} took: {stopWatch.Elapsed}");
            Console.WriteLine(string.Join(", ", bucketHits.Select(h => ((ulong)h).ToString())));
            Console.WriteLine($"Max: {bucketHits.Max()}, Min: {bucketHits.Min()}, MaxMinDelta: {bucketHits.Max() - bucketHits.Min()}");
        }

        private static long ComputeCrc32(string value1, string value2)
        {
            return new ActorId(value1).GetPartitionKey() ^ new ActorId(value2).GetPartitionKey();
        }

        private static long ComputeFnv(string value1, string value2)
        {
            return Fnv1aHash.Hash64(value1, value2);
        }

        private static long ComputeGetHashCode(string value1, string value2)
        {
            long hash = value1.GetHashCode() ^ value2.GetHashCode();
            long key = (long)(((ulong)hash) * 2);
            return key;
        }

        private static long ComputeGetHashCode32(string value1, string value2)
        {
            return (long)(value1.GetHashCode() ^ value2.GetHashCode());
        }

        private static long DotNetCoreStableStringHash(string value1, string value2)
        {
            return InternalDotNetCoreStableStringHash(value1) ^ InternalDotNetCoreStableStringHash(value2);
        }

        private static long RandomKey()
        {
            byte[] buffer = new byte[8];
            new Random().NextBytes(buffer);
            long key = BitConverter.ToInt64(buffer, 0);
            return key;
        }

        private static long GuidBasedKey()
        {
            Guid guid = Guid.NewGuid();
            return Fnv1aHash.Hash64(guid.ToByteArray());
        }

        private static long InternalDotNetCoreStableStringHash(string input)
        {
            unchecked
            {
                int hash1 = (5381 << 16) + 5381;
                int hash2 = hash1;

                for (int i = 0; i < input.Length; i += 2)
                {
                    hash1 = ((hash1 << 5) + hash1) ^ input[i];
                    if (i == input.Length - 1)
                    {
                        break;
                    }

                    hash2 = ((hash2 << 5) + hash2) ^ input[i + 1];
                }

                return hash1 + (hash2 * 1566083941);
            }
        }

    }

}
