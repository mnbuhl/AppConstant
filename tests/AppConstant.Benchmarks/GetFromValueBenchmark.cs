using AppConstant.Benchmarks.Fixtures;
using BenchmarkDotNet.Attributes;

namespace AppConstant.Benchmarks;

[MemoryDiagnoser]
public class GetFromValueBenchmark
{
    [Benchmark]
    public void GetConstant_FromNumber_Zero() => Number.Get(0);
    
    [Benchmark]
    public void GetConstant_FromNumber_Ten() => Number.Get(10);
    
    [Benchmark]
    public void GetConstant_FromLetter_A() => Letter.Get('A');
    
    [Benchmark]
    public void GetConstant_FromLetter_Z() => Letter.Get('Z');
    
    [Benchmark]
    public void GetConstant_FromRole_Admin() => Role.Get("Admin");
    
    [Benchmark]
    public void GetConstant_FromRole_Guest() => Role.Get("Guest");
}