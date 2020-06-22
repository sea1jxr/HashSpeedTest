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

CRC32 took: 00:00:03.5644465
1997614, 1999152, 2000857, 2001177, 2001200
Max: 2001200, Min: 1997614, MaxMinDelta: 3586

Fnv1a took: 00:00:02.0458714
2001450, 1998078, 2000986, 2000155, 1999331
Max: 2001450, Min: 1998078, MaxMinDelta: 3372

GetHashCode took: 00:00:00.8285899
4998010, 0, 0, 0, 5001990
Max: 5001990, Min: 0, MaxMinDelta: 5001990

GetHashCode32 took: 00:00:00.8232419
4998010, 0, 0, 0, 5001990
Max: 5001990, Min: 0, MaxMinDelta: 5001990

DotNetCoreStableStringHash took: 00:00:00.7829821
4998767, 0, 0, 0, 5001233
Max: 5001233, Min: 0, MaxMinDelta: 5001233

RandomKey took: 00:00:18.3500610
1998557, 1999112, 2001257, 2001148, 1999926
Max: 2001257, Min: 1998557, MaxMinDelta: 2700

GuidBasedKey took: 00:00:01.2822294
2000125, 1998991, 2000346, 1998874, 2001664
Max: 2001664, Min: 1998874, MaxMinDelta: 2790
```
