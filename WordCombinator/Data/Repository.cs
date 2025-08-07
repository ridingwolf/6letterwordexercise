namespace WordCombinator.Data;

public class Repository {
  private readonly string _sourcePath;

  public Repository(string sourcePath) {
    _sourcePath = sourcePath;
  }
  
  /// <summary> Gets formated input data </summary>
  public async Task<IEnumerable<string>> GetInputData(CancellationToken cancel)
  {
    var input = await File.ReadAllLinesAsync(_sourcePath, cancel);
    
    return input
      .Where(value => !string.IsNullOrWhiteSpace(value))
      .Select(value => value.Trim());
  }
}