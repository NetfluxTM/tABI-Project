using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

// Get TextBoxes to only accept numeric input:
// https://stackoverflow.com/questions/1268552/how-do-i-get-a-textbox-to-only-accept-numeric-input-in-wpf

// Do we want the main menu parameters to be saved between program sessions? Here's how:
// https://stackoverflow.com/questions/2704516/how-to-save-user-inputed-value-in-textbox-wpf-xaml

namespace tABI_Project
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindowViewModel CurrentModel = new MainWindowViewModel();

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = CurrentModel;
        }

        private void btnBegin_Click(object sender, RoutedEventArgs e)
        {
            var graphWindow = new GraphWindow();
            graphWindow.Show();
        }

        /* Not necessary to update 
        private void DecRate_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        */
    }


    // Below code from https://stackoverflow.com/questions/56952535/how-to-save-wpf-textbox-value-to-variable
    // and https://wpf-tutorial.com/data-binding/responding-to-changes/

    public class MainWindowViewModel : INotifyPropertyChanged
    {
        // Fields
        private string _GraphUpperFTR;
        private string _GraphLowerFTR;
        private string _GraphUpperSTR;
        private string _GraphLowerSTR;
        private string _GraphUpperCMR;
        private string _GraphLowerCMR;
        private string _GraphChannel;
        private string _DecRate;
        //private string AvgRate    // No longer needed?
        private string _SlowFilterType;
        private string _SlowFilterUpper;
        private string _SlowFilterLower;
        private string _FastFilterType;
        private string _FastFilterUpper;
        private string _FastFilterLower;



        // Methods

        // Declare the event
        public event PropertyChangedEventHandler PropertyChanged;

        // Create the OnPropertyChanged method to raise the event
        // (Wrap the event in a protected virtual method to enable derived classes to raise the event.)
        protected virtual void OnPropertyChanged(string name)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(name));
                // Could also remove the if statement and use "this.PropertyChange?.Invoke(this, new PropertyChangedEventArgs(name));"
                // "PropertyChange()" and "PropertyChange.Invoke()" are the same exact thing, but using the ?. operator allows us to 
                // raise the event in a thread-safe manner, since ?. works like normal member access, but returns null if the member trying to be accessed is null.
            }
        }


        // Properties
        // TODO: Add Other boxes too, such as decimation rate, etc.
        public string GraphUpperFTR
        {
            get { return this._GraphUpperFTR; }
            set
            {
                if (this._GraphUpperFTR != value)
                {
                    this._GraphUpperFTR = value;
                    this.OnPropertyChanged(nameof(this.GraphUpperFTR));
                }
            }
        }

        public string GraphLowerFTR
        {
            get { return this._GraphLowerFTR; }
            set
            {
                if (this._GraphLowerFTR != value)
                {
                    this._GraphLowerFTR = value;
                    this.OnPropertyChanged(nameof(this.GraphLowerFTR));
                }
            }
        }

        public string GraphUpperSTR
        {
            get { return this._GraphUpperSTR; }
            set
            {
                if (this._GraphUpperSTR != value)
                {
                    this._GraphUpperSTR = value;
                    this.OnPropertyChanged(nameof(this.GraphUpperSTR));
                }
            }
        }
 
        public string GraphLowerSTR
        {
            get { return this._GraphLowerSTR; }
            set
            {
                if (this._GraphLowerSTR != value)
                {
                    this._GraphLowerSTR = value;
                    this.OnPropertyChanged(nameof(this.GraphLowerSTR));
                }
            }
        }

        public string GraphUpperCMR
        {
            get { return this._GraphUpperCMR; }
            set
            {
                if (this._GraphUpperCMR != value)
                {
                    this._GraphUpperCMR = value;
                    this.OnPropertyChanged(nameof(this.GraphUpperCMR));
                }
            }
        }

        public string GraphLowerCMR
        {
            get { return this._GraphLowerCMR; }
            set
            {
                if (this._GraphLowerCMR != value)
                {
                    this._GraphLowerCMR = value;
                    this.OnPropertyChanged(nameof(this.GraphLowerCMR));
                }
            }
        }

        public string GraphChannel
        {
            get { return this._GraphChannel; }
            set
            {
                if (this._GraphChannel != value)
                {
                    this._GraphChannel = value;
                    this.OnPropertyChanged(nameof(this.GraphChannel));
                }
            }
        }

        public string DecRate
        {
            get { return this._DecRate; }
            set
            {
                if (this._DecRate!= value)
                {
                    this._DecRate = value;
                    this.OnPropertyChanged(nameof(this.DecRate));
                }
            }
        }

        public string SlowFilterType
        {
            get { return this._SlowFilterType; }
            set
            {
                if (this._SlowFilterType != value)
                {
                    this._SlowFilterType = value;
                    this.OnPropertyChanged(nameof(this.SlowFilterType));
                }
            }
        }

        public string SlowFilterUpper
        {
            get { return this._SlowFilterUpper; }
            set
            {
                if (this._SlowFilterUpper != value)
                {
                    this._SlowFilterUpper = value;
                    this.OnPropertyChanged(nameof(this.SlowFilterUpper));
                }
            }
        }

        public string SlowFilterLower
        {
            get { return this._SlowFilterLower; }
            set
            {
                if (this._SlowFilterLower != value)
                {
                    this._SlowFilterLower = value;
                    this.OnPropertyChanged(nameof(this.SlowFilterLower));
                }
            }
        }

        public string FastFilterType
        {
            get { return this._FastFilterType; }
            set
            {
                if (this._FastFilterType != value)
                {
                    this._FastFilterType = value;
                    this.OnPropertyChanged(nameof(this.FastFilterType));
                }
            }
        }

        public string FastFilterUpper
        {
            get { return this._FastFilterUpper; }
            set
            {
                if (this._FastFilterUpper != value)
                {
                    this._FastFilterUpper = value;
                    this.OnPropertyChanged(nameof(this.FastFilterUpper));
                }
            }
        }

        public string FastFilterLower
        {
            get { return this._FastFilterLower; }
            set
            {
                if (this._FastFilterLower != value)
                {
                    this._FastFilterLower = value;
                    this.OnPropertyChanged(nameof(this.FastFilterLower));
                }
            }
        }
    }
}
