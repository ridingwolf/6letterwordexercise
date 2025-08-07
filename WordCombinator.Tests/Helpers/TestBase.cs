using AutoFixture;

namespace WordCombinator.Tests.Helpers;

public abstract class TestBase {
  protected Random Random { get; } = new Random();
  protected IFixture Fixture { get; } = new Fixture();
}