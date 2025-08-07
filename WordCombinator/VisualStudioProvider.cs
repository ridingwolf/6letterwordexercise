namespace WordCombinator;

public static class VisualStudioProvider
{
  // stack overflow copy + refactor, might not be optimal
  // didn't want to spend too much time making sure the input file directory was relative
  public static DirectoryInfo GetSolutionDirectory()
    => FindSolutionDirectory(new DirectoryInfo(Directory.GetCurrentDirectory()));

  private static DirectoryInfo FindSolutionDirectory(DirectoryInfo directory) {
    if (directory.GetFiles("*.sln").Any())
      return directory;

    return directory.Parent == null
      ? throw new DirectoryNotFoundException("Solution directory not found")
      : FindSolutionDirectory(directory.Parent);
  }
}