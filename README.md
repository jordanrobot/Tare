[api documentation](docs/api/Tare.md 'Tare API') | [contributions](docs/Contributions.md) | [changelog](docs/CHANGELOG.md)

# Tare

A simple, dynamic library for a unit of measure value type.

## Sample

```c#
var length1 = Quantity.Parse("1.5 m");
var length2 = Quantity.Parse("13 in");

var totalLength = length1 + length2;
Console.WriteLine(totalLength.Format("m"))); // "1.8302 m"

if (length1 > Length2)
    Console.WriteLine("Length 1 is greater than length 2");

Quantity scalar = 3;

var lengthMultiple = Length1 * scalar;
Console.WriteLine(lengthMultiple.Format("ft")); // "14.7638 ft"
```
