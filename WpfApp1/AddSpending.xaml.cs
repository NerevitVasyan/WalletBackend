using DTOLayer;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для AddSpending.xaml
    /// </summary>
    public partial class AddSpending : Window
    {
        public SpendingDto Spending { get; set; }
        public AddSpending()
        {
            InitializeComponent();
            Spending = new SpendingDto();
            DataContext = Spending;
        }

        private void AddSpending_Click(object sender, RoutedEventArgs e)
        {
            var tags = tags_textbox.Text.Split(" ,".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            Spending.Tags = tags.ToList();

            DialogResult = true;
            Close();
        }
    }
}
