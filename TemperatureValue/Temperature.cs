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
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Ekstrand
{
    [Serializable]
    [DebuggerDisplay("{ToString(\"SD\")}")]
    public struct Temperature : IEquatable<Temperature>, IComparable<Temperature>, IFormattable, IConvertible, IComparable
    {
        #region Structure level global varables and objects.

        /// <summary>
        /// Minimum value for a Temperature.
        /// </summary>
        public const double MinValue = -1.7976931348623157E+308;
        /// <summary>
        /// Maximum value for a Temperature.
        /// </summary>
        public const double MaxValue =  1.7976931348623157E+308;

        private double m_TemperatureValue;                  // scaled to Celsius
        private TemperatureScaleTypes m_TemperatureType;    // temperature scale

        #endregion

        #region Constructors
    
        /// <summary>
        /// Create a new instance of Temperature with a Celsius value.
        /// </summary>
        /// <param name="value">Numeric Value Type in Celsius.</param>
        public Temperature(ValueType value) 
        {
            if (!IsNumeric(value))
            { throw new ArgumentException("Value is not a number."); }

            this.m_TemperatureValue = 0;
            this.m_TemperatureType = TemperatureScaleTypes.Celsius;
            RescaleToCelsius(Convert.ToDouble(value), m_TemperatureType);
        }

        /// <summary>
        /// Create a new instance of Temperature with defined temperature scale.
        /// </summary>
        /// <param name="value">Enumeration TemperatureTypes</param>
        public Temperature(TemperatureScaleTypes value)
        {
            this.m_TemperatureValue = 0;
            this.m_TemperatureType = value;
            RescaleToCelsius(0, m_TemperatureType);
        }

        /// <summary>
        /// Create a new instance of Temperature with a value in one of the defined scales.
        /// </summary>
        /// <param name="value">Numeric Value Type</param>
        /// <param name="temp">The scale the given value is in.</param>
        public Temperature(ValueType value, TemperatureScaleTypes temp)
        {
            if (!IsNumeric(value))
            { throw new ArgumentException("Value is not a number."); }

            this.m_TemperatureValue = 0;
            this.m_TemperatureType = temp; 
            RescaleToCelsius(Convert.ToDouble(value), m_TemperatureType);
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Check if given ValueType is a numeric type.
        /// </summary>
        /// <param name="value">ValueType to be type queried.</param>
        /// <returns>True iff ValueType given is a numeric type otherwise false.</returns>
        private static bool IsNumeric(ValueType value)
        {
            if (value is SByte ||
                value is Byte ||
                value is Decimal ||
                value is Double ||
                value is UInt16 ||
                value is UInt32 ||
                value is UInt64 ||
                value is Int16 ||
                value is Int32 ||
                value is Int64 ||
                value is Single
              )
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Rescale given value to Celsius.
        /// </summary>
        /// <param name="value">Double value to be rescaled.</param>
        /// <param name="tempType">TemperatureScaleType the given value is in.</param>
        private void RescaleToCelsius(double value, TemperatureScaleTypes tempType)
        {
            switch (tempType)
            {
                case TemperatureScaleTypes.Celsius:
                    this.m_TemperatureValue = Math.Round((value), 4, MidpointRounding.AwayFromZero);
                    break;
                case TemperatureScaleTypes.Fahrenheit:
                    this.m_TemperatureValue = Math.Round((value - 32) * .555555555, 4, MidpointRounding.AwayFromZero);
                    break;
                case TemperatureScaleTypes.Kelvin:
                    this.m_TemperatureValue = Math.Round((value - 273.15), 4, MidpointRounding.AwayFromZero);
                    break;
                case TemperatureScaleTypes.Rankine:
                    this.m_TemperatureValue = Math.Round((value - 491.67) * .555555555, 4, MidpointRounding.AwayFromZero);                   
                    break;
            }
        }

        /// <summary>
        /// Rescale from Celsius to any of the supported temperature scales.
        /// </summary>
        /// <param name="tempType">TemperatureScaleTypes to rescale to.</param>
        /// <returns>Returns the internal Celsius value rescaled to the given TemperatureScaleTypes value.</returns>
        private double RescaleFromCelsius(TemperatureScaleTypes tempType)
        {
            switch (tempType)
            {
                case TemperatureScaleTypes.Celsius:
                    return m_TemperatureValue;
                case TemperatureScaleTypes.Fahrenheit:
                    return Math.Round((m_TemperatureValue * 1.80000) + 32,4,MidpointRounding.AwayFromZero);
                case TemperatureScaleTypes.Kelvin:
                    return Math.Round(m_TemperatureValue + 273.15, 4, MidpointRounding.AwayFromZero);
                case TemperatureScaleTypes.Rankine:
                    return Math.Round((m_TemperatureValue + 273.15) * 1.80000, 4, MidpointRounding.AwayFromZero);
            }

            // should never get here
            return MinValue;
        }

        #endregion

        #region Properities

        /// <summary>
        /// The get accessor return a value based on its set temperature scale.
        /// The set accessor sets the value at the current temperature scale.
        /// </summary>
        public double Value
        {
            get { return RescaleFromCelsius(this.m_TemperatureType); }
            set { RescaleToCelsius(value,this.m_TemperatureType); }
        }

        /// <summary>
        /// The get accessor returns the set temperature scale.
        /// </summary>
        public TemperatureScaleTypes TemperatureScale
        {
            get { return this.m_TemperatureType; }
            internal set { this.m_TemperatureType = value; }
        }

        /// <summary>
        /// The get accessor returns the temperature value in Celsius scale.
        /// </summary>
        public double ValueAsCelsius
        {
            get { return RescaleFromCelsius(TemperatureScaleTypes.Celsius); }
        }

        /// <summary>
        /// The get accessor returns the temperature value in Fahrenheit scale.
        /// </summary>
        public double ValueAsFahrenheit
        {
            get { return RescaleFromCelsius(TemperatureScaleTypes.Fahrenheit); }
        }

        /// <summary>
        /// The get accessor returns the temperature value in Kelvin scale.
        /// </summary>
        public double ValueAsKelvin
        {
            get { return RescaleFromCelsius(TemperatureScaleTypes.Kelvin); }
        }

        /// <summary>
        /// The get accessor returns the temperature value in Rankine scale.
        /// </summary>
        public double ValueAsRankine
        {
            get { return RescaleFromCelsius(TemperatureScaleTypes.Rankine); }
        }

        #endregion

        # region Implicit Operators

        public static implicit operator Temperature(Byte value)
        {
            return new Temperature(value);
        }

        public static implicit operator Temperature(SByte value)
        {
            return new Temperature(value);
        }

        public static implicit operator Temperature(UInt16 value)
        {
            return new Temperature(value);
        }

        public static implicit operator Temperature(UInt32 value)
        {
            return new Temperature(value);
        }

        public static implicit operator Temperature(UInt64 value)
        {
            return new Temperature(value);
        }

        public static implicit operator Temperature(Int16 value)
        {
            return new Temperature(value);
        }

        public static implicit operator Temperature(Int32 value)
        {
            return new Temperature(value);
        }

        public static implicit operator Temperature(Int64 value)
        {
            return new Temperature(value);
        }

        public static implicit operator Temperature(Single value)
        {
            return new Temperature(value);
        }

        public static implicit operator Temperature(Decimal value)
        {
            return new Temperature(value);
        }

        public static implicit operator Temperature(Double value)
        {
            return new Temperature(value);
        }

        public static implicit operator SByte(Temperature value)
        {
            return (SByte)value.Value;
        }

        public static implicit operator Byte(Temperature value)
        {
            return (Byte)value.Value;
        }

        public static implicit operator UInt16(Temperature value)
        {
            return (UInt16)value.Value;
        }

        public static implicit operator UInt32(Temperature value)
        {
            return (UInt32)value.Value;
        }

        public static implicit operator UInt64(Temperature value)
        {
            return (UInt64)value.Value;
        }

        public static implicit operator Int16(Temperature value)
        {
            return (Int16)value.Value;
        }

        public static implicit operator Int32(Temperature value)
        {
            return (Int32)value.Value;
        }

        public static implicit operator Int64(Temperature value)
        {
            return (Int64)value.Value;
        }

        public static implicit operator Decimal(Temperature value)
        {
            return (Decimal)value.Value;
        }

        public static implicit operator Double(Temperature value)
        {
            return value.Value;
        }

        public static implicit operator Single(Temperature value)
        {
            return (Single)value.Value;
        }

        #endregion

        #region Operators

        public static Temperature operator -(Temperature temp)
        {
            return new Temperature(-temp.Value);
        }

        public static Temperature operator +(Temperature temp)
        {
            return new Temperature(temp.Value);
        }

        public static Temperature operator -(Temperature tempL, Temperature tempR)
        {
            return new Temperature(tempL.Value - tempR.Value);
        }

        public static Temperature operator +(Temperature tempL, Temperature tempR)
        {
            return new Temperature(tempL.Value + tempR.Value);
        }

        public static Temperature operator *(Temperature tempL, Temperature tempR)
        {
            return new Temperature(tempL.Value * tempR.Value);
        }

        public static Temperature operator /(Temperature numerator, Temperature denominator)
        {
            return new Temperature((double)(numerator.Value / denominator.Value));
        }

        public static Boolean operator ==(Temperature left, Temperature right)
        {
            return left.Equals(right);
        }

        public static Boolean operator !=(Temperature left, Temperature right)
        {
            return !left.Equals(right);
        }

        public static Boolean operator >(Temperature left, Temperature right)
        {
            return left.CompareTo(right) > 0;
        }

        public static Boolean operator <(Temperature left, Temperature right)
        {
            return left.CompareTo(right) < 0;
        }

        public static Boolean operator >=(Temperature left, Temperature right)
        {
            return left.CompareTo(right) >= 0;
        }

        public static Boolean operator <=(Temperature left, Temperature right)
        {
            return left.CompareTo(right) <= 0;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Change the current temperature scale to another.
        /// </summary>
        /// <param name="tempType">Enumeration TemperatureScaleTypes</param>
        /// <remarks>
        /// Changing the temperature scale would cause the currently value to be recalculated at the new scale.
        /// </remarks>
        public void ChangeScale(TemperatureScaleTypes tempType)
        {
            double tempVal = this.m_TemperatureValue;
            m_TemperatureValue = 0;
            this.m_TemperatureType = tempType;
            RescaleToCelsius(tempVal, this.m_TemperatureType);
        }

        /// <summary>
        /// Change the current temperature value and scale.
        /// </summary>
        /// <param name="value">ValueType for temperature.</param>
        /// <param name="tempType">Enumeration TemperatureTypes</param>
        public void ChangeScale(ValueType value, TemperatureScaleTypes tempType)
        {
            if (!IsNumeric(value))
            { throw new ArgumentException("Value is not a number."); }

            double tempVal = Convert.ToDouble(value);
            this.m_TemperatureValue = 0;
            this.m_TemperatureType = tempType;
            RescaleToCelsius(tempVal,this.m_TemperatureType);
        }

        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        /// <param name="obj">The object to compare with the current object. </param>
        /// <returns>true if the specified object is equal to the current object; otherwise, false.</returns>
        public override Boolean Equals(Object obj)
        {
            if (!(obj is Temperature))
            {
                return false;
            }

            double other = (Temperature)obj;
            return Equals(Math.Round(other,4));
        }



        /// <summary>
        /// Serves as the default hash function. 
        /// </summary>
        /// <returns>A hash code for the current object.</returns>
        public override Int32 GetHashCode()
        {
            return Value.GetHashCode();
        }

        /// <summary>
        /// Converts the string representation of a number to its double-precision 
        /// floating-point number equivalent. A return value indicates whether the 
        /// conversion succeeded or failed.
        /// </summary>
        /// <param name="str">A string containing a number to convert. </param>
        /// <param name="temp">When this method returns, contains the double-precision 
        /// floating-point number equivalent of the str parameter, if the conversion 
        /// succeeded, or false if the conversion failed. The conversion fails if the str
        /// parameter is null or String.Empty, is not a number in a valid format, or 
        /// represents a number less than MinValue or greater than MaxValue. This parameter 
        /// is passed uninitialized; any value originally supplied in result will be overwritten. </param>
        /// <returns></returns>
        public static Boolean TryParse(String str, out Temperature temp)
        {
            temp = 0;
            double value = 0;
            string parseString = string.Empty;
            TemperatureScaleTypes tempType = TemperatureScaleTypes.Celsius;

            if (str == null || str == string.Empty)
            {
                return false;
            }

            str.Trim();

            // add check for C,F,K,R
            char[] scales = new char[] { '°', 'C', 'F', 'K', 'R' };
            int pos = str.ToUpper().IndexOfAny(scales);

            if (pos != -1)
            {
                switch (str[str.Length - 1])
                {
                    case 'C':
                        tempType = TemperatureScaleTypes.Celsius;
                        break;
                    case 'F':
                        tempType = TemperatureScaleTypes.Fahrenheit;
                        break;
                    case 'K':
                        tempType = TemperatureScaleTypes.Kelvin;
                        break;
                    case 'R':
                        tempType = TemperatureScaleTypes.Rankine;
                        break;
                    default:
                        throw new ArgumentException("Temperature scale unknown.");
                }

                if (str[str.Length - 2] == '°')
                {
                    parseString = str.Substring(0, str.Length - 3);
                }
                else
                {
                    parseString = str.Substring(0, str.Length - 2);
                }
            }
            else
            {
                parseString = str;
            }

            // temp scale format 

            if (!Double.TryParse(parseString, out value))
            {
                return false;
            }

            temp.ChangeScale(value, tempType);
            return true;
        }

        /// <summary>
        /// Return a formatted string of this object.
        /// </summary>
        /// <returns>Returns a string representation of this Temperature value.</returns>
        public override string ToString()
        {
            return String.Format(new TemperatureToStringFormatter(), "{0:SD}", this);
        }

        /// <summary>
        /// Return a formatted string of this object.
        /// </summary>
        /// <param name="format">String format values. "S" for scalar only, "SD" for scalar and degree symbol or "N" for scalar value only. </param>
        /// <returns>Return a formatted string based on given format value</returns>
        public string ToString(String format)
        {
            switch (format)
            {
                case "S":
                    return String.Format(new TemperatureToStringFormatter(), "{0:S}", this);
                case "SD":
                    return String.Format(new TemperatureToStringFormatter(), "{0:SD}", this);
                case "N":
                    return String.Format(new TemperatureToStringFormatter(), "{0:N}", this);
                case "SN":
                    return String.Format(new TemperatureToStringFormatter(), "{0:SN}", this);
                case "SDN":
                    return String.Format(new TemperatureToStringFormatter(), "{0:SDN}", this);
            }

            throw new FormatException("Invalid format parameter: " + format);
        }


        #endregion

        #region Interface Implimentation

        public int CompareTo(object obj)
        {
            if (obj == null)
            {
                throw new NullReferenceException();
            }

            Temperature temp = (Temperature)obj;
            return this.m_TemperatureValue.CompareTo(temp.ValueAsCelsius);

        }

        public int CompareTo(Temperature other)
        {
            if (other == null)
            {
                throw new NullReferenceException();
            }

            return this.m_TemperatureValue.CompareTo(other.ValueAsCelsius);
        }

        public bool Equals(Temperature other)
        {
            if (other == null)
            {
                throw new NullReferenceException();
            }

            return this.m_TemperatureValue.Equals(other.ValueAsCelsius);
        }

        public TypeCode GetTypeCode()
        {
            return TypeCode.Object;
        }

        public bool ToBoolean(IFormatProvider provider)
        {
            throw new NotSupportedException();
        }

        public byte ToByte(IFormatProvider provider)
        {
            return (byte)m_TemperatureValue;
        }

        public char ToChar(IFormatProvider provider)
        {
            throw new NotSupportedException();
        }

        public DateTime ToDateTime(IFormatProvider provider)
        {
            throw new NotSupportedException();
        }

        public decimal ToDecimal(IFormatProvider provider)
        {
            return (decimal)m_TemperatureValue;
        }

        public double ToDouble(IFormatProvider provider)
        {
            return (double)m_TemperatureValue;
        }

        public short ToInt16(IFormatProvider provider)
        {
            return (Int16)m_TemperatureValue;
        }

        public int ToInt32(IFormatProvider provider)
        {
            return (Int32)m_TemperatureValue;
        }

        public long ToInt64(IFormatProvider provider)
        {
            return (Int64)m_TemperatureValue;
        }

        public sbyte ToSByte(IFormatProvider provider)
        {
            return (sbyte)m_TemperatureValue;
        }

        public float ToSingle(IFormatProvider provider)
        {
            return (float)m_TemperatureValue;
        }

        public string ToString(IFormatProvider provider)
        {
            return this.ToString(provider);
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            return this.ToString(format, formatProvider);
        }

        public object ToType(Type conversionType, IFormatProvider provider)
        {
            throw new NotSupportedException();
        }

        public ushort ToUInt16(IFormatProvider provider)
        {
            return (UInt16)m_TemperatureValue;
        }

        public uint ToUInt32(IFormatProvider provider)
        {
            return (UInt32)m_TemperatureValue;
        }

        public ulong ToUInt64(IFormatProvider provider)
        {
            return (UInt64)m_TemperatureValue;
        }

        #endregion

    }

    public enum TemperatureScaleTypes
    {
        Celsius = 0,
        Fahrenheit,
        Kelvin,
        Rankine // Yah. Still in use here in the USA
                // Delisle, Newton, Réaumur, Rømer are scales that are no longer in use.
    }
}

