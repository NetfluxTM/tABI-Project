﻿using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace tABI_Project.Signal_Processing
{
    public class DataPoint
    {
        // TO DO: Make this the main object to hold all the signal data, gui parameters, and methods
        //          Change _data into a list of double data points, remove _time, add methods to convert gui parameters
        //          from string to whatever variable type they should be.



        // Class members (Fields, properties, methods, instance constructor)

        // Fields
        private double _data = 0;
        private double _time = 0;
        public List<Double> properData = new List<Double>();


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
        /*   
         *  Used in the Decimate function to read a line from the .csv file, and create a new DataPoint instance
         */
        public static DataPoint FromCsv(string csvLine)
        {
            string[] line_segments = csvLine.Split(','); // Cuts each line into multiple line segments. (separates at each ',' could change to ';' or whatever else)
            for (int i = 0; i < line_segments.Length; i++) // "for each column"
            {
                line_segments[i] = line_segments[i].Trim(); // This removes whitespace from each string in the array.
            }
            DataPoint dataPoint = new DataPoint();
            dataPoint._data = Convert.ToDouble(line_segments[0]);
            dataPoint._time = Convert.ToDouble(line_segments[1]);
            return dataPoint;
        }

        public void Decimate(string readPath, int decimationRate)
        {   // Incoming data should be 2 columns and unspecified rows.

            // Read CSV file to list of datapoints (data and time)
            List<DataPoint> data = File.ReadAllLines(readPath)  // reads all lines from the CSV file into a string array
                                       .Skip(1)   // Use to skip header lines in csv file. Comment out if not needed.
                                       .Select(v => DataPoint.FromCsv(v))   // uses Linq to select each line and create a new DataPoint instance using the FromCsv method
                                                                            // (This creates a System.Collections.Generic.IEnumerable<Signal_Processing.DataPoint> type)
                                       .ToList(); // converts the IEnumerable to a List

            // "data" is now a list of datapoints, each datapoint contains two doubles to hold the value of both columns from the CSV.

            //List<DataPoint> decimatedData = new List<DataPoint>();

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

                    //decimatedData.Add(avg);
                    properData.Add(avg.data);
                }
            }
            string writePath = @"D:\Libraries\OneDrive\Documents\Visual Studio 2019\Repos\tABI Project\Resources\Test_Data\Decimated.txt"; // Temporary location until added to "settings"

            File.WriteAllLines( // Write properData (list of doubles) to a text file
                writePath,
                properData.ToArray()
                          .Select(v => v.ToString())
                );
        }

        public double[] Filter(double[] a, double[] b, double[] signal)
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
    }
}
