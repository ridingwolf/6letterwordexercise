using System.Collections;
using Shouldly;
using WordCombinator.Domain;
using WordCombinator.Tests.Helpers;

namespace WordCombinator.Tests;

public class When_resolving_a_test_input_data_set : TestBase {
  private readonly CombinationResolver _sut;
  private readonly int _wordLength;

  public When_resolving_a_test_input_data_set() {
    _wordLength = new Configuration().WordLength;
    _sut = new CombinationResolver();
  }

  [Theory]
  [ClassData(typeof(ResolverTestCaseData))]
  public void Then_the_expected_test_result_should_be_returned(string[] input, IEnumerable<Combination> expectedCombinations) {
    
    var results = _sut.FindCombinations(input, _wordLength);
    results.ShouldBe(expectedCombinations,  ignoreOrder: true);
  }
}

public class ResolverTestCaseData : IEnumerable<object[]> {
  
  private readonly ResolverTestCase[] _cases = [
    new() {
      InputData = ["foo", "bar", "foobar"],
      ExpectedCombinations = [
        (["foo", "bar"], "foobar")
      ]
    },
    new() {
      InputData = ["bar", "foobar", "foo"],
      ExpectedCombinations = [
        (["foo", "bar"], "foobar")
      ]
    },
    new() {
      InputData = ["barfoo", "bar", "foobar", "foo"],
      ExpectedCombinations = [
        (["foo", "bar"], "foobar"),
        (["bar", "foo"], "barfoo")
      ]
    },
    new() {
      InputData = ["bar", "o", "foobar", "fo"],
      ExpectedCombinations = [
        (["fo", "o", "bar"], "foobar")
      ]
    },
    new() {
      InputData = ["fo", "ar", "ob", "bar",  "foobar"],
      ExpectedCombinations = [
        (["fo", "ob", "ar"], "foobar")
      ]
    },
    new() {
      InputData = ["b", "foo", "o", "f", "o", "bar", "r", "a",  "foobar"],
      ExpectedCombinations = [
        (["foo", "bar"], "foobar"),
        (["f", "o", "o", "b", "a", "r"], "foobar"),
        (["f", "o", "o", "bar"], "foobar"),
        (["foo", "b", "a", "r"], "foobar"),
        
        // because "o" exists twice you get duplicates in possible combinations
        // "f" "o1" "o2" ....
        // "f" "o2" "o1" ....
        // duplicates not removed until clear how duplicates should handled, some options are:
        // - duplicates are OK
        // - validate/sanitize input (will likely result in issues, removing the duplicate "o" would remove both combinations)
        // - remove duplicates from results
        (["f", "o", "o", "b", "a", "r"], "foobar"),
        (["f", "o", "o", "bar"], "foobar"),
      ]
    }
  ];
  
  public IEnumerator<object[]> GetEnumerator()
    => _cases
    .Select(testCase => new object[] { testCase.InputData,  testCase.ExpectedCombinations.Select(combination => new Combination(combination.Parts, combination.Result)) })
    .GetEnumerator();

  IEnumerator IEnumerable.GetEnumerator()
    => GetEnumerator();

  private class ResolverTestCase {
    public string[] InputData { get; set; }
    public (string[] Parts, string Result)[] ExpectedCombinations { get; set; }
  }
}

