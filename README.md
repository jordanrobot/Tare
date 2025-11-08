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

## References and Further Reading

For those interested in learning more about dimensional analysis and units of measure:

- **[Dimensional Analysis (Wikipedia)](https://en.wikipedia.org/wiki/Dimensional_analysis)** - Comprehensive overview of dimensional analysis concepts and applications
- **[Types and Units of Measure (Kennedy Paper)](http://typesatwork.imm.dtu.dk/material/TaW_Paper_TypesAtWork_Kennedy.pdf)** - Academic paper on type-safe units of measure in programming languages
- **[Frink Programming Language](https://frinklang.org/)** - A programming language designed around physical units and dimensional analysis
- **[Frink Units Database](https://frinklang.org/frinkdata/units.txt)** - Comprehensive database of unit definitions and conversion factors
