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
    /// Логика взаимодействия для Registration.xaml
    /// </summary>
    public partial class Registration : Page
    {
        public govnoEntities DB = DBConnection.DB;
        public Registration()
        {
            InitializeComponent();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            Navigation.Navigate(this, new Login());
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Role role = DB.Role.FirstOrDefault(r => r.Name == "Maker");

            if (role == null) return;

            roles.ItemsSource = DB.Role.ToList();

            registrationForm.DataContext = new Employeee()
            {
                RoleId = role.Id
            };
        }

        private void Registration_Click(object sender, RoutedEventArgs e)
        {
            if (passwordBox.Password != repeatPasswordBox.Password)
            {
                MessageBox.Show("Пароли не равны");
                return;
            }

            Employeee newEmployee = registrationForm.DataContext as Employeee;

            newEmployee.Password = passwordBox.Password;

            DB.Employeee.Add(newEmployee);
            try
            {
                DB.SaveChanges();
            }
            catch (Exception ex)
            {
                DB.Employeee.Remove(newEmployee);
            }
        }
    }
}
