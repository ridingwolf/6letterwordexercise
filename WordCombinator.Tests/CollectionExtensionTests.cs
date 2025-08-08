using AutoFixture;
using Shouldly;
using WordCombinator.Infrastructure;
using WordCombinator.Tests.Helpers;

namespace WordCombinator.Tests;

public class When_extracting_an_item_from_a_collection : TestBase {
  private readonly TestItem _expectedItem;
  private readonly IEnumerable<TestItem> _expectedList;
  private readonly (TestItem item, IEnumerable<TestItem> LeftoverCollection) _splitResult;
  private readonly List<TestItem> _originalCollection;

  public When_extracting_an_item_from_a_collection() {
    _expectedItem = Fixture.Create<TestItem>();
    _expectedList = Fixture.CreateMany<TestItem>(Random.Next(1, 5));

    _originalCollection = _expectedList.Concat([_expectedItem]).Shuffle().ToList();
    var index = _originalCollection.IndexOf(_expectedItem);

    _splitResult = _originalCollection.ExtractItemAtIndex(index);
  }

  [Fact]
  public void Then_the_item_is_the_expected_item() {
    _splitResult.item.ShouldBeSameAs(_expectedItem);
  }

  [Fact]
  public void Then_the_leftover_is_the_expected_leftover_list() {
    _splitResult.LeftoverCollection.ShouldBe(_expectedList, ignoreOrder: true);
  }

  [Fact]
  public void Then_the_leftover_does_not_contain_the_expected_item() {
    _splitResult.LeftoverCollection.ShouldNotContain(_expectedItem);
  }

  [Fact]
  public void Then_the_original_collection_should_still_contain_the_expected_item() {
    _originalCollection.ShouldContain(_expectedItem);
  }

private class TestItem { public Guid Id { get; init; } }
}