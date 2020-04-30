using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Windows;//For getting window forms. Used here for message boxes.
using System.Runtime.InteropServices;
/*note: for all the following library namespaces below,  Download the specific library. For example, to install the MathNet.Filtering Library 
through the NuGet Package Manager in visual studios, go to the Tools menu>NuGet Package Manager>Manage Packages For Solution. 
Once here, select the browse tab, and search for MathNet.Filtering option in the search. Select latest version and install*/
using MathNet.Filtering; //Filtering and other math operation library. See https://filtering.mathdotnet.com/api/ and https://filtering.mathdotnet.com/ for more details
using Accord.Math; // Math Library, includes useful functions like hilbert transform. 
using Accord.Controls; //Plotting library from accord. This is not necessary for the program but may help with debug. See http://accord-framework.net/docs/html/N_Accord_Controls.htm and http://accord-framework.net/docs/html/T_Accord_Controls_ScatterplotBox.htm for more info. 

using Accord; //General Namespace for all Accord libraries. Is needed for functionality of above libraries. 

namespace tABI_Project.Signal_Processing
{
    public class DataPoint
    {
        // TO DO: Make this the main object to hold all the signal data, gui parameters, and methods
        //          Change _data into a list of double data points, remove _time, add methods to convert gui parameters
        //          from string to whatever variable type they should be.



        // Class members (Fields, properties, methods, instance constructor)

        #region ∙ Fields ∙
        private double _data = 0;
        private double _time = 0;
        public List<Double> properData = new List<Double>();
        #endregion


        #region ∙ Properties ∙
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
        #endregion


        #region ∙ Methods ∙ 
        // FromCsv: Used in the Decimate function to read a line from the .csv file, and create a new DataPoint instance
        public static DataPoint FromCsv(string csvLine)
        {
            string[] line_segments = csvLine.Split(',');        // Cuts each line into multiple line segments. (separates at each ',' could change to ';' or whatever else)
            for (int i = 0; i < line_segments.Length; i++)      // "for each column"
            {
                line_segments[i] = line_segments[i].Trim();     // This removes whitespace from each string in the array.
            }
            DataPoint dataPoint = new DataPoint
            {
                _data = Convert.ToDouble(line_segments[0]),
                _time = Convert.ToDouble(line_segments[1])
            };
            return dataPoint;
        }

        public void Decimate(string readPath, int decimationRate)
        {   // Current configuration: Incoming data expected to be a csv with 2 columns and unspecified rows.
            // TO DO: Explain how to change for different amount of columns

            // Read CSV file to list of datapoints (data and time)
            List<DataPoint> data = File.ReadAllLines(readPath)  // reads all lines from the CSV file into a string array
                                       .Skip(1)   // Use to skip header lines in csv file. Comment out if not needed.
                                       .Select(v => DataPoint.FromCsv(v))   // uses Linq to select each line and create a new DataPoint instance using the FromCsv method
                                                                            // (This creates a System.Collections.Generic.IEnumerable<Signal_Processing.DataPoint> type)
                                       .ToList(); // converts the IEnumerable to a List

            // "data" is now a list of datapoints, each datapoint contains two doubles to hold the value of both columns from the CSV.

            //List<DataPoint> decimatedData = new List<DataPoint>(); // old, replaced List of datapoints with list of doubles for decimated data

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

                    //decimatedData.Add(avg); // old, replaced List of datapoints with list of doubles for decimated data
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

        public double[] Filter(double[] inputArray,MathNet.Filtering.ImpulseResponse responseType, int samplingFreq, int frequencyLow, int frequencyHigh,int q)//ResponseType: IIR or FIR, samplingFreq: Sampling frequency in Hz, frequencyLow: Low cuttoff for filter.
        {
            OnlineFilter bandpassFast = OnlineFilter.CreateBandpass(ImpulseResponse.Infinite, samplingFreq, frequencyLow, frequencyHigh, q);
            bandpassFast.ProcessSamples(inputArray);
            return inputArray;//Double check to see if we really need to return, if this is working on a referenced array, then the input array should already be altered, which would be this classes data frame. 

        }
        public double[] hilbertTransformBaseBand(double[] input)
        {
            int lengthIn = input.Length;
            if (lengthIn > 16384)
            {
               
                MessageBox.Show("Error: Input Length Too large or hilbert transform. Data will not be transformed.","Warning",MessageBoxButton.OK,MessageBoxImage.Error);//For gui, convert this to an error box. 
                return null;
            }
            else
            {

                int power = (int)Math.Ceiling(Math.Log10(lengthIn)/Math.Log10(2));
                int lengthFiller = (int)Math.Pow(2, power) - (int)lengthIn;
                double[] fillerArray = MathNet.Numerics.Generate.Step(lengthFiller, 0.0, 0);
                Array.Resize<double>(ref input, lengthIn + lengthFiller);
                Array.Copy(fillerArray, 0, input, lengthIn, lengthFiller);
                HilbertTransform.FHT(input, FourierTransform.Direction.Forward);
                return input;
            }

        }
        #endregion
    }
}
