using WordCombinator.Data;

namespace WordCombinator.Domain;

public class CombinationResolver {
  private readonly InputSplitter _splitter = new();
  
  public IEnumerable<Combination> FindCombinations(IEnumerable<string> inputData, int expectedLength) {
    var (parts, validWords) = _splitter.SplitPartsAndValidWords(inputData, expectedLength);

    throw new NotImplementedException("Todo implement logic");
  }
}