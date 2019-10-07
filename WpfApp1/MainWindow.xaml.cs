using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
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

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            GetSpendings();
        }

        public async void GetSpendings()
        {

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44342/api/");

            var result = (await client.GetAsync("wallet"));

            var json = await result.Content.ReadAsStringAsync();

            var data = JsonConvert.DeserializeObject<List<object>>(json);

            list_box_spendings.ItemsSource = data;
        }

        private async void AddSpending_Click(object sender, RoutedEventArgs e)
        {
            AddSpending addSpending = new AddSpending();

            if(addSpending.ShowDialog()==true)
            {
                var spending = addSpending.Spending;

                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri("https://localhost:44342/api/");

                var jsonSpending = JsonConvert.SerializeObject(spending);
                var content = new StringContent(jsonSpending,Encoding.UTF8,"application/json");

                var res = await client.PostAsync("wallet/addspending", content);

                MessageBox.Show($"Done with status: {res.StatusCode.ToString()}");
            }
        }
    }
}
