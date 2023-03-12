using System;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// Документацию по шаблону элемента "Пустая страница" см. по адресу https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x419

namespace CurrencyConverter
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private Data _data;
        DataValue _dataValue;
        private float _leftValue = 0;
        private int _leftNomenal = 0;

        private float _rightValue = 0;
        private int _rightNomenal = 0;
        

        public MainPage()
        {
            this.InitializeComponent();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter != null)
            {
                
                
                if(e.Parameter.GetType().ToString() == "CurrencyConverter.Data")
                {
                    _data = (Data)e.Parameter;

                    _dataValue = new DataValue();

                    _dataValue.LeftValue = _data.valutsList[0].Value;
                    _dataValue.LeftNominal = _data.valutsList[0].Nominal;
                    _dataValue.LeftCharCode = _data.valutsList[0].CharCode;

                    _dataValue.RightValue = _data.valutsList[1].Value;
                    _dataValue.RightNominal = _data.valutsList[1].Nominal;
                    _dataValue.RightCharCode = _data.valutsList[1].CharCode;
                }
                if (e.Parameter.GetType().ToString() == "CurrencyConverter.DataValue")
                {
                    _dataValue = (DataValue)e.Parameter;
                    _data = _dataValue.Data;
                }

                SetDate(_data.Date.ToString());
                SetLeftValue(_dataValue.LeftValue, _dataValue.LeftNominal, _dataValue.LeftCharCode);
                SetRightValue(_dataValue.RightValue, _dataValue.RightNominal, _dataValue.RightCharCode);
            }
        }


        private void SetLeftValue(float value, int nomenal, string charCode)
        {
            _leftValue = value;
            _leftNomenal = nomenal;

            Left.Content = charCode;
        }
        private void SetRightValue(float value, int nomenal, string charCode)
        {
            _rightValue = value;
            _rightNomenal = nomenal;

            Right.Content = charCode;
        }

        private void SetDate(string date)
        { 
            string newDate = "";
            for (int i = 0; i <= 10 && i < date.Length; i++)
                newDate += date[i];

            this.date.Text = newDate;
        }

        private void LeftCalculate(float x)
        {
            float y = x * (_leftValue / _leftNomenal) * (_rightNomenal/ _rightValue);
            RightValue.Text = y.ToString();
        }
        private void RightCalculate(float y)
        {
            float x = y /((_leftValue / _leftNomenal) * (_rightNomenal / _rightValue));
            LeftValue.Text = x.ToString();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;

            _dataValue.Position = button.Name;
            _dataValue.Data = _data;

            this.Frame.Navigate(typeof(ListPage), _dataValue);
            
        }

        private void LeftValue_TextChanged(object sender, TextChangedEventArgs e)
        {
            LeftValue.BorderBrush = new SolidColorBrush(Color.FromArgb(255, 128, 128, 128));

            if (LeftValue.Text == "") return;
            if (!IsFloat(LeftValue.Text))
            {
                LeftValue.BorderBrush = new SolidColorBrush(Color.FromArgb(255, 255, 0, 0));
                return;
            }

            LeftCalculate(Convert.ToSingle(LeftValue.Text));

        }

        private void RightValue_TextChanged(object sender, TextChangedEventArgs e)
        {
            RightValue.BorderBrush = new SolidColorBrush(Color.FromArgb(255, 128, 128, 128));

            if (RightValue.Text == "") return;
            if (!IsFloat(RightValue.Text))
            {
                RightValue.BorderBrush = new SolidColorBrush(Color.FromArgb(255, 255, 0, 0));
                return;
            }

            RightCalculate(Convert.ToSingle(RightValue.Text));
        }

        private bool IsFloat(string str)
        {
            float n;
            return (float.TryParse(str, out n));
        }
    }
}
