using govno.Context;
using govno.Navigations;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace govno.Pages
{
    /// <summary>
    /// Логика взаимодействия для RequestsList.xaml
    /// </summary>
    public partial class RequestsList : Page
    {
        public govnoEntities DB = DBConnection.DB;
        public RequestsList()
        {
            InitializeComponent();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            DB.SaveChanges();
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            Request request = requestsList.SelectedItem as Request;
            if (request == null)
            {
                MessageBox.Show("Чел ты не выбрал заявку");
                return;
            }
            Navigation.Navigate(this, new RequestCard(request.Id));
        }

        private void Create_Click(object sender, RoutedEventArgs e)
        {
            Navigation.Navigate(this, new RequestCard());
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            DB.Request.ToList();
            requestsList.ItemsSource = DB.Request.Local;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox searchBox = sender as TextBox;
            if (String.IsNullOrWhiteSpace(searchBox.Text))
            {
                requestsList.ItemsSource = DB.Request.ToList();
                return;
            }
            requestsList.ItemsSource = DB.Request.Where(r => r.ClientName.Contains(searchBox.Text)
                || r.ClientSurname.Contains(searchBox.Text)
                || r.ClientPatronymic.Contains(searchBox.Text)
                ).ToList();
        }
    }
}
