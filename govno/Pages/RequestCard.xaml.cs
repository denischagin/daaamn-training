using govno.Context;
using govno.Navigations;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
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
    /// Логика взаимодействия для RequestCard.xaml
    /// </summary>
    public partial class RequestCard : Page
    {
        Request request = new Request()
        {
            AdditionalDate = DateTime.Now
        };
        govnoEntities DB = DBConnection.DB;
        decimal? Id = null;
        public RequestCard(decimal Id)
        {
            this.Id = Id;
            InitializeComponent();
            Request findedRequest = DB.Request.FirstOrDefault(x => x.Id == Id);
            if (findedRequest == null) return;
            request = findedRequest;
        }
        public RequestCard()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            requestForm.DataContext = request;
            employeesListCB.ItemsSource = DB.Employeee.ToList();
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            Request createdRequest = null;
            if (Id == null)
            {
                createdRequest = DB.Request.Add(request);
            }

            try
            {
                DB.SaveChanges();

            }
            catch 
            {
                MessageBox.Show("Не все поля заполнены");
                if (createdRequest == null)
                {
                    DB.Request.Remove(request);
                }
                return;
            }

            Navigation.Navigate(this, new RequestsList());
        }
    }
}
