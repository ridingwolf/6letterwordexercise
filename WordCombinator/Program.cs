using System.Diagnostics;
using WordCombinator;
using WordCombinator.Data;
using WordCombinator.Domain;

var tokenSource = new CancellationTokenSource();
var stopWatch = new Stopwatch();

var configuration = new Configuration();
var repo = new Repository(configuration.SourcePath);
var resolver = new CombinationResolver();

Console.WriteLine($"Start processing: {configuration.SourcePath}");
Console.WriteLine("========================================");
stopWatch.Start();

var inputData = await repo.GetInputData(tokenSource.Token);
var results = resolver.FindCombinations(inputData, configuration.WordLength, configuration.MaximumNumberOfParts);

foreach (var combination in results)
  Console.WriteLine(combination);

stopWatch.Stop();
Console.WriteLine("========================================");
Console.WriteLine($"Took {stopWatch.ElapsedMilliseconds/1000} seconds");
Console.WriteLine("Press 'the any key' to exit.");
Console.ReadKey();
