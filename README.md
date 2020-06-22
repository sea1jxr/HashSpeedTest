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
Time to generate: 00:00:03.6565103
Bucket Hits: 1997922, 1997294, 2001672, 2001833, 2001279
Bucket Stats: Max: 2001833, Min: 1997294, MaxMinDelta: 4539

Algorithm: Fnv1a
Time to generate: 00:00:02.0686897
Bucket Hits: 1999540, 2001065, 1998053, 2001362, 1999980
Bucket Stats: Max: 2001362, Min: 1998053, MaxMinDelta: 3309

Algorithm: GetHashCode
Time to generate: 00:00:00.7873994
Bucket Hits: 0, 0, 10000000, 0, 0
Bucket Stats: Max: 10000000, Min: 0, MaxMinDelta: 10000000

Algorithm: GetHashCode32
Time to generate: 00:00:00.7711973
Bucket Hits: 0, 0, 10000000, 0, 0
Bucket Stats: Max: 10000000, Min: 0, MaxMinDelta: 10000000

Algorithm: DotNetCoreStableStringHash
Time to generate: 00:00:00.7066899
Bucket Hits: 0, 0, 10000000, 0, 0
Bucket Stats: Max: 10000000, Min: 0, MaxMinDelta: 10000000

Algorithm: RandomKey
Time to generate: 00:00:17.8900950
Bucket Hits: 1997183, 2002898, 1998996, 2000976, 1999947
Bucket Stats: Max: 2002898, Min: 1997183, MaxMinDelta: 5715

Algorithm: GuidBasedKey
Time to generate: 00:00:01.2908527
Bucket Hits: 2000998, 1999113, 2000805, 2001027, 1998057
Bucket Stats: Max: 2001027, Min: 1998057, MaxMinDelta: 2970
```
