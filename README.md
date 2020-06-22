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
Time to generate:  00:00:03.6054792
Bucket Hits: 1999193, 2000728, 2001119, 1999842, 1999118
Bucket Stats: Max: 2001119, Min: 1999118, MaxMinDelta: 2001

Algorithm: Fnv1a
Time to generate:  00:00:02.0496456
Bucket Hits: 1999328, 2000920, 2000337, 1998854, 2000561
Bucket Stats: Max: 2000920, Min: 1998854, MaxMinDelta: 2066

Algorithm: GetHashCode
Time to generate:  00:00:00.8273842
Bucket Hits: 5000069, 0, 0, 0, 4999931
Bucket Stats: Max: 5000069, Min: 0, MaxMinDelta: 5000069

Algorithm: GetHashCode32
Time to generate:  00:00:00.8295120
Bucket Hits: 5000069, 0, 0, 0, 4999931
Bucket Stats: Max: 5000069, Min: 0, MaxMinDelta: 5000069

Algorithm: DotNetCoreStableStringHash
Time to generate:  00:00:00.7690728
Bucket Hits: 5000173, 0, 0, 0, 4999827
Bucket Stats: Max: 5000173, Min: 0, MaxMinDelta: 5000173

Algorithm: RandomKey
Time to generate:  00:00:19.0113142
Bucket Hits: 1999427, 1999226, 2000978, 2000791, 1999578
Bucket Stats: Max: 2000978, Min: 1999226, MaxMinDelta: 1752

Algorithm: GuidBasedKey
Time to generate:  00:00:01.3039845
Bucket Hits: 1999525, 1999072, 2000115, 2001941, 1999347
Bucket Stats: Max: 2001941, Min: 1999072, MaxMinDelta: 2869
```
