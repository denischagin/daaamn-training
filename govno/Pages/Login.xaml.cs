using govno.Context;
using govno.Navigations;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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

namespace govno.Pages
{
    /// <summary>
    /// Логика взаимодействия для Login.xaml
    /// </summary>
    public partial class Login : Page
    {
        private govnoEntities DB = DBConnection.DB;
        public Login()
        {
            InitializeComponent();
        }

        private void Registration_Click(object sender, RoutedEventArgs e)
        {
            Navigation.Navigate(this, new Registration());
        }

        async private void Login_Click(object sender, RoutedEventArgs _e)
        {
            string loginValue = login.Text;
            string passwordValue = password.Password;

            var request = await DB.Request.Include(r => r.Employeee.Role).ToListAsync();
            var employeee = await DB.Employeee.Include(e => e.Role).FirstOrDefaultAsync(e => e.Login == login.Text && e.Password == passwordValue);
            if (employeee == null)
            {
                MessageBox.Show($"Иди нахуй employeeeeeee {password.Password}");
                return;
            };
            if (employeee.Role.Name == "Admin")
            {
                MessageBox.Show("Красава админ");
                Navigation.Navigate(this, new RequestsList());
                return;
            }
            if (employeee.Role.Name == "Maker")
            {
                MessageBox.Show("Иди зарегайся, не позорься");
            }
            Navigation.Navigate(this, new Registration());
        }
    }
}
