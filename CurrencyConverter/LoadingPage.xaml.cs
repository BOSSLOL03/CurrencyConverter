using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using Windows.UI.Xaml.Controls;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Windows.Storage;

// Документацию по шаблону элемента "Пустая страница" см. по адресу https://go.microsoft.com/fwlink/?LinkId=234238

namespace CurrencyConverter
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class LoadingPage : Page
    {

        private const string address = "https://www.cbr-xml-daily.ru/daily_json.js";
        public static string fileName = "kurs.js";


        public LoadingPage()
        {
            this.InitializeComponent();

            Task task = LoadingFileAsync();

        }

        private async Task LoadingFileAsync()
        {
            StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
            
            StorageFile file;
            try
            {
                WebClient webload = new WebClient();

                Stream stream = webload.OpenRead(address);
                StreamReader sr = new StreamReader(stream);
                string newLine;
                var listOfStrings = new List<string>();
                while ((newLine = sr.ReadLine()) != null)
                    listOfStrings.Add(newLine);


                

                file = await storageFolder.CreateFileAsync(fileName,
                   CreationCollisionOption.ReplaceExisting);

                await FileIO.AppendLinesAsync(file, listOfStrings);

                sr.Close();
                stream.Close();

                

            }
            catch
            {

                file = await storageFolder.CreateFileAsync(fileName,
                   CreationCollisionOption.OpenIfExists);
            }

            Info.Text = "Загрузка завершена";

            string text = await FileIO.ReadTextAsync(file);
            var json = JsonConvert.DeserializeObject<Rootobject>(text);

            Data data = new Data(json.Date);

            
            data.valutsList.Add(new Valut());
            data.valutsList.Add(new Valut(json.Valute.USD.CharCode, json.Valute.USD.Nominal, json.Valute.USD.Name, json.Valute.USD.Value));
            data.valutsList.Add(new Valut(json.Valute.EUR.CharCode, json.Valute.EUR.Nominal, json.Valute.EUR.Name, json.Valute.EUR.Value));
            data.valutsList.Add(new Valut(json.Valute.JPY.CharCode, json.Valute.JPY.Nominal, json.Valute.JPY.Name, json.Valute.JPY.Value));
            data.valutsList.Add(new Valut(json.Valute.KRW.CharCode, json.Valute.KRW.Nominal, json.Valute.KRW.Name, json.Valute.KRW.Value));
            data.valutsList.Add(new Valut(json.Valute.AUD.CharCode, json.Valute.AUD.Nominal, json.Valute.AUD.Name, json.Valute.AUD.Value));
            data.valutsList.Add(new Valut(json.Valute.AZN.CharCode, json.Valute.AZN.Nominal, json.Valute.AZN.Name, json.Valute.AZN.Value));
            data.valutsList.Add(new Valut(json.Valute.GBP.CharCode, json.Valute.GBP.Nominal, json.Valute.GBP.Name, json.Valute.GBP.Value));
            data.valutsList.Add(new Valut(json.Valute.AMD.CharCode, json.Valute.AMD.Nominal, json.Valute.AMD.Name, json.Valute.AMD.Value));
            data.valutsList.Add(new Valut(json.Valute.BYN.CharCode, json.Valute.BYN.Nominal, json.Valute.BYN.Name, json.Valute.BYN.Value));
            data.valutsList.Add(new Valut(json.Valute.BGN.CharCode, json.Valute.BGN.Nominal, json.Valute.BGN.Name, json.Valute.BGN.Value));
            data.valutsList.Add(new Valut(json.Valute.BRL.CharCode, json.Valute.BRL.Nominal, json.Valute.BRL.Name, json.Valute.BRL.Value));
            data.valutsList.Add(new Valut(json.Valute.HUF.CharCode, json.Valute.HUF.Nominal, json.Valute.HUF.Name, json.Valute.HUF.Value));
            data.valutsList.Add(new Valut(json.Valute.HKD.CharCode, json.Valute.HKD.Nominal, json.Valute.HKD.Name, json.Valute.HKD.Value));
            data.valutsList.Add(new Valut(json.Valute.DKK.CharCode, json.Valute.DKK.Nominal, json.Valute.DKK.Name, json.Valute.DKK.Value));
            data.valutsList.Add(new Valut(json.Valute.INR.CharCode, json.Valute.INR.Nominal, json.Valute.INR.Name, json.Valute.INR.Value));
            data.valutsList.Add(new Valut(json.Valute.KZT.CharCode, json.Valute.KZT.Nominal, json.Valute.KZT.Name, json.Valute.KZT.Value));
            data.valutsList.Add(new Valut(json.Valute.CAD.CharCode, json.Valute.CAD.Nominal, json.Valute.CAD.Name, json.Valute.CAD.Value));
            data.valutsList.Add(new Valut(json.Valute.KGS.CharCode, json.Valute.KGS.Nominal, json.Valute.KGS.Name, json.Valute.KGS.Value));
            data.valutsList.Add(new Valut(json.Valute.CNY.CharCode, json.Valute.CNY.Nominal, json.Valute.CNY.Name, json.Valute.CNY.Value));
            data.valutsList.Add(new Valut(json.Valute.MDL.CharCode, json.Valute.MDL.Nominal, json.Valute.MDL.Name, json.Valute.MDL.Value));
            data.valutsList.Add(new Valut(json.Valute.NOK.CharCode, json.Valute.NOK.Nominal, json.Valute.NOK.Name, json.Valute.NOK.Value));
            data.valutsList.Add(new Valut(json.Valute.PLN.CharCode, json.Valute.PLN.Nominal, json.Valute.PLN.Name, json.Valute.PLN.Value));
            data.valutsList.Add(new Valut(json.Valute.RON.CharCode, json.Valute.RON.Nominal, json.Valute.RON.Name, json.Valute.RON.Value));
            data.valutsList.Add(new Valut(json.Valute.XDR.CharCode, json.Valute.XDR.Nominal, json.Valute.XDR.Name, json.Valute.XDR.Value));
            data.valutsList.Add(new Valut(json.Valute.SGD.CharCode, json.Valute.SGD.Nominal, json.Valute.SGD.Name, json.Valute.SGD.Value));
            data.valutsList.Add(new Valut(json.Valute.TJS.CharCode, json.Valute.TJS.Nominal, json.Valute.TJS.Name, json.Valute.TJS.Value));
            data.valutsList.Add(new Valut(json.Valute.TRY.CharCode, json.Valute.TRY.Nominal, json.Valute.TRY.Name, json.Valute.TRY.Value));
            data.valutsList.Add(new Valut(json.Valute.TMT.CharCode, json.Valute.TMT.Nominal, json.Valute.TMT.Name, json.Valute.TMT.Value));
            data.valutsList.Add(new Valut(json.Valute.UZS.CharCode, json.Valute.UZS.Nominal, json.Valute.UZS.Name, json.Valute.UZS.Value));
            data.valutsList.Add(new Valut(json.Valute.UAH.CharCode, json.Valute.UAH.Nominal, json.Valute.UAH.Name, json.Valute.UAH.Value));
            data.valutsList.Add(new Valut(json.Valute.CZK.CharCode, json.Valute.CZK.Nominal, json.Valute.CZK.Name, json.Valute.CZK.Value));
            data.valutsList.Add(new Valut(json.Valute.SEK.CharCode, json.Valute.SEK.Nominal, json.Valute.SEK.Name, json.Valute.SEK.Value));
            data.valutsList.Add(new Valut(json.Valute.CHF.CharCode, json.Valute.CHF.Nominal, json.Valute.CHF.Name, json.Valute.CHF.Value));
            data.valutsList.Add(new Valut(json.Valute.ZAR.CharCode, json.Valute.ZAR.Nominal, json.Valute.ZAR.Name, json.Valute.ZAR.Value));


            this.Frame.Navigate(typeof(MainPage), data);
        }
    }
}
