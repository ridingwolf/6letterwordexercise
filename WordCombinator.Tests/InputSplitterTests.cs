using AutoFixture;
using Shouldly;
using WordCombinator.Domain;
using WordCombinator.Tests.Helpers;

namespace WordCombinator.Tests;

public class When_splitting_input_values_in_parts_and_words: TestBase {
  private readonly int _wordLength;
  private readonly IEnumerable<string> _expectedParts;
  private readonly IEnumerable<string> _expectedValidWords;
  private readonly (IEnumerable<string> WordParts, IEnumerable<string> ValidWords) _splitResult;

  public When_splitting_input_values_in_parts_and_words() {
    _wordLength = new Configuration().WordLength;
    
    _expectedParts = Fixture
      .CreateMany<string>(Random.Next(1, 8))
      .Select(s => s.Substring(0, Random.Next(1, _wordLength)))
      .ToList();
    
    _expectedValidWords = Fixture
      .CreateMany<string>(Random.Next(1, 4))
      .Select(s => s.Substring(0, _wordLength))
      .ToList();
    
    var invalidWords = Fixture
      .CreateMany<string>(Random.Next(1, 4))
      .Select(s => s.Substring(0, Random.Next(_wordLength + 1, _wordLength + 5)))
      .ToList();

    var inputData = _expectedParts
      .Concat(invalidWords)
      .Concat(_expectedValidWords)
      .Shuffle()
      .ToList();
    
    _splitResult = new InputSplitter().SplitPartsAndValidWords(inputData,  _wordLength);
  }
  
  [Fact]
  public void Then_parts_should_contain_the_expected_parts() {
    _splitResult.WordParts.ShouldBe(_expectedParts, ignoreOrder: true);
  }
  
  [Fact]
  public void Then_all_parts_should_be_less_characters_then_the_given_word_length() {
    _splitResult.WordParts.ShouldAllBe(s => s.Length < _wordLength);
  }
  
  [Fact]
  public void Then_valid_words_should_contain_the_expected_valid_words() {
    _splitResult.ValidWords.ShouldBe(_expectedValidWords, ignoreOrder: true);
  }
  
  [Fact]
  public void Then_all_valid_words_should_have_the_given_word_length() {
    _splitResult.ValidWords.ShouldAllBe(s => s.Length == _wordLength);
  }
}