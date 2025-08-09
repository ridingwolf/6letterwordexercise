using WordCombinator.Infrastructure;

namespace WordCombinator.Domain;

public class CombinationResolver {
  private readonly InputSplitter _splitter = new();
  
  public IEnumerable<Combination> FindCombinations(IEnumerable<string> inputData, int expectedLength) {
    var (parts, validWords) = _splitter.SplitPartsAndValidWords(inputData, expectedLength);
    // benchmark what type would be best to optimize the lookup depending on memory use VS speed given production size data
    var validWordLookup = validWords.ToLookup(name => name);
    
    return ResolveCombinations(new PartialCombination(), parts, expectedLength)
      .Where(combination => validWordLookup.Contains(combination.Result))
      .Select(combination => new Combination(combination.Parts, combination.Result));
  }
  
  private IEnumerable<PartialCombination> ResolveCombinations(PartialCombination baseCombination, IReadOnlyCollection<string> parts, int expectedLength) {
    for (var i = 0; i < parts.Count; i++) {
      
      var (part, otherParts) = parts.ExtractItemAtIndex(i);
      var partialCombination = baseCombination.WithExtraPart(part);

      if (partialCombination.Result.Length == expectedLength) {
        yield return partialCombination;
      }
      else if (partialCombination.Result.Length < expectedLength) {
        foreach (var nextLevelPartialCombination in ResolveCombinations(partialCombination, otherParts, expectedLength)) {
            yield return nextLevelPartialCombination;
        }
      }
    }
  }
  
  private class PartialCombination {

    public PartialCombination()
      :this(new List<string>())
    { }

    private PartialCombination(IEnumerable<string> parts) {
      Parts = parts.ToList();
      Result = string.Join(null, Parts);
    }

    public IEnumerable<string> Parts { get; }
    public string Result { get; }

    public PartialCombination WithExtraPart(string part) {
      return new PartialCombination(Parts.Concat([part]));
    }
  }
}