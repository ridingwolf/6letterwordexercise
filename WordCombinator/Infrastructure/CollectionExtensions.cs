namespace WordCombinator.Infrastructure;

public static class CollectionExtensions {
  public static (T Item ,  IReadOnlyCollection<T> LeftoverCollection) ExtractItemAtIndex<T>(this IEnumerable<T> collection, int index) {
    // Depending on the collection size and usage, it might be good to benchmark this with specific collection Types instead of using IEnumerable & List
    // for now IEnumerable & List will do
    var items = collection.ToList();
    
    var item = items.ElementAt(index);
    items.RemoveAt(index);
    
    return (item, items);
  }
}