#### [Tare](index.md 'index')
### [Tare.Internal.Units](Tare.Internal.Units.md 'Tare.Internal.Units').[UnitToken](Tare.Internal.Units.UnitToken.md 'Tare.Internal.Units.UnitToken')

## UnitToken(string) Constructor

Constructs a unit token from a canonical string.

```csharp
public UnitToken(string canonical);
```
#### Parameters

<a name='Tare.Internal.Units.UnitToken.UnitToken(string).canonical'></a>

`canonical` [System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

The canonical unit identifier (e.g., "in", "lbf", "m").

#### Exceptions

[System.ArgumentException](https://docs.microsoft.com/en-us/dotnet/api/System.ArgumentException 'System.ArgumentException')  
Thrown when canonical is null, empty, or whitespace.