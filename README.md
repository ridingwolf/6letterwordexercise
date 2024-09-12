# Technical Developer test

In this developer test case, you're going to implement an algorithmn. The objective is to test your skills as a developer.

Technologies that should be used:

- .NET
- C#

Your code will be evaluated using the following criteria:

- Correctness of the algorithm
- Performance
- Clean code
- Seperation of concerns

The way the code needs to be run, is not important. This can be as simple as a console app.

Be mindful of changing requirements like a different maximum combination length, or a different source of the input data. Don't spend too much time on this exercise. 

Feel free to send us a mail if you have any questions about the exercise.

## Submitting the exercise

The assignment can be submitted by sending us your solution through e-mail: 

- Briefly write down where you would improve the code if you were given more time.
- The solution may be sent as a link to your repository, as a zip file in the mail, or uploaded to a file hosting service (eg: wetransfer).

## 6 letter words

There's a file in the root of the repository, input.txt, that contains words of varying lengths (1 to 6 characters).

Your objective is to show all combinations of those words that:

- Together form a word of 6 characters.
- That combination must also be present in input.txt.

You can start by only supporting combinations of two words and improve the algorithm at the end of the exercise to support any combinations.


### Example

When the program is run with this input:
```
foobar
fo
o
bar
```

Then the program should ouput:
```
fo+o+bar=foobar
```
