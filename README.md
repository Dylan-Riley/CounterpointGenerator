# CounterpointGenerator
Given a set of rules and a user melody input generate a melodic counterpoint.

This was initially created and developed as a part of [CodeDay Labs'](https://labs.codeday.org/) summer of 2021 internship program. Our showcase can be found [here](https://showcase.codeday.org/project/ckqtyhsom91734710quoskwgr13).

## Input

Input is currently a string expected to follow the pattern "[int] [space] [double] [comma] [space] ... *repeat*" to crudely represent a melody line made up of note pitches and note lengths. Example:
```
0 16, 5 4, 6 4, 9 8, 0 16
```
As established in the constants file a pitch of 0 represents C5, and a length of 16.0 represents a whole note.

## Contributing
Contributions are welcome!
Style guidelines: https://docs.microsoft.com/en-us/dotnet/csharp/fundamentals/coding-style/coding-conventions
