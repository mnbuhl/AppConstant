// See https://aka.ms/new-console-template for more information

using AppConstant.Benchmarks;
using BenchmarkDotNet.Running;

var res = BenchmarkRunner.Run<GetFromValueBenchmark>();