// See https://aka.ms/new-console-template for more information

using WordCombinator;
using WordCombinator.Data;
using WordCombinator.Domain;

var inputFile =  $@"{VisualStudioProvider.GetSolutionDirectory().FullName}\input.txt";

var tokenSource = CreateLinkedCancellationTokenSource();
var repo = new Repository(inputFile);
var resolver = new CombinationResolver();

Console.WriteLine($"Start processing: {inputFile}");
Console.WriteLine("========================================");

var (wordParts, validWords) = await repo.GetInputData(tokenSource.Token);
var results = resolver.FindCombinations(wordParts, validWords);

foreach (var (parts, words) in results)
  Console.WriteLine($"{string.Join('+', parts)}={words}");

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