namespace WordCombinator.Data;

public class InputSplitter {
  private const int WordLength = 6;
  
  public (IEnumerable<string> WordParts, IEnumerable<string> ValidWords) Split(IEnumerable<string> inputData) {
    var wordParts = new List<string>();
    var validWords = new List<string>();

    foreach (var data in inputData) {
      switch (data.Length)
      {
        case WordLength:
          validWords.Add(data);
          break;
        case < WordLength:
          wordParts.Add(data);
          break;
        
        // data.Length > WordLength -> data not valid, ignored in split should be handled by a validation service
      }
    }
    
    return new ValueTuple<IEnumerable<string>, IEnumerable<string>>(wordParts, validWords);
  }
}