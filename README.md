# How-to-create-custom-legend-items-in-WPF-Chart

This article describes how to define the custom legend items with view model data. Please find the below KB for more details

[How to create custom LegendItems in WPF SfChart](https://www.syncfusion.com/kb/10675/?utm_medium=listing&utm_source=github-examples)

Custom legend items are achieved by adding required LegendItem count to ChartLegend’s Items property with Item, Series, IconHeight, IconWidth, IconVisibility, Interior and ItemTemplate as per the below code snippet and you can download the complete sample here.

 **Xaml**
```
<DataTemplate x:Key="itemTemplate"> 
        <StackPanel Margin="10,0,10,0"  Orientation="Horizontal"> 
                    <CheckBox Margin="2" Tag="{Binding}" 
                              IsChecked="True" 
                              Checked="CheckBox_Checked"   
                              Unchecked="CheckBox_Unchecked"/> 
 
                    <Rectangle  Width="{Binding IconWidth}"  
                                Height="{Binding IconHeight}"                                         
                                Fill="{Binding Interior}" Margin="2"/> 
 
                    <TextBlock HorizontalAlignment="Center" 
                               Margin="5,0,0,0" 
                               VerticalAlignment="Center" 
                               Text="{Binding Item.XValue}"> 
 
                    </TextBlock> 
          </StackPanel> 
</DataTemplate> 
```
**C#**
```
          ChartLegend legend = new ChartLegend(); 
 
            foreach (var series in chart.Series) 
            { 
                var data = series.ItemsSource as ObservableCollection<Model>; 
                for (int i = 0; i < data.Count; i++) 
                { 
                    legend.Items.Add(new LegendItem() 
                    { 
                        Item = data[i].XValue.ToString(), 
                        Series = series, 
                        IconHeight = 10, 
                        IconWidth = 10, 
                        IconVisibility = Visibility.Visible, 
                        Interior = series1.ColorModel.GetBrush(i),
                        CheckBoxVisibility = Visibility.Visible
                    }); 
                } 
            } 
 
            legend.ItemTemplate = grid.Resources["itemTemplate"] as DataTemplate; 
            chart.Legend = legend; 
```
 ```

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
```

KB article - [How-to-create-custom-legend-items-in-WPF-Chart](https://www.syncfusion.com/kb/10675/how-to-create-custom-legend-items-in-wpf-chart)


