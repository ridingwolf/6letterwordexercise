namespace WordCombinator;

public class Configuration {
  public Configuration() {
    WordLength = 6;
    SourcePath = $@"{VisualStudioProvider.GetSolutionDirectory().FullName}\input.txt";
  }
  
  public readonly int WordLength;
  public readonly string SourcePath;
}