// See https://aka.ms/new-console-template for more information

using WordCombinator;
using WordCombinator.Data;
using WordCombinator.Domain;

var tokenSource = CreateLinkedCancellationTokenSource();
var configuration = new Configuration();
var repo = new Repository(configuration.SourcePath);
var resolver = new CombinationResolver();

Console.WriteLine($"Start processing: {configuration.SourcePath}");
Console.WriteLine("========================================");

var inputData = await repo.GetInputData(tokenSource.Token);
var results = resolver.FindCombinations(inputData, configuration.WordLength);

foreach (var combination in results)
  Console.WriteLine(combination);

if (tokenSource.IsCancellationRequested)
  return;

Console.WriteLine("========================================");
tokenSource.Dispose();
WaitForExitConfirmation();
return;

CancellationTokenSource CreateLinkedCancellationTokenSource()
{
  var source = new CancellationTokenSource();
  Console.CancelKeyPress += (_, eventArgs) => {
    Console.WriteLine("Cancelling...");
    source.Cancel();
    eventArgs.Cancel = true;
  };
  
  return source;
}

void WaitForExitConfirmation()
{
  Console.WriteLine("Press 'the any key' to exit.");
  Console.ReadKey();
}