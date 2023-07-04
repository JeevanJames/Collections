using BenchmarkDotNet.Running;

using Benchmarks;

BenchmarkRunner.Run<NoCaptureVsCaptureBenchmarks>();
