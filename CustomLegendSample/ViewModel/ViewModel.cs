namespace CustomLegendSample
{
    using System.Collections.ObjectModel;

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
                    XValue = "Label" + i.ToString(),
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
}
