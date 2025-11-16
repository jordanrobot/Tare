#### [Tare](index.md 'index')
### [Tare\.Internal\.Units](Tare.Internal.Units.md 'Tare\.Internal\.Units')

## UnitSystemPreference Enum

Specifies the unit system preference for resolving dimension signatures\.

```csharp
internal enum UnitSystemPreference
```
### Fields

<a name='Tare.Internal.Units.UnitSystemPreference.SI'></a>

`SI` 0

Prefer SI units \(meters, newtons, pascals\)\.
This is the default and currently the only implemented preference\.

<a name='Tare.Internal.Units.UnitSystemPreference.USCustomary'></a>

`USCustomary` 1

Prefer US Customary units \(feet, pound\-force, PSI\)\.
Reserved for future implementation\.

### Remarks
This enum enables future support for preferred unit naming based on unit system\.
Initially, only SI \(SI\-first policy\) is implemented\. US Customary support will be
added in a future iteration based on user demand\.