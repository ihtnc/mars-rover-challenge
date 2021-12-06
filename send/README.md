# Mars Rover Challenge
This project is an implementation of the [Mars Rover](https://code.google.com/archive/p/marsrovertechchallenge/) technical challenge.

It was created as a class library so different clients can utilise it. Because of this, the input and output formats in the library are slightly different than what was originally defined in the challenge.

## To use
```C#
var processor = new Processor();

/*
input and output are objects that represents the expected input and output format mentioned in the challenge.
*/
var output = processor.Run(input);
```