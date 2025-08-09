namespace WordCombinator;

public class Configuration {
  public Configuration() {
    WordLength = 6;
    MaximumNumberOfParts = 2;
    SourcePath = $@"{VisualStudioProvider.GetSolutionDirectory().FullName}\input.txt";
  }
  
  public int WordLength { get; }
  public int MaximumNumberOfParts { get; }
  public string SourcePath { get; }
}