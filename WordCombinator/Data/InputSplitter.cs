namespace WordCombinator.Data;

public class InputSplitter {
  private readonly int _wordLength;

  public InputSplitter(Configuration configuration) {
    _wordLength = configuration.WordLength;
  }

  public (IEnumerable<string> WordParts, IEnumerable<string> ValidWords) Split(IEnumerable<string> inputData) {
    var wordParts = new List<string>();
    var validWords = new List<string>();

    foreach (var data in inputData) {
      if (data.Length == _wordLength)
        validWords.Add(data);
      
      if (data.Length < _wordLength)
        wordParts.Add(data);
      
      // data.Length > _wordLength -> data not valid, ignored in split should be handled by a validation service
    }
    
    return new (wordParts, validWords);
  }
}