namespace WordCombinator.Data;

public class Repository {
  private readonly InputSplitter _inputSplitter;
  private readonly string _sourcePath;

  public Repository(Configuration configuration) {
    _sourcePath = configuration.SourcePath;
    _inputSplitter = new InputSplitter(configuration);
  }
  
  /// <summary> Gets formated input data </summary>
  public async Task<(IEnumerable<string> WordParts, IEnumerable<string> ValidWords)> GetInputData(CancellationToken cancel)
  {
    var input = await File.ReadAllLinesAsync(_sourcePath, cancel);
    // splitting the file in parts|valid-words
    // assuming that in a real scenario there would need to be validation on the file(s), input and data might even be split over several files
    var (parts, words) = _inputSplitter.Split(
      input
        .Where(value => !string.IsNullOrWhiteSpace(value))
        .Select(value => value.Trim()
        ));
    
    return (parts, words.Distinct());
  }
}