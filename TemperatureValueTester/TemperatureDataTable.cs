using System;
using System.Data;
using System.Text;
using Ekstrand;

namespace TemperatureValueTester
{
    public class TemperatureDataTable
    {
        #region Class Global Varables and Objects

        DataTable m_TempTable = new DataTable();
        Random rnd = new Random();

        #region Temperature Data Arrays
        // the arrays are conversions from Celsius to F, K, R
        double[] Celsius = new double[]
        {
            0.23478,101.2987,0.02,120.9,-1.0001,-2.34,-100.08,500,490,480,470,460,450,440,430,420,410,400,
            390,380,370,360,350,340,330,320,310,300,290,280,270,260,250,240,230,220,
            210,200,190,180,170,160,150,140,130,120,110,100,90,80,70,60,50,40,
            30,20,10,0,-10.00,-20.00,-30.00,-40.00,-50.00,-60.00,-70.00,-80.00,-90.00,-100.00,-110.00,-120.00,-130.00,-140.00,
            -150.00,-160.00,-170.00,-180.00,-190.00,-200.00,-210.00,-220.00,-230.00,-240.00,-250.00,-260.00,-270.00,-273.15
        };

        double[] Fahrenheit = new double[]
        {
            32.422604,214.33766,32.036,249.62,30.19982,27.788,-148.144,932,914,896,878,860,842,824,806,788,770,752,
             734,716,698,680,662,644,626,608,590,572,554,536,518,500,482,464,446,428,
             410,392,374,356,338,320,302,284,266,248,230,212,194,176,158,140,122,104,
             86,68,50,32,14,-4.00,-22.00,-40.00,-58.00,-76.00,-94.00,-112.00,-130.00,-148.00,-166.00,-184.00,-202.00,-220.00,
             -238.00,-256.00,-274.00,-292.00,-310.00,-328.00,-346.00,-364.00,-382.00,-400.00,-418.00,-436.00,-454.00,-459.67
        };

        double[] Kelvin = new double[]
        {
            273.38478,374.4487,273.17,394.05,272.1499,270.81,173.07,773.15,763.15,753.15,743.15,733.15,723.15,713.15,703.15,693.15,683.15,673.15,
             663.15,653.15,643.15,633.15,623.15,613.15,603.15,593.15,583.15,573.15,563.15,553.15,543.15,533.15,523.15,513.15,503.15,493.15,
             483.15,473.15,463.15,453.15,443.15,433.15,423.15,413.15,403.15,393.15,383.15,373.15,363.15,353.15,343.15,333.15,323.15,313.15,
             303.15,293.15,283.15,273.15,263.15,253.15,243.15,233.15,223.15,213.15,203.15,193.15,183.15,173.15,163.15,153.15,143.15,133.15,
             123.15,113.15,103.15,93.15,83.15,73.15,63.15,53.15,43.15,33.15,23.15,13.15,3.15,0
        };

        double[] Rankine = new double[]
        {
            492.092604,674.00766,491.706,709.29,489.86982,487.458,311.526,1391.67,1373.67,1355.67,1337.67,1319.67,1301.67,1283.67,1265.67,1247.67,1229.67,1211.67,
             1193.67,1175.67,1157.67,1139.67,1121.67,1103.67,1085.67,1067.67,1049.67,1031.67,1013.67,995.67,977.67,959.67,941.67,923.67,905.67,887.67,
             869.67,851.67,833.67,815.67,797.67,779.67,761.67,743.67,725.67,707.67,689.67,671.67,653.67,635.67,617.67,599.67,581.67,563.67,
             545.67,527.67,509.67,491.67,473.67,455.67,437.67,419.67,401.67,383.67,365.67,347.67,329.67,311.67,293.67,275.67,257.67,239.67,
             221.67,203.67,185.67,167.67,149.67,131.67,113.67,95.67,77.67,59.67,41.67,23.67,5.67,0
        };
        #endregion

        #endregion

        public TemperatureDataTable()
        {
            CreateTable();
            PopulateTableRows();
        }

        #region Private Methods

        private void CreateTable()
        {
            DataColumn column;

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Double");
            column.ColumnName = "C";
            column.Unique = true;
            column.ReadOnly = false;
            m_TempTable.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Double");
            column.ColumnName = "F";
            column.Unique = true;
            column.ReadOnly = false;
            m_TempTable.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Double");
            column.ColumnName = "K";
            column.Unique = true;
            column.ReadOnly = false;
            m_TempTable.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Double");
            column.ColumnName = "R";
            column.Unique = true;
            column.ReadOnly = false;
            m_TempTable.Columns.Add(column);
            
        }

        private void PopulateTableRows()
        {
            DataRow row;

            for(int i = 0; i < Celsius.Length; i++)
            {
                row = m_TempTable.NewRow();
                row[0] = Celsius[i];
                row[1] = Fahrenheit[i];
                row[2] = Kelvin[i];
                row[3] = Rankine[i];
                m_TempTable.Rows.Add(row);
            }
        }

        #endregion

        #region Public Methods

        public double[] NextDataSet()
        {
            int index = rnd.Next(-1, Celsius.Length);

            if (index == Celsius.Length || index == -1)
            {
                return NextDataSet();
            }

            return new double[] { Celsius[index], Fahrenheit[index], Kelvin[index], Rankine[index] };
        }

        public TemperatureScaleTypes NextSecaler()
        {
            int index = rnd.Next(-1, 4);

            if (index == 4 || index == -1)
            {
                return NextSecaler();
            }

            switch(index)
            {
                case 0:
                    return TemperatureScaleTypes.Celsius;
                case 1:
                    return TemperatureScaleTypes.Fahrenheit;
                case 2:
                    return TemperatureScaleTypes.Kelvin;
                case 3:
                    return TemperatureScaleTypes.Rankine;
            }

            return TemperatureScaleTypes.Celsius;
        }

        public TempDataSet NextTestSet()
        {
            TempDataSet temp = new TempDataSet();
            double[] data = this.NextDataSet();
            temp.Celsius = data[0];
            temp.Fahrenheit = data[1];
            temp.Kelvin = data[2];
            temp.Rankine = data[3];
            temp.Scalar = this.NextSecaler();

            return temp;
        }
        #endregion

    }

    public struct TempDataSet
    {
        public double Celsius;
        public double Fahrenheit;
        public double Kelvin;
        public double Rankine;
        public int Index;
        public TemperatureScaleTypes Scalar;

        public double Value
        {
            get
            {
                switch (Scalar)
                {
                    case TemperatureScaleTypes.Celsius:
                        return Celsius;
                    case TemperatureScaleTypes.Fahrenheit:
                        return Fahrenheit;
                    case TemperatureScaleTypes.Kelvin:
                        return Kelvin;
                    case TemperatureScaleTypes.Rankine:
                        return Rankine;
                }

                return Celsius;
            }
        }
    }
}
