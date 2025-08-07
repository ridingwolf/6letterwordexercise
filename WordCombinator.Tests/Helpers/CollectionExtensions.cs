namespace WordCombinator.Tests.Helpers;

public static class CollectionExtensions {
  public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> collection)
    => collection.OrderBy(_ => Guid.NewGuid());
}