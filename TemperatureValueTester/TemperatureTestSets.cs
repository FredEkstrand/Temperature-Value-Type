using System;
using System.Data;
using NUnit.VisualStudio.TestAdapter;
using NUnit.Framework;
using Ekstrand;
using System.Globalization;

namespace TemperatureValueTester
{
    [TestFixture]
    public class TemperatureTestSets
    {
        TemperatureDataTable tdt = new TemperatureDataTable();

        #region Internal Methods

        private bool ValuesEqual(ValueType val1, ValueType val2)
        {
            double difference = Math.Abs(Convert.ToDouble(val1) * .0001);
            if (Math.Abs(Convert.ToDouble(val1) - Convert.ToDouble(val2)) <= difference)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private double RescaleToCelsius(double value, TemperatureScaleTypes tempType)
        {
            switch (tempType)
            {
                case TemperatureScaleTypes.Celsius:
                    return value;
                case TemperatureScaleTypes.Fahrenheit:
                    return (value - 32) * (5 / 9);
                case TemperatureScaleTypes.Kelvin:
                    return (value - 273.15);
                case TemperatureScaleTypes.Rankine:
                    return (value - 491.67) * (5 / 9);
            }

            // should never get here
            return value;
        }

        private double RecaleFromCelsius(double value, TemperatureScaleTypes tempType)
        {
            switch (tempType)
            {
                case TemperatureScaleTypes.Celsius:
                    return value;
                case TemperatureScaleTypes.Fahrenheit:
                    return value * 9 / 5 + 32;
                case TemperatureScaleTypes.Kelvin:
                    return value + 273.15;
                case TemperatureScaleTypes.Rankine:
                    return (value + 273.15) * 9 / 5;
            }

            // should never get here
            return value;
        }

        #endregion

        #region Constructors Test Sets

        [Test]
        [Category("Constructors")]
        public void ConstructorWithNoParameters()
        {
            Temperature temp = new Temperature();

            // checking default states 
            Assert.AreEqual(true, ValuesEqual(0, temp.ValueAsCelsius));
            Assert.AreEqual(true, ValuesEqual(32, temp.ValueAsFahrenheit));
            Assert.AreEqual(true, ValuesEqual(273.15, temp.ValueAsKelvin));
            Assert.AreEqual(true, ValuesEqual(491.67, temp.ValueAsRankine));
            Assert.AreEqual(0, temp.Value);
            Assert.AreEqual(TemperatureScaleTypes.Celsius, temp.TemperatureScale);
        }

        [Test]
        [Category("Constructors")]
        public void ConstructorWithTemperatureScaleParameter()
        {
            Temperature temp = new Temperature(TemperatureScaleTypes.Rankine);

            // checking default states
            Assert.AreEqual(true, ValuesEqual(-273.15, temp.ValueAsCelsius));
            Assert.AreEqual(true, ValuesEqual(-459.67, temp.ValueAsFahrenheit));
            Assert.AreEqual(true, ValuesEqual(0, temp.ValueAsKelvin));
            Assert.AreEqual(true, ValuesEqual(0, temp.ValueAsRankine));
            Assert.AreEqual(true, ValuesEqual(0, temp.Value));
            Assert.AreEqual(TemperatureScaleTypes.Rankine, temp.TemperatureScale);
        }

        [Test]
        [Category("Constructors")]
        public void ConstructorWithValueTypeParameter()
        {
            TempDataSet tds = tdt.NextTestSet();
            Temperature temp = new Temperature(tds.Celsius);
            
            // checking default states
            Assert.AreEqual(true, ValuesEqual(tds.Celsius, temp.ValueAsCelsius));
            Assert.AreEqual(true, ValuesEqual(tds.Fahrenheit, temp.ValueAsFahrenheit));
            Assert.AreEqual(true, ValuesEqual(tds.Kelvin, temp.ValueAsKelvin));
            Assert.AreEqual(true, ValuesEqual(tds.Rankine, temp.ValueAsRankine));
            Assert.AreEqual(true, ValuesEqual(tds.Celsius, temp.Value));
            Assert.AreEqual(TemperatureScaleTypes.Celsius, temp.TemperatureScale);
        }

        [Test]
        [Category("Constructors")]
        public void ConstructorWithValueTypeAndTemperatureScale()
        {
            TempDataSet tds = tdt.NextTestSet();
            Temperature temp = new Temperature(tds.Celsius, TemperatureScaleTypes.Celsius);

            Assert.AreEqual(true, ValuesEqual(tds.Celsius, temp.ValueAsCelsius));
            Assert.AreEqual(true, ValuesEqual(tds.Fahrenheit, temp.ValueAsFahrenheit));
            Assert.AreEqual(true, ValuesEqual(tds.Kelvin, temp.ValueAsKelvin));
            Assert.AreEqual(true, ValuesEqual(tds.Rankine, temp.ValueAsRankine));
            Assert.AreEqual(true, ValuesEqual(tds.Celsius, temp.Value));
            Assert.AreEqual(TemperatureScaleTypes.Celsius, temp.TemperatureScale);
        }

        #endregion

        #region Prosperities Test Sets

        [Test]
        [Category("Prosperities")]
        public void ProperitieValueGetTest()
        {

            TempDataSet tds = tdt.NextTestSet();
            Temperature temp = new Temperature(tds.Celsius);
          
            // get every prosperities and check them
            Assert.AreEqual(true, ValuesEqual(tds.Celsius, temp.ValueAsCelsius));
            Assert.AreEqual(true, ValuesEqual(tds.Fahrenheit, temp.ValueAsFahrenheit));
            Assert.AreEqual(true, ValuesEqual(tds.Kelvin, temp.ValueAsKelvin));
            Assert.AreEqual(true, ValuesEqual(tds.Rankine, temp.ValueAsRankine));
            Assert.AreEqual(true, ValuesEqual(tds.Celsius, temp.Value));
            Assert.AreEqual(TemperatureScaleTypes.Celsius, temp.TemperatureScale);
        }

        [Test]
        [Category("Prosperities")]
        public void ProperitieValueSetTest()
        {
            TempDataSet tds = tdt.NextTestSet();
            Temperature temp = new Temperature(tds.Celsius);

            temp.Value = tds.Rankine;
            Assert.AreEqual(true, ValuesEqual(tds.Rankine, temp.Value));         
        }

        [Test]
        [Category("Prosperities")]
        public void ProperitieTemperatureScaleGetTest()
        {
            TempDataSet tds = tdt.NextTestSet();
            Temperature temp = new Temperature(tds.Value, tds.Scalar);

            Assert.AreEqual(tds.Scalar, temp.TemperatureScale);
        }

        [Test]
        [Category("Prosperities")]
        public void ProperitieValueAsCelsiusTest()
        {
            TempDataSet tds = tdt.NextTestSet();
            Temperature temp = new Temperature(tds.Celsius);

            Assert.AreEqual(true, ValuesEqual(tds.Celsius, temp.ValueAsCelsius));
        }

        [Test]
        [Category("Prosperities")]
        public void ProperitieValueAsFahrenheitTest()
        {
            TempDataSet tds = tdt.NextTestSet();
            Temperature temp = new Temperature(tds.Celsius);

            Assert.AreEqual(true, ValuesEqual(tds.Fahrenheit, temp.ValueAsFahrenheit));
        }

        [Test]
        [Category("Prosperities")]
        public void ProperitieValueAsKelvinTest()
        {
            TempDataSet tds = tdt.NextTestSet();
            Temperature temp = new Temperature(tds.Celsius);

            Assert.AreEqual(true, ValuesEqual(tds.Kelvin, temp.ValueAsKelvin));
        }

        [Test]
        [Category("Prosperities")]
        public void ProperitieValueAsRankineTest()
        {
            TempDataSet tds = tdt.NextTestSet();
            Temperature temp = new Temperature(tds.Celsius);

            Assert.AreEqual(true, ValuesEqual(tds.Rankine, temp.ValueAsRankine));
        }

        #endregion

        #region Implicit Operators Test Sets
       
        [Test]
        [Category("Implicit")]
        public void ImplicitByteToTemperature()
        {
            Byte b = 45;
            Temperature temp = b;        
            Assert.AreEqual(45, temp.Value);
        }

        [Test]
        [Category("Implicit")]
        public void ImplicitSByteToTemperature()
        {
            SByte b = 45;
            Temperature temp = b;
            Assert.AreEqual(45, temp.Value);
        }

        [Test]
        [Category("Implicit")]
        public void ImplicitUInt16ToTemperature()
        {
            UInt16 ui = UInt16.MaxValue;
            Temperature temp = ui;
            Assert.AreEqual(ui, temp.Value);
        }

        [Test]
        [Category("Implicit")]
        public void ImplicitUInt32ToTemperature()
        {
            UInt32 ui = UInt32.MaxValue;
            Temperature temp = ui;
            Assert.AreEqual(ui, temp.Value);
        }

        [Test]
        [Category("Implicit")]
        public void ImplicitUInt64ToTemperature()
        {
            UInt64 ui = UInt64.MaxValue;
            Temperature temp = ui;
            Assert.AreEqual(ui, temp.Value);
        }

        [Test]
        [Category("Implicit")]
        public void ImplicitInt16ToTemperature()
        {
            Int16 ui = Int16.MaxValue;
            Temperature temp = ui;
            Assert.AreEqual(ui, temp.Value);
        }

        [Test]
        [Category("Implicit")]
        public void ImplicitInt32ToTemperature()
        {
            Int32 ui = Int32.MaxValue;
            Temperature temp = ui;
            Assert.AreEqual(ui, temp.Value);
        }

        [Test]
        [Category("Implicit")]
        public void ImplicitInt64ToTemperature()
        {
            Int64 ui = Int64.MaxValue;
            Temperature temp = ui;
            Assert.AreEqual(ui, temp.Value);
        }

        [Test]
        [Category("Implicit")]
        public void ImplicitSingleToTemperature()
        {
            Single ui = 44.56f;
            Temperature temp = ui;
            Assert.AreEqual(true, ValuesEqual(ui, temp.Value));
        }

        [Test]
        [Category("Implicit")]
        public void ImplicitDecimalToTemperature()
        {
            Decimal ui = 44.56M;
            Temperature temp = ui;
            Assert.AreEqual(true, ValuesEqual(ui, temp.Value));
        }

        [Test]
        [Category("Implicit")]
        public void ImplicitDoubleToTemperature()
        {
            Double ui = 44.56;
            Temperature temp = ui;
            Assert.AreEqual(true, ValuesEqual(ui, temp.Value));
        }
        
        [Test]
        [Category("Implicit From")]
        public void ImplicitByteFromTemperature()
        {
            Temperature temp = new Temperature(10);
            SByte b = temp;
            Assert.AreEqual(10, b);
        }

        [Test]
        [Category("Implicit From")]
        public void ImplicitSByteFromTemperature()
        {       
            Temperature temp = new Temperature(10);
            Byte b = temp;
            Assert.AreEqual(10, b);
        }

        [Test]
        [Category("Implicit From")]
        public void ImplicitUInt16FromTemperature()
        {
            Temperature temp = new Temperature(132);
            UInt16 u = temp;
            Assert.AreEqual(132, u);
        }

        [Test]
        [Category("Implicit From")]
        public void ImplicitUInt32FromTemperature()
        {
            Temperature temp = new Temperature(132);
            UInt32 u = temp;
            Assert.AreEqual(132, u);
        }

        [Test]
        [Category("Implicit From")]
        public void ImplicitUInt64FromTemperature()
        {
            Temperature temp = new Temperature(132);
            UInt64 u = temp;
            Assert.AreEqual(132, u);
        }

        [Test]
        [Category("Implicit From")]
        public void ImplicitInt16FromTemperature()
        {
            Temperature temp = new Temperature(132);
            Int16 u = temp;
            Assert.AreEqual(132, u);
        }

        [Test]
        [Category("Implicit From")]
        public void ImplicitInt32FromTemperature()
        {
            Temperature temp = new Temperature(132);
            Int32 u = temp;
            Assert.AreEqual(132, u);
        }

        [Test]
        [Category("Implicit From")]
        public void ImplicitInt64FromTemperature()
        {
            Temperature temp = new Temperature(132);
            Int64 u = temp;
            Assert.AreEqual(132, u);
        }

        [Test]
        [Category("Implicit From")]
        public void ImplicitSingleFromTemperature()
        {
            Temperature temp = new Temperature(33.467);
            Single u = temp;
            Assert.AreEqual(true, ValuesEqual(33.467, u));
        }

        [Test]
        [Category("Implicit From")]
        public void ImplicitDecimalFromTemperature()
        {
            Temperature temp = new Temperature(33.467);
            Decimal u = temp;
            Assert.AreEqual(true, ValuesEqual(33.467, u));
        }

        [Test]
        [Category("Implicit From")]
        public void ImplicitDoubleFromTemperature()
        {
            Temperature temp = new Temperature(33.467);
            Double u = temp;
            Assert.AreEqual(true, ValuesEqual(33.467, u));
        }

        #endregion

        #region Operators Test Sets

        [Test]
        [Category("Operators")]
        public void OperatorMinusA()
        {
            double A = 56;
            double B = 78;
            Temperature tempA = new Temperature(A);
            Temperature tempB = new Temperature(B);

            Temperature t = tempB - tempA;
      
            Assert.AreEqual(true, ValuesEqual(22, t));
        }

        [Test]
        [Category("Operators")]
        public void OperatorMinusB()
        {
            double A = 56;
            double B = 78;
            Temperature tempA = new Temperature(A, TemperatureScaleTypes.Fahrenheit);
            Temperature tempB = new Temperature(B, TemperatureScaleTypes.Kelvin);

            Temperature t = tempB - tempA;

            Assert.AreEqual(true, ValuesEqual(22, t.ValueAsCelsius));
        }

        [Test]
        [Category("Operators")]
        public void OperatorMinusC()
        {
            double A = 56;
            double B = 78;
            Temperature tempA = new Temperature(A, TemperatureScaleTypes.Fahrenheit);
            Temperature tempB = new Temperature(B, TemperatureScaleTypes.Kelvin);

            Temperature t = tempB - tempA;

            Assert.AreEqual(true, ValuesEqual(22, t.ValueAsCelsius));
        }

        [Test]
        [Category("Operators")]
        public void OperatorPlusA()
        {
            double A = 56;
            double B = 78;
            Temperature tempA = new Temperature(A);
            Temperature tempB = new Temperature(B);

            Temperature t = tempB + tempA;

            Assert.AreEqual(true, ValuesEqual(134, t));
        }

        [Test]
        [Category("Operators")]
        public void OperatorPlusB()
        {
            double A = 56;
            double B = 78;
            Temperature tempA = new Temperature(A, TemperatureScaleTypes.Fahrenheit);
            Temperature tempB = new Temperature(B, TemperatureScaleTypes.Rankine);

            Temperature t = tempB + tempA;

            Assert.AreEqual(true, ValuesEqual(134, t));
        }

        [Test]
        [Category("Operators")]
        public void OperatorPlusC()
        {
            double A = 56;
            double B = 78;
            Temperature tempA = new Temperature(A, TemperatureScaleTypes.Kelvin);
            Temperature tempB = new Temperature(B, TemperatureScaleTypes.Rankine);

            Temperature t = tempB + tempA;

            Assert.AreEqual(true, ValuesEqual(134, t));
        }

        [Test]
        [Category("Operators")]
        public void OperatorMinusIdenity()
        {
            double A = 56;
            Temperature tempA = new Temperature(A);
            

            Temperature t = 0 - tempA;
            Assert.AreEqual(true, ValuesEqual(-56, t));
        }

        [Test]
        [Category("Operators")]
        public void OperatorPlusIdentity()
        {

        }

        [Test]
        [Category("Operators")]
        public void OperatorMultiplyA()
        {
            double A = 56;
            double B = 78;
            Temperature tempA = new Temperature(A);
            Temperature tempB = new Temperature(B);

            Temperature t = tempB * tempA;

            Assert.AreEqual(true, ValuesEqual(4368, t));
        }

        [Test]
        [Category("Operators")]
        public void OperatorMultiplyB()
        {
            double A = 56;
            double B = 78;
            Temperature tempA = new Temperature(A,TemperatureScaleTypes.Fahrenheit);
            Temperature tempB = new Temperature(B, TemperatureScaleTypes.Kelvin);

            Temperature t = tempB * tempA;

            Assert.AreEqual(true, ValuesEqual(4368, t));
        }

        [Test]
        [Category("Operators")]
        public void OperatorMultiplyC()
        {
            double A = 56;
            double B = 78;
            Temperature tempA = new Temperature(A, TemperatureScaleTypes.Kelvin);
            Temperature tempB = new Temperature(B, TemperatureScaleTypes.Rankine);

            Temperature t = tempB * tempA;

            Assert.AreEqual(true, ValuesEqual(4368, t));
        }

        [Test]
        [Category("Operators")]
        public void OperatorDivideA()
        {
            double A = 56;
            double B = 78;
            Temperature tempA = new Temperature(A);
            Temperature tempB = new Temperature(B);

            Temperature t = tempB / tempA;
            Console.WriteLine("A {0}, B {0}", 1.3928, t);
            Assert.AreEqual(true, ValuesEqual(1.3928, t));

        }

        [Test]
        [Category("Operators")]
        public void OperatorDivideB()
        {
            double A = 56;
            double B = 78;
            Temperature tempA = new Temperature(A, TemperatureScaleTypes.Fahrenheit);
            Temperature tempB = new Temperature(B, TemperatureScaleTypes.Kelvin);

            Temperature t = tempB / tempA;
            Console.WriteLine("A {0}, B {0}", 1.3928, t);
            Assert.AreEqual(true, ValuesEqual(1.3928, t));

        }

        [Test]
        [Category("Operators")]
        public void OperatorDivideC()
        {
            double A = 56;
            double B = 78;
            Temperature tempA = new Temperature(A, TemperatureScaleTypes.Kelvin);
            Temperature tempB = new Temperature(B, TemperatureScaleTypes.Rankine);

            Temperature t = tempB / tempA;
            Console.WriteLine("A {0}, B {0}", 1.3928, t);
            Assert.AreEqual(true, ValuesEqual(1.3928, t));

        }

        [Test]
        [Category("Operators")]
        public void OperatorLessThanA()
        {
            double A = 56;
            double B = 78;
            Temperature tempA = new Temperature(A);
            Temperature tempB = new Temperature(B);

            bool b = tempB < tempA;
            Assert.AreEqual(false, b);

            b = tempA < tempB;
            Assert.AreEqual(true, b);
        }

        [Test]
        [Category("Operators")]
        public void OperatorLessThanB()
        {
            double A = 56;
            double B = 632.1;
            Temperature tempA = new Temperature(A, TemperatureScaleTypes.Fahrenheit);
            Temperature tempB = new Temperature(B, TemperatureScaleTypes.Rankine);

            bool b = tempB < tempA;
            Assert.AreEqual(false, b);

            b = tempA < tempB;
            Assert.AreEqual(true, b);
        }

        [Test]
        [Category("Operators")]
        public void OperatorLessThanC()
        {
            double A = 56;
            double B = 78;
            Temperature tempA = new Temperature(A, TemperatureScaleTypes.Rankine);
            Temperature tempB = new Temperature(B, TemperatureScaleTypes.Kelvin);

            bool b = tempB < tempA;
            Assert.AreEqual(false, b);

            b = tempA < tempB;
            Assert.AreEqual(true, b);
        }

        [Test]
        [Category("Operators")]
        public void OperatorGreatherThanA()
        {
            double A = 56;
            double B = 78;
            Temperature tempA = new Temperature(A);
            Temperature tempB = new Temperature(B);

            bool b = tempB > tempA;
            Assert.AreEqual(true, b);

            b = tempA > tempB;
            Assert.AreEqual(false, b);
        }

        [Test]
        [Category("Operators")]
        public void OperatorGreatherThanB()
        {
            double A = 56;
            double B = 78;
            Temperature tempA = new Temperature(A, TemperatureScaleTypes.Rankine);
            Temperature tempB = new Temperature(B, TemperatureScaleTypes.Fahrenheit);

            bool b = tempB > tempA;
            Assert.AreEqual(true, b);

            b = tempA > tempB;
            Assert.AreEqual(false, b);
        }

        [Test]
        [Category("Operators")]
        public void OperatorGreatherThanC()
        {
            double A = 56;
            double B = 78;
            Temperature tempA = new Temperature(A, TemperatureScaleTypes.Rankine);
            Temperature tempB = new Temperature(B, TemperatureScaleTypes.Kelvin);

            bool b = tempB > tempA;
            Assert.AreEqual(true, b);

            b = tempA > tempB;
            Assert.AreEqual(false, b);
        }


        [Test]
        [Category("Operators")]
        public void OperatorEqualEqualA()
        {
            double A = 56;
            double B = 78;
            Temperature tempA = new Temperature(A);
            Temperature tempB = new Temperature(B);

            bool b = tempB == tempA;
            Assert.AreEqual(false, b);

        }

        [Test]
        [Category("Operators")]
        public void OperatorEqualEqualB()
        {
            double A = 56;
            double B = 78;
            Temperature tempA = new Temperature(A, TemperatureScaleTypes.Kelvin);
            Temperature tempB = new Temperature(B, TemperatureScaleTypes.Rankine);

            bool b = tempB == tempA;
            Assert.AreEqual(false, b);

        }

        [Test]
        [Category("Operators")]
        public void OperatorEqualEqualC()
        {
            double A = 56;
            double B = 78;
            Temperature tempA = new Temperature(A, TemperatureScaleTypes.Rankine);
            Temperature tempB = new Temperature(B, TemperatureScaleTypes.Fahrenheit);

            bool b = tempB == tempA;
            Assert.AreEqual(false, b);

        }

        [Test]
        [Category("Operators")]
        public void OperatorMinusNotEqualA()
        {
            double A = 56;
            double B = 78;
            Temperature tempA = new Temperature(A);
            Temperature tempB = new Temperature(B);

            bool b = tempB != tempA;
            Assert.AreEqual(true, b);

        }

        [Test]
        [Category("Operators")]
        public void OperatorMinusNotEqualB()
        {
            double A = 56;
            double B = 78;
            Temperature tempA = new Temperature(A, TemperatureScaleTypes.Fahrenheit);
            Temperature tempB = new Temperature(B, TemperatureScaleTypes.Rankine);

            bool b = tempB != tempA;
            Assert.AreEqual(true, b);

        }

        [Test]
        [Category("Operators")]
        public void OperatorMinusNotEqualC()
        {
            double A = 56;
            double B = 78;
            Temperature tempA = new Temperature(A, TemperatureScaleTypes.Fahrenheit);
            Temperature tempB = new Temperature(B, TemperatureScaleTypes.Kelvin);

            bool b = tempB != tempA;
            Assert.AreEqual(true, b);

        }

        [Test]
        [Category("Operators")]
        public void OperatorGreaterThanEqualA()
        {
            double A = 56;
            double B = 78;
            Temperature tempA = new Temperature(A);
            Temperature tempB = new Temperature(B);

            bool b = tempB >= tempA;
            Assert.AreEqual(true, b);

            b = tempA >= tempB;
            Assert.AreEqual(false, b);
        }

        [Test]
        [Category("Operators")]
        public void OperatorGreaterThanEqualB()
        {
            double A = 56;
            double B = 78;
            Temperature tempA = new Temperature(A, TemperatureScaleTypes.Kelvin);
            Temperature tempB = new Temperature(B, TemperatureScaleTypes.Fahrenheit);

            bool b = tempB >= tempA;
            Assert.AreEqual(true, b);

            b = tempA >= tempB;
            Assert.AreEqual(false, b);
        }

        [Test]
        [Category("Operators")]
        public void OperatorGreaterThanEqualC()
        {
            double A = 56; // 13.33 c
            double B = 632.1; // 78 c
            Temperature tempA = new Temperature(A, TemperatureScaleTypes.Fahrenheit);
            Temperature tempB = new Temperature(B, TemperatureScaleTypes.Rankine);

            bool b = tempB >= tempA;
            Assert.AreEqual(true, b);

            b = tempA >= tempB;
            Assert.AreEqual(false, b);
        }

        [Test]
        [Category("Operators")]
        public void OperatorLessThanEqualA()
        {
            double A = 56;
            double B = 78;
            Temperature tempA = new Temperature(A);
            Temperature tempB = new Temperature(B);

            bool b = tempB <= tempA;
            Assert.AreEqual(false, b);

            b = tempA <= tempB;
            Assert.AreEqual(true, b);
        }

        [Test]
        [Category("Operators")]
        public void OperatorLessThanEqualB()
        {
            double A = 56;
            double B = 78;
            Temperature tempA = new Temperature(A, TemperatureScaleTypes.Rankine);
            Temperature tempB = new Temperature(B, TemperatureScaleTypes.Fahrenheit);

            bool b = tempB <= tempA;
            Assert.AreEqual(false, b);

            b = tempA <= tempB;
            Assert.AreEqual(true, b);
        }

        [Test]
        [Category("Operators")]
        public void OperatorLessThanEqualC()
        {
            double A = 56;
            double B = 78;
            Temperature tempA = new Temperature(A, TemperatureScaleTypes.Rankine);
            Temperature tempB = new Temperature(B, TemperatureScaleTypes.Kelvin);

            bool b = tempB <= tempA;
            Assert.AreEqual(false, b);

            b = tempA <= tempB;
            Assert.AreEqual(true, b);
        }

        #endregion

        #region Public Method Test Sets

        [Test]
        [Category("Methods")]
        public void ChangeScaleTemerature()
        {
            TempDataSet tds = tdt.NextTestSet();

            Temperature temp1 = new Temperature(tds.Fahrenheit);
            temp1.ChangeScale(TemperatureScaleTypes.Fahrenheit);
            Assert.AreEqual(true, ValuesEqual(tds.Fahrenheit, temp1.Value));

            Temperature temp2 = new Temperature(tds.Kelvin);
            temp2.ChangeScale(TemperatureScaleTypes.Kelvin);
            Assert.AreEqual(true, ValuesEqual(tds.Kelvin, temp2.Value));

            Temperature temp3 = new Temperature(tds.Rankine);
            temp3.ChangeScale(TemperatureScaleTypes.Rankine);
            Assert.AreEqual(true, ValuesEqual(tds.Rankine, temp3.Value));

            Temperature temp4 = new Temperature(tds.Celsius);
            temp4.ChangeScale(TemperatureScaleTypes.Celsius);
            Assert.AreEqual(true, ValuesEqual(tds.Celsius, temp4.Value));
        }

        [Test]
        [Category("Methods")]
        public void ChangeScaleValueTemerature()
        {
            TempDataSet tds = tdt.NextTestSet();

            Temperature temp1 = new Temperature(586);
            temp1.ChangeScale(tds.Fahrenheit, TemperatureScaleTypes.Fahrenheit);
            Assert.AreEqual(true, ValuesEqual(tds.Fahrenheit, temp1.Value));

            Temperature temp2 = new Temperature(796);
            temp2.ChangeScale(tds.Kelvin, TemperatureScaleTypes.Kelvin);
            Assert.AreEqual(true, ValuesEqual(tds.Kelvin, temp2.Value));

            Temperature temp3 = new Temperature(854.2);
            temp3.ChangeScale(tds.Rankine, TemperatureScaleTypes.Rankine);
            Assert.AreEqual(true, ValuesEqual(tds.Rankine, temp3.Value));

            Temperature temp4 = new Temperature(923.339);
            temp4.ChangeScale(tds.Celsius, TemperatureScaleTypes.Celsius);
            Assert.AreEqual(true, ValuesEqual(tds.Celsius, temp4.Value));
        }

        [Test]
        [Category("Methods")]
        public void Equals()
        {
            TempDataSet tds = tdt.NextTestSet();

            Temperature temp1 = new Temperature(tds.Celsius);
            Temperature temp2 = new Temperature();
            temp2.ChangeScale(tds.Rankine, TemperatureScaleTypes.Rankine);
                       
            Assert.AreEqual(true, temp1.Equals(Math.Round(temp2.ValueAsCelsius, 4)));

            temp2.ChangeScale(-454, TemperatureScaleTypes.Fahrenheit);

            Assert.AreEqual(false, temp1.Equals(temp2));
        }

        [Test]
        [Category("Methods")]
        public void TempGetHashCode()
        {
            TempDataSet tds = tdt.NextTestSet();
            int hash1 = 0;
            int hash2 = 0;

            Temperature temp1 = new Temperature(tds.Celsius);
            Temperature temp2 = new Temperature();
            temp2.ChangeScale(tds.Kelvin, TemperatureScaleTypes.Rankine);

            hash1 = temp1.GetHashCode();
            hash2 = temp2.GetHashCode();
            Assert.AreEqual(false, hash1 == hash2);

            temp2.ChangeScale(tds.Celsius, TemperatureScaleTypes.Celsius);
            hash2 = temp2.GetHashCode();

            Assert.AreEqual(true, hash1 == hash2);
        }

        [Test]
        [Category("Methods")]
        public void TryParseValueOnly()
        {
            TempDataSet tds = tdt.NextTestSet();
            Temperature temp1 = new Temperature();
            bool result = Temperature.TryParse(tds.Celsius.ToString(), out temp1);

            // parse with just value only
            Assert.AreEqual(true, result);
            Assert.AreEqual(true, ValuesEqual(temp1.Value,tds.Celsius));

        }

        [Test]
        [Category("Methods")]
        public void TryParseNoDegreeSymbol_A()
        {
            Temperature temp1 = new Temperature();
            string valueC = "76.56104 C";
            string valueF = "76.56104 F"; // 24.756133333°C
            string valueK = "76.56104 K"; // -196.58896°C
            string valueR = "76.56104 R"; // -230.61608889
            double tempC = 76.56104;
            double tempF = 24.75613;
            double tempK = -196.58896;
            double tempR = -230.61608;
            bool result = false;

            result = Temperature.TryParse(valueC, out temp1);
            Assert.AreEqual(true, result);
            Assert.AreEqual(true, ValuesEqual(temp1.Value, tempC));

            result = Temperature.TryParse(valueF, out temp1);
            temp1.ChangeScale(TemperatureScaleTypes.Fahrenheit);
            Assert.AreEqual(true, result);
            Assert.AreEqual(true, ValuesEqual(temp1.Value, tempF));

            result = Temperature.TryParse(valueK, out temp1);
            temp1.ChangeScale(TemperatureScaleTypes.Kelvin);
            Assert.AreEqual(true, result);
            Assert.AreEqual(true, ValuesEqual(temp1.Value, tempK));
        
            result = Temperature.TryParse(valueR, out temp1);
            temp1.ChangeScale(TemperatureScaleTypes.Rankine);
            Assert.AreEqual(true, result);
            Assert.AreEqual(true, ValuesEqual(temp1.Value, tempR));

        }

        [Test]
        [Category("Methods")]
        public void TryParseNoDegreeSymbol_B()
        {
            Temperature temp1 = new Temperature();
            string valueC = "76.56104C";
            string valueF = "76.56104F";
            string valueK = "76.56104K";
            string valueR = "76.56104R";
            double tempC = 76.56104;
            double tempF = 24.75613;
            double tempK = -196.58896;
            double tempR = -230.61608;
            bool result = false;

            result = Temperature.TryParse(valueC, out temp1);
            Assert.AreEqual(true, result);
            Assert.AreEqual(true, ValuesEqual(temp1.Value, tempC));

            result = Temperature.TryParse(valueF, out temp1);
            temp1.ChangeScale(TemperatureScaleTypes.Fahrenheit);
            Assert.AreEqual(true, result);
            Assert.AreEqual(true, ValuesEqual(temp1.Value, tempF));

            result = Temperature.TryParse(valueK, out temp1);
            temp1.ChangeScale(TemperatureScaleTypes.Kelvin);
            Assert.AreEqual(true, result);;
            Assert.AreEqual(true, ValuesEqual(temp1.Value, tempK));

            result = Temperature.TryParse(valueR, out temp1);
            temp1.ChangeScale(TemperatureScaleTypes.Rankine);
            Assert.AreEqual(true, result);
            Assert.AreEqual(true, ValuesEqual(temp1.Value, tempR));

        }

        [Test]
        [Category("Methods")]
        public void TryParseWithDegreeSymbol()
        {
            Temperature temp1 = new Temperature();
            string valueC = "76.56104 °C";
            string valueF = "76.56104 °F";
            string valueK = "76.56104 °K";
            string valueR = "76.56104 °R";
            double tempC = 76.56104;
            double tempF = 24.75613;
            double tempK = -196.58896;
            double tempR = -230.61608;
            bool result = false;

            result = Temperature.TryParse(valueC, out temp1);
            Assert.AreEqual(true, result);
            Assert.AreEqual(true, ValuesEqual(temp1.Value, tempC));

            result = Temperature.TryParse(valueF, out temp1);
            temp1.ChangeScale(TemperatureScaleTypes.Fahrenheit);
            Assert.AreEqual(true, result);
            Assert.AreEqual(true, ValuesEqual(temp1.Value, tempF));

            result = Temperature.TryParse(valueK, out temp1);
            temp1.ChangeScale(TemperatureScaleTypes.Kelvin);
            Assert.AreEqual(true, result);
            Assert.AreEqual(true, ValuesEqual(temp1.Value, tempK));

            result = Temperature.TryParse(valueR, out temp1);
            temp1.ChangeScale(TemperatureScaleTypes.Rankine);
            Assert.AreEqual(true, result);
            Assert.AreEqual(true, ValuesEqual(temp1.Value, tempR));
        }

        [Test]
        [Category("Methods")]
        public void TempToString()
        {
            Temperature temp = new Temperature();
            double tempS = 76.56104;       
           
            temp = tempS;
            temp.ChangeScale(TemperatureScaleTypes.Rankine);
            string result = temp.ToString();
            Assert.AreEqual('°', result[result.Length - 2]);
            Assert.AreEqual('R', result[result.Length - 1]);

            temp = tempS;
            temp.ChangeScale(TemperatureScaleTypes.Kelvin);
            result = temp.ToString();
            Assert.AreEqual('°', result[result.Length - 2]);
            Assert.AreEqual('K', result[result.Length - 1]);

            temp = tempS;
            temp.ChangeScale(TemperatureScaleTypes.Fahrenheit);
            result = temp.ToString();
            Assert.AreEqual('°', result[result.Length - 2]);
            Assert.AreEqual('F', result[result.Length - 1]);

            temp = tempS;
            temp.ChangeScale(TemperatureScaleTypes.Celsius);
            result = temp.ToString();
            Assert.AreEqual('°', result[result.Length - 2]);
            Assert.AreEqual('C', result[result.Length - 1]);

        }

        [Test]
        [Category("Methods")]
        public void ToStringFormatParameterS()
        {
            Temperature temp = new Temperature();
            double tempS = 76.56104;

            temp = tempS;
            temp.ChangeScale(TemperatureScaleTypes.Rankine);
            string result = temp.ToString("S");
            Assert.AreEqual(' ', result[result.Length - 2]);
            Assert.AreEqual('R', result[result.Length - 1]);

            temp = tempS;
            temp.ChangeScale(TemperatureScaleTypes.Kelvin);
            result = temp.ToString("S");
            Assert.AreEqual(' ', result[result.Length - 2]);
            Assert.AreEqual('K', result[result.Length - 1]);

            temp = tempS;
            temp.ChangeScale(TemperatureScaleTypes.Fahrenheit);
            result = temp.ToString("S");
            Assert.AreEqual(' ', result[result.Length - 2]);
            Assert.AreEqual('F', result[result.Length - 1]);

            temp = tempS;
            temp.ChangeScale(TemperatureScaleTypes.Celsius);
            result = temp.ToString("S");
            Assert.AreEqual(' ', result[result.Length - 2]);
            Assert.AreEqual('C', result[result.Length - 1]);
        }

        [Test]
        [Category("Methods")]
        public void ToStringFormatParameterSD()
        {
            // Temperature to string default is SD
            // See test: TempToString()

            Assert.AreEqual(true, true);
        }

        [Test]
        [Category("Methods")]
        public void ToStringFormatParameterEmptyString()
        {
            Temperature temp = new Temperature();
            double tempS = 76.56104;

            temp = tempS;
            temp.ChangeScale(TemperatureScaleTypes.Rankine);
            string result = temp.ToString("N");
            Assert.AreNotEqual('R', result[result.Length - 1]);

            temp = tempS;
            temp.ChangeScale(TemperatureScaleTypes.Kelvin);
            result = temp.ToString("N");
            Assert.AreNotEqual('K', result[result.Length - 1]);

            temp = tempS;
            temp.ChangeScale(TemperatureScaleTypes.Fahrenheit);
            result = temp.ToString("N");
            Assert.AreNotEqual('F', result[result.Length - 1]);

            temp = tempS;
            temp.ChangeScale(TemperatureScaleTypes.Celsius);
            result = temp.ToString("N");
            Assert.AreNotEqual('C', result[result.Length - 1]);

        }

        #endregion

        #region Interface Implementation Test Sets

        [Test]
        [Category("Interface")]
        public void TempCompareToObject()
        {
            Temperature temp = new Temperature();
            double tempS = 76.56104;
            temp = tempS;
            int result = temp.CompareTo(76.56104);
            Assert.AreEqual(0, result);

            result = temp.CompareTo(45.67);
            Assert.AreEqual(1, result);

            result = temp.CompareTo(76.99999);
            Assert.AreEqual(-1, result);
        }

        [Test]
        [Category("Interface")]
        public void TempCompareToTemerature()
        {
            Temperature temp = new Temperature();
            Temperature tempC = new Temperature();
            double tempS = 76.56104;
            temp = tempS;
            tempC = tempS;
            int result = temp.CompareTo(tempC);
            Assert.AreEqual(0, result);

            tempC = 46.98;
            result = temp.CompareTo(tempC);
            Assert.AreEqual(1, result);

            tempC = 76.99999;
            result = temp.CompareTo(tempC);
            Assert.AreEqual(-1, result);
        }

        [Test]
        [Category("Interface")]
        public void TempEqualsTemerature()
        {
            Temperature temp = new Temperature();
            Temperature tempC = new Temperature();
            double tempS = 76.56104;
            temp = tempS;
            tempC = tempS;

            Assert.AreEqual(true, temp.Equals(tempC));

            tempC = 76.4;
            Assert.AreEqual(false, temp.Equals(tempC));
        }

        [Test]
        [Category("Interface")]
        public void TempGetTypeCode()
        {
            Temperature temp = new Temperature();
            Assert.AreEqual(TypeCode.Object, temp.GetTypeCode());
            
        }

        [Test]
        [Category("Interface")]
        public void TempToBoolean()
        {
            Temperature temp = new Temperature();
            Assert.Throws(typeof(NotSupportedException),
                delegate { temp.ToBoolean(CultureInfo.InvariantCulture.NumberFormat); });
        }

        [Test]
        [Category("Interface")]
        public void TempToChar()
        {
            Temperature temp = new Temperature();
            Assert.Throws(typeof(NotSupportedException),
                delegate { temp.ToChar(CultureInfo.InvariantCulture.NumberFormat); });
        }

        [Test]
        [Category("Interface")]
        public void TempToDateTime()
        {
            Temperature temp = new Temperature();
            Assert.Throws(typeof(NotSupportedException),
                delegate { temp.ToDateTime(CultureInfo.InvariantCulture.NumberFormat); });
        }

        [Test]
        [Category("Interface")]
        public void TempToDecimal()
        {
            Temperature temp = new Temperature();
            temp = 44.6;

            Decimal result = temp.ToDecimal(CultureInfo.InvariantCulture.NumberFormat);
            Assert.AreEqual(true, ValuesEqual(temp.Value, result));
        }

        [Test]
        [Category("Interface")]
        public void TempToInt16()
        {
            Temperature temp = new Temperature();
            temp = 44.6;

            Int16 result = temp.ToInt16(CultureInfo.InvariantCulture.NumberFormat);
            Assert.AreEqual((int)temp.Value, result);
        }

        [Test]
        [Category("Interface")]
        public void TempToInt32()
        {
            Temperature temp = new Temperature();
            temp = 44.6;

            Int32 result = temp.ToInt32(CultureInfo.InvariantCulture.NumberFormat);
            Assert.AreEqual((int)temp.Value, result);
        }

        [Test]
        [Category("Interface")]
        public void TempToInt64()
        {
            Temperature temp = new Temperature();
            temp = 44.6;

            Int64 result = temp.ToInt64(CultureInfo.InvariantCulture.NumberFormat);
            Assert.AreEqual((int)temp.Value, result);
        }
        

        [Test]
        [Category("Interface")]
        public void TempToSByte()
        {
            Temperature temp = new Temperature();
            temp = 44.6;

            SByte result = temp.ToSByte(CultureInfo.InvariantCulture.NumberFormat);
            Assert.AreEqual((int)temp.Value, result);
        }

        [Test]
        [Category("Interface")]
        public void TempToSingle()
        {
            Temperature temp = new Temperature();
            temp = 44.6;

            Single result = temp.ToSingle(CultureInfo.InvariantCulture.NumberFormat);
            Assert.AreEqual(true, ValuesEqual(temp.Value, result));
        }

        [Test]
        [Category("Interface")]
        public void TempToType()
        {
            Temperature temp = new Temperature();
            Assert.Throws(typeof(NotSupportedException),
                delegate { temp.ToType(typeof(Temperature),CultureInfo.InvariantCulture.NumberFormat); });
        }

        [Test]
        [Category("Interface")]
        public void TempToUInt16()
        {
            Temperature temp = new Temperature();
            temp = 44.6;

            UInt16 result = temp.ToUInt16(CultureInfo.InvariantCulture.NumberFormat);
            Assert.AreEqual((int)temp.Value, result);
        }

        [Test]
        [Category("Interface")]
        public void TempToUInt32()
        {
            Temperature temp = new Temperature();
            temp = 44.6;

            UInt32 result = temp.ToUInt32(CultureInfo.InvariantCulture.NumberFormat);
            Assert.AreEqual((int)temp.Value, result);
        }

        [Test]
        [Category("Interface")]
        public void TempToUInt64()
        {
            Temperature temp = new Temperature();
            temp = 44.6;

            UInt64 result = temp.ToUInt64(CultureInfo.InvariantCulture.NumberFormat);
            Assert.AreEqual((int)temp.Value, result);
        }

        #endregion

    }
}
