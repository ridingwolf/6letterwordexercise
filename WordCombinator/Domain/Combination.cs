namespace WordCombinator.Domain;

public class Combination {
  public Combination(IEnumerable<string> parts, string result) {
    Parts = parts;
    Result = result;
  }

  public IEnumerable<string> Parts { get; }
  public string Result { get; }

  public override bool Equals(object? obj)
    => obj is Combination combination && Equals(combination);

  private bool Equals(Combination other)
    => Result == other.Result && Parts.SequenceEqual(other.Parts);

  public override int GetHashCode()
    => HashCode.Combine(Parts, Result);

  public override string ToString()
    => $"{string.Join('+', Parts)}={Result}";
}