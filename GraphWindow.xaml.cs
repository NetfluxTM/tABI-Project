using System;
using System.Windows;


namespace tABI_Project
{
    /// <summary>
    /// Interaction logic for GraphWindow.xaml
    /// </summary>
    public partial class GraphWindow : Window
    {
        public GraphWindow()
        {
            InitializeComponent();

            cbColors.SelectedIndex = 0;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            RenderContent();
        }

        private void Grid_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            RenderContent();
        }

        /// <summary>
        /// Renderss random heat map
        /// </summary>
        private void RenderContent()
        {
            cHeatMap.Clear();

            Random rRand = new Random();

            // Loop variables
            int x;
            int y;
            byte intense;

            // Lets loop few times and create a random point each iteration
            for (int i = 0; i < 1000; i++)
            {
                // Pick random locations and intensity
                x = rRand.Next(0, (int)(cHeatMap.ActualWidth - 1));
                y = rRand.Next(0, (int)(cHeatMap.ActualHeight - 1));
                intense = (byte)rRand.Next(0, 255);

                // Add heat point to heat points list
                cHeatMap.AddHeatPoint(new HeatPoint(x, y, intense));
            }

            cHeatMap.Render();
        }

        private void cbColors_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

        }
    }
}
