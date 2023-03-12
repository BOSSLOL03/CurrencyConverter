using Windows.UI;
using Windows.UI.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// Документацию по шаблону элемента "Пустая страница" см. по адресу https://go.microsoft.com/fwlink/?LinkId=234238

namespace CurrencyConverter
{

    public sealed partial class ListPage : Page
    {
        private DataValue _dataValue;


        public ListPage()
        {
            this.InitializeComponent();


        }



        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter != null)
            {
                _dataValue = (DataValue)e.Parameter;
                
                foreach(Valut val in _dataValue.Data.valutsList)
                {
                    Button button = CreateButton(val.Name, val.CharCode);
                    parentPanel.Children.Add(button);
                }

            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;

            foreach (Valut val in _dataValue.Data.valutsList)
            {
                if (val.CharCode == button.Name)
                {
                    if(_dataValue.Position == "Left")
                    {
                        _dataValue.LeftValue = val.Value;
                        _dataValue.LeftNominal = val.Nominal;
                        _dataValue.LeftCharCode = val.CharCode;
                    }
                    if (_dataValue.Position == "Right")
                    {
                        _dataValue.RightValue = val.Value;
                        _dataValue.RightNominal = val.Nominal;
                        _dataValue.RightCharCode = val.CharCode;
                    }
                }
            }

            this.Frame.Navigate(typeof(MainPage), _dataValue);
        }

        public Button CreateButton(string Name, string CharName)
        {
            Button button = new Button();
            button.Name = CharName;
            button.Margin = new Thickness(2,2,2,2);
            button.BorderBrush = new SolidColorBrush(Color.FromArgb(255, 102, 103, 103));
            button.Background = new SolidColorBrush(Color.FromArgb(255, 114, 115, 115));
            button.Click += Button_Click;



            RelativePanel relativePanel = new RelativePanel
            {
                Width = 340,
                Height = 70,
            };
            TextBlock textBlock = new TextBlock()
            {
                Text = Name,
                Width = 190,
                FontSize = 15,
                TextWrapping = TextWrapping.Wrap,
                Margin = new Thickness(30,0,50,0),
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Center
            };
            relativePanel.Children.Add(textBlock);

            TextBlock textBlock1 = new TextBlock()
            {
                Text = CharName,
                FontSize = 20,
                FontWeight = FontWeights.Bold,
                TextWrapping = TextWrapping.Wrap,
                Margin = new Thickness(50, 0, 50, 0),
                HorizontalAlignment = HorizontalAlignment.Right,
                VerticalAlignment = VerticalAlignment.Center,
                
                
            };
            relativePanel.Children.Add(textBlock1);

            RelativePanel.SetAlignLeftWithPanel(textBlock, true);
            RelativePanel.SetAlignVerticalCenterWithPanel(textBlock, true);
            RelativePanel.SetAlignRightWithPanel(textBlock1, true);
            RelativePanel.SetAlignVerticalCenterWithPanel(textBlock1, true);

            button.Content = relativePanel;
            
            return button;
        }

    }
}
