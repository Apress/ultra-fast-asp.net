using System.Text;
using System.Windows;
using System.Windows.Browser;
using System.Windows.Controls;

namespace CountryList
{
    public partial class MainPage : UserControl
    {
        private string[] countries = { "AF", "Afghanistan", "AL", "Albania", "DZ", "Algeria", "US", "United States" };

        public MainPage()
        {
            HtmlPage.RegisterScriptableObject("Page", this);
            this.Loaded += new RoutedEventHandler(Page_Loaded);
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            HtmlElement div = HtmlPage.Document.GetElementById("putCountriesHere");
            if (div != null)
            {
                string selected = (string)div.GetProperty("innerHTML");
                StringBuilder sb = new StringBuilder();
                sb.Append("<select>");
                for (int i = 0; i < countries.Length; i += 2)
                {
                    sb.Append("<option value=\"");
                    sb.Append(countries[i]);
                    sb.Append("\"");
                    if (countries[i] == selected)
                        sb.Append(" selected");
                    sb.Append(">");
                    sb.Append(countries[i + 1]);
                    sb.Append("</option>");
                }
                sb.Append("</select>");
                div.SetProperty("innerHTML", sb.ToString());
            }
        }
        
        [ScriptableMember]
        public string BrowserType()
        {
            return HtmlPage.BrowserInformation.UserAgent;
        }
    }
}
