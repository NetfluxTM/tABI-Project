using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace tABI_Project
{
    class Signal_Processing
    {
        /*
        static void Main(string[] args)
        {
            string path = @"D:\Libraries\OneDrive\Documents\Visual Studio 2019\Repos\tABI Project\Resources\1noisySignal.csv";
            decimate(path, 2);
        }
        */

        
        private static double[] Filter(double[] a, double[] b, double[] signal)
        {
            double[] result = new double[signal.Length];

            for (int i = 0; i < signal.Length; ++i)
            {
                double tmp = 0.0;
                for (int j = 0; j < b.Length; ++j)
                {
                    if (i - j < 0) continue;
                    tmp += b[j] * signal[i - j];
                }
                for (int j = 1; j < a.Length; ++j)
                {
                    if (i - j < 0) continue;
                    tmp -= a[j] * result[i - j];
                }
                tmp /= a[0];
                result[i] = tmp;
            }
            return result;
        }

        public static void Decimate(string filePath, int decimationRate)
        {   // Incoming data should be 2 columns and unspecified rows.

            // Read CSV file to list of datapoints (data and time)
            List<DataPoint> data = File.ReadAllLines(filePath)  // reads all lines from the CSV file into a string array
                                       .Skip(1)   // Use to skip header lines in csv file. Comment out if not needed.
                                       .Select(v => DataPoint.FromCsv(v))   // uses Linq to select each line and create a new DataPoint instance using the FromCsv method
                                                                            // (This creates a System.Collections.Generic.IEnumerable<Signal_Processing.DataPoint> type)
                                       .ToList(); // converts the IEnumerable to a List

            // "data" is now a list of datapoints, each datapoint contains two doubles to hold the value of both columns from the CSV.

            List<DataPoint> decimatedData = new List<DataPoint>();

            for (int i = decimationRate; i <= data.Count; i++)
            {
                if ((i % decimationRate) == 0)
                {
                    DataPoint avg = new DataPoint();
                    for (int j = 0; j < decimationRate; j++)
                    {
                        avg.data = avg.data + data[i - 1 - j].data;
                        avg.time = avg.time + data[i - 1 - j].time;
                    }
                    avg.data = avg.data / decimationRate;
                    avg.time = avg.time / decimationRate;

                    decimatedData.Add(avg);
                }
            }

            foreach (DataPoint i in decimatedData)
            {
                Console.WriteLine(i.data);
                Console.WriteLine(i.time);
                Console.WriteLine("NEXT!");
            }

        }
    }

    public class DataPoint
    {
        // Class members (Fields, properties, methods, instance constructor)

        // Fields
        private double _data = 0;
        private double _time = 0;

        // Properties
        public double data
        {
            get { return _data; }
            set { _data = value; }
        }
        public double time
        {
            get { return _time; }
            set { _time = value; }
        }

        // Methods
        public static DataPoint FromCsv(string csvLine)
        {
            string[] line_segments = csvLine.Split(','); // Cuts each line into multiple line segments. (separates at each ',' could change to ';' or whatever else)
            for (int i = 0; i < line_segments.Length; i++) // "for each column"
            {   // line_segments.Length should always be 2.
                line_segments[i] = line_segments[i].Trim(); // This removes whitespace from each string in the array.
            }
            DataPoint dataPoint = new DataPoint();
            dataPoint._data = Convert.ToDouble(line_segments[0]);
            dataPoint._time = Convert.ToDouble(line_segments[1]);
            return dataPoint;
        }
    }
}
