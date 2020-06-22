# HashSpeedTest
Some quick speed and bucket spreading tests across a 64bit number for usage with ServiceFabric partitions.

CRC32 - Algorithm that is used in ActorId.GetPartitionKey()

Fnv1a - Suggested by Service Fabric in documentation

GetHashCode - using string.GetHashCode() and trying to expand it to 64 bits with *2

GetHashCode32 - using string.GetHashCode() without trying to expand it to 64 bits

DotNetCoreStableStringHash - using the stable string hash from DotNetCore

RandomKey - Generate a random key using Random to generate a random set of 8 bytes

GuidBasedKey - Generate a random key by using the Fnv1a algorithm over a new guid.

And Example Run
```
Iterations: 10000000

Algorithm: CRC32
Time to generate: 00:00:03.9287843
Bucket Hits: 1999163, 1999287, 2000497, 2000252, 2000801
Bucket Stats: Max: 2000801, Min: 1999163, MaxMinDelta: 1638

Algorithm: Fnv1a
Time to generate: 00:00:02.2127846
Bucket Hits: 1999539, 1999020, 2000282, 2001366, 1999793
Bucket Stats: Max: 2001366, Min: 1999020, MaxMinDelta: 2346

Algorithm: GetHashCode
Time to generate: 00:00:00.8392753
Bucket Hits: 4997688, 0, 0, 0, 5002312
Bucket Stats: Max: 5002312, Min: 0, MaxMinDelta: 5002312

Algorithm: GetHashCode32
Time to generate: 00:00:00.8378993
Bucket Hits: 4997688, 0, 0, 0, 5002312
Bucket Stats: Max: 5002312, Min: 0, MaxMinDelta: 5002312

Algorithm: DotNetCoreStableStringHash
Time to generate: 00:00:00.7803366
Bucket Hits: 4999034, 0, 0, 0, 5000966
Bucket Stats: Max: 5000966, Min: 0, MaxMinDelta: 5000966

Algorithm: RandomKey
Time to generate: 00:00:17.8780455
Bucket Hits: 2000856, 1997936, 2000415, 2000030, 2000763
Bucket Stats: Max: 2000856, Min: 1997936, MaxMinDelta: 2920

Algorithm: GuidBasedKey
Time to generate: 00:00:01.3107351
Bucket Hits: 2000758, 2000682, 1999633, 1998893, 2000034
Bucket Stats: Max: 2000758, Min: 1998893, MaxMinDelta: 1865
```
