namespace WordCombinator.Domain;

public class InputSplitter {
  public (IReadOnlyCollection<string> WordParts, IReadOnlyCollection<string> ValidWords) SplitPartsAndValidWords(IEnumerable<string> inputData, int expectedLength) {
    var wordParts = new List<string>();
    var validWords = new List<string>();

    foreach (var data in inputData) {
      if (data.Length == expectedLength)
        validWords.Add(data);
      
      if (data.Length < expectedLength)
        wordParts.Add(data);

      if (data.Length > expectedLength) {
        // words that are already too long get ignored
        // maybe add some logging later
      }
    }
    
    return new (wordParts, validWords);
  }
}