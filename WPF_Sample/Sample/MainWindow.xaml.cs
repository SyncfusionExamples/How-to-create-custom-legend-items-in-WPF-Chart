using Syncfusion.UI.Xaml.Charts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
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
using System.Collections;
using System.Reflection;

namespace Sample1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<int> toggleIndexes;
        public MainWindow()
        {
            InitializeComponent();

            series1.ItemsSource = viewModel.Data;
            series2.ItemsSource = viewModel.Data;
            series1.ColorModel = new ChartColorModel();
            ChartLegend legend = new ChartLegend();
            toggleIndexes = new List<int>();

            var data = viewModel.Data;
            for (int i = 0; i < data.Count; i++)
            {
                legend.Items.Add(new LegendItem()
                {
                    Item = data[i],
                    Series = series1,
                    IconHeight = 10,
                    IconWidth = 10,
                    IconVisibility = Visibility.Visible,
                    Interior = series1.ColorModel.GetBrush(i),
                    CheckBoxVisibility = Visibility.Visible
                });
            }

            legend.ItemTemplate = grid.Resources["itemTemplate"] as DataTemplate;
            chart.Legend = legend;
         
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox box = sender as CheckBox;
            LegendItem item = box.Tag as LegendItem;

            UpdateLegend(true, item.Item as Model);
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            CheckBox box = sender as CheckBox;
            LegendItem item = box.Tag as LegendItem;

            UpdateLegend(false, item.Item as Model);
        }

        private void UpdateLegend(bool isChecked, Model item)
        {
            foreach (PieSeriesExt series in chart.Series)
            {
                if (isChecked)
                    toggleIndexes.Remove(viewModel.Data.IndexOf(item));
                else
                    toggleIndexes.Add(viewModel.Data.IndexOf(item));

                series.GetType().GetField("ToggledLegendIndex", BindingFlags.NonPublic |
                    BindingFlags.Instance).SetValue(series, toggleIndexes);
            }

            UpdateArea();
        }

        private void UpdateArea()
        {
            MethodInfo info = chart.GetType().GetMethod("UpdateArea",
                               BindingFlags.NonPublic | BindingFlags.Instance,
                               null,
                               new Type[] { typeof(bool) },
                               null);

            info?.Invoke(chart, new object[] { true });
        }
    }
    public class Model
    {
        public string XValue { get; set; }
        public double YValue { get; set; }
    }

        public class ViewModel
        {
            public ViewModel()
            {
                GenerateData();
            }

            public void GenerateData()
            {
                Data = new ObservableCollection<Model>();
                Random rd = new Random();
                for (int i = 0; i < 6; i++)
                {
                    Data.Add(new Model()
                    {
                        XValue = "Label" +i.ToString(),
                        YValue = rd.Next(0, 50)
                    });
                }
            }

            private ObservableCollection<Model> data;

            public ObservableCollection<Model> Data
            {
                get { return data; }
                set { data = value; }
            }

        }
       
    public class PieSeriesExt :PieSeries
    {
        public ObservableCollection<ChartSegment> ChartSegments
        {
            get
            {
                return Segments;
            }
        }
    }
    
}
