#### [Tare](index.md 'index')
### [Tare.Internal.Units](Tare.Internal.Units.md 'Tare.Internal.Units').[PreferredUnit](Tare.Internal.Units.PreferredUnit.md 'Tare.Internal.Units.PreferredUnit')

## PreferredUnit(string, string, string[]) Constructor

Initializes a new instance of the [PreferredUnit](Tare.Internal.Units.PreferredUnit.md 'Tare.Internal.Units.PreferredUnit') struct.

```csharp
public PreferredUnit(string canonicalName, string description, params string[] alternativeNames);
```
#### Parameters

<a name='Tare.Internal.Units.PreferredUnit.PreferredUnit(string,string,string[]).canonicalName'></a>

`canonicalName` [System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

The canonical unit name.

<a name='Tare.Internal.Units.PreferredUnit.PreferredUnit(string,string,string[]).description'></a>

`description` [System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

Description of the physical quantity.

<a name='Tare.Internal.Units.PreferredUnit.PreferredUnit(string,string,string[]).alternativeNames'></a>

`alternativeNames` [System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')[[]](https://docs.microsoft.com/en-us/dotnet/api/System.Array 'System.Array')

Optional alternative names for the same signature.

#### Exceptions

[System.ArgumentNullException](https://docs.microsoft.com/en-us/dotnet/api/System.ArgumentNullException 'System.ArgumentNullException')  
Thrown when canonicalName or description is null.