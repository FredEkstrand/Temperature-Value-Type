/*
Open source MIT License
Copyright (c) 2017 Fred A Ekstrand Jr.

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.

AKH8-NN0E9-N3ST7-9491J-1R656-18CT0-U816
*/

using System;
using System.Globalization;
using System.Text;

namespace Ekstrand
{
    /// <summary>
    /// Formatter for adding degree symbol and/or degree scale identifier or just the value in string form.
    /// </summary>
    public class TemperatureToStringFormatter : IFormatProvider, ICustomFormatter
    {
        private const string CelsiusDegreeSymbol = " °C";
        private const string FahrenheitDegreeSymbol = " °F";
        private const string KelvinDegreeSymbol = " °K";
        private const string RankineDegreeSymbol = " °R";

        /// <summary>
        /// Crate an instance of TemperatureToStringFormatter
        /// </summary>
        public TemperatureToStringFormatter()
        { }

        
        /// <summary>
        /// IFormatProvider interface implementation
        /// </summary>
        /// <param name="formatType">Type of formatter provider</param>
        /// <returns>Return the defined formatter otherwise null</returns>
        public object GetFormat(Type formatType)
        {
            if (formatType == typeof(ICustomFormatter))
            {
                return this;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// ICustomFormatter interface implementation.
        /// </summary>
        /// <param name="format">string to be formatted.</param>
        /// <param name="arg">Object reference.</param>
        /// <param name="formatProvider">FormatProvider to be used.</param>
        /// <returns>Return a formatted string otherwise null.</returns>
        public string Format(string format, object arg, IFormatProvider formatProvider)
        {
            // Check if arg is of type Temperature structure
            if (arg.GetType() != typeof(Temperature))
            {
                throw new FormatException(String.Format("The format of '{0}' is invalid.", format));
            }

            Temperature temp = (Temperature)arg;
            if(temp == null)
            {
                throw new ArgumentException("Invalid Temperature object.");
            }

            string result = string.Empty;
            string ufmt = format.ToUpper(CultureInfo.InvariantCulture);

            switch(ufmt)
            {
                case "S":
                    return result = string.Format("{0:0.0000}{1}", temp.Value , ScaluarAttribute("S",temp.TemperatureScale));
                case "SD":
                    return result = string.Format("{0:0.0000}{1}", temp.Value , ScaluarAttribute("SD", temp.TemperatureScale));
                case "N":
                    return result = string.Format("{0:0.0000}", temp.Value);
                case "SN":
                    return result = string.Format("{0:0.0000}{1}", temp.Value, ScaluarAttribute("SN", temp.TemperatureScale));
                case "SDN":
                    return result = string.Format("{0:0.0000}{1}", temp.Value, ScaluarAttribute("SDN", temp.TemperatureScale));
                default:
                try
                {
                    return HandleOtherFormats(format, arg);
                }
                catch (FormatException e)
                {
                    throw new FormatException(String.Format("The format of '{0}' is invalid. ", format), e);
                }
            }
        }

        private string HandleOtherFormats(string format, object arg)
        {
            if (arg is IFormattable)
            {
                return ((IFormattable)arg).ToString(format, CultureInfo.CurrentCulture);
            }
            else if (arg != null)
            {
                return arg.ToString();
            }
            else
            {
                return String.Empty;
            }
        }


        private string ScaluarAttribute(string format, TemperatureScaleTypes type)
        {
            switch (type)
            {
                case TemperatureScaleTypes.Celsius:
                    return ScaluarAttributeFormat(format, CelsiusDegreeSymbol);
                case TemperatureScaleTypes.Fahrenheit:
                    return ScaluarAttributeFormat(format, FahrenheitDegreeSymbol);
                case TemperatureScaleTypes.Kelvin:
                    return ScaluarAttributeFormat(format, KelvinDegreeSymbol);
                case TemperatureScaleTypes.Rankine:
                    return ScaluarAttributeFormat(format, RankineDegreeSymbol);
            }
            return string.Empty;
        }

        private string ScaluarAttributeFormat(string format, string scalar)
        {
            switch (format)
            {
                case "S":
                    return " " + scalar.Substring(scalar.Length - 1);
                case "SD":
                    return scalar;
                case "SN":
                    return scalar.Substring(scalar.Length - 1);
                case "SDN":
                    return scalar.Substring(scalar.Length - 2);
            }
            return string.Empty;
        }
    }
}
