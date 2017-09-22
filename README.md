![Project type](https://github.com/FredEkstrand/ImageFiles/raw/master/CodeIcon.png ) 

![Version 1.0.0](https://img.shields.io/badge/Version-1.0.0-brightgreen.svg) ![Licence MIT](https://img.shields.io/badge/Licence-MIT-blue.svg)

# Temperature Value Type
Temperature as a value type in the .Net Framework.

#### Features
The Temperature value type have the following features:
* Create instances in desire temperature scale: Celsius, Fahrenheit, Kelvin, and Rankine.
* Default temperature scale is Celsius.
* Min/Max value are ±1.7976931348623157E+308.
* Temperature value type have properties to allow quick conversion to Celsius, Fahrenheit, Kelvin, and Rankine from default defined scale.
* Implicit conversion to/from UInt16, UInt32, UInt64, Int16, Int32, Int64, Single, Decimal, and Double.
* Defined operator: + unary, - negation, - subtract, + addition, * multiplicity, / division, ==, !=, <, >, <=, and >=.
* Defined IComparable, IEquatable, IConvertible, and IFormattable.

## Installing 
The souce code and provided DLL is written in C# and targeted for the .Net Framework 4.0 and later.
You can download the DLL [here](#).
## Getting started
Once downloaded add a reference to the DLL in your Visual Studio project.
Then in your code file add the following to the collection of using statement.
```csharp
using Ekstrand;
```
### Examples
```csharp
Temperature tempValue = new Temperature(23.5); // <- Creates a new instance of Temperature at default Celsius scale.
Temperature tempValue = new Temperature(TemperatureScaleTypes.Celsius); // <- Create a new instance of Temperature with defined temperature scale and default value of 0.
Temperature tempValue = new Temperature(23.5, TemperatureScaleTypes.Fahrenheit); // <--Create a new instance of Temperature with a value and defined scale.
```

### API
API documentation can be found [here](http://fredekstrand.github.io/TemperatureValue). 

## History
 1.0.0 Initial release into the wild.
 
## Contributing

If you'd like to contribute, please fork the repository and use a feature
branch. Pull requests are always welcome.

## Contact
Fred Ekstrand 
email: fredekstrandgithub@gmail.com
## Licensing

This project is licensed under the MIT License - see the [LICENSE.md](https://github.com/FredEkstrand/TemperatureValue) file for details.


