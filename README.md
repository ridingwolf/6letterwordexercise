# 6 letter words

There's a file in the root of the repository, input.txt, that contains words of varying lengths (1 to 6 characters).

Your objective is to show all combinations of those words that together form a word of 6 characters. That combination must also be present in input.txt  
E.g.:

```
foobar
fo
obar
```

should result in the ouput:

```
fo+obar=foobar
```

You can start by only supporting combinations of two words and improve the algorithm at the end of the exercise to support any combinations.

Treat this exercise as if you were writing production code; think unit tests, SOLID, clean code and avoid primitive obsession. Be mindful of changing requirements like a different maximum combination length, or a different source of the input data.

Don't spend too much time on this. When submitting the exercise, briefly write down where you would improve the code if you were given more time.
