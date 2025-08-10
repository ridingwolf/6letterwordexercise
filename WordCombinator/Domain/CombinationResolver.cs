namespace WordCombinator.Domain;

public class CombinationResolver {
  private readonly InputSplitter _splitter = new();
  
  public IEnumerable<Combination> FindCombinations(IEnumerable<string> inputData, int expectedLength, int maximumNumberOfParts) {
    var (parts, validWords) = _splitter.SplitPartsAndValidWords(inputData, expectedLength);
    // benchmark what type would be best to optimize the lookup depending on memory use VS speed given production size data
    var validWordLookup = validWords.ToLookup(name => name);
    
    return ResolveCombinations(new PartialCombination(), parts, expectedLength, maximumNumberOfParts)
      .Where(combination => validWordLookup.Contains(combination.Result))
      .Select(combination => new Combination(combination.PartsValues, combination.Result));
  }
  
  private IEnumerable<PartialCombination> ResolveCombinations(PartialCombination baseCombination, IReadOnlyCollection<string> parts, int expectedLength, int maximumNumberOfParts) {
    for (var i = 0; i < parts.Count; i++) {
      if (baseCombination.PartsFoundAtIndexes.Contains(i))
        continue;
      
      var partialCombination = baseCombination.WithExtraPart(new PartialCombination.Part(parts.ElementAt(i), i));

      if (partialCombination.Result.Length > expectedLength)
        continue;
      
      if (partialCombination.Result.Length == expectedLength)
        yield return partialCombination;
      
      if (partialCombination.NumberOfParts < maximumNumberOfParts) {
        foreach (var nextLevelPartialCombination in ResolveCombinations(partialCombination, parts, expectedLength, maximumNumberOfParts)) {
          yield return nextLevelPartialCombination;
        }
      }
    }
  }
  
  private class PartialCombination {
    private readonly IReadOnlyCollection<Part> _parts;
    
    public PartialCombination()
      :this([])
    { }
    
    private PartialCombination(IEnumerable<Part> parts) {
      _parts = parts.ToList();
      Result = string.Join(null, PartsValues);
    }

    public IEnumerable<string> PartsValues => _parts.Select(p => p.Value);
    public IEnumerable<int> PartsFoundAtIndexes => _parts.Select(p => p.FoundAtIndex);
    public int NumberOfParts => _parts.Count;
    
    public string Result { get; }

    public PartialCombination WithExtraPart(Part part) {
      return new PartialCombination(_parts.Concat([part]));
    }
    
    public record Part(string Value, int FoundAtIndex);
  }
}