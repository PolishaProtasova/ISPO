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

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для AddBooking.xaml
    /// </summary>
    public partial class AddBooking : Page
    {
        public AddBooking()
        {
            InitializeComponent();
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (HotelEntities db = new HotelEntities())
                {
                    Customers c = new Customers();
                    c.Age = Convert.ToInt32(ageTxt.Text);
                    c.PassportID = Convert.ToInt32(passporttxt.Text);
                    c.FirstName = nameTxt.Text;
                    c.LastName = lastnameTxt.Text;
                    c.Email = emailTxt.Text;
                    c.Phone = phoneTxt.Text;
                    db.Customers.Add(c);
                    db.SaveChanges();
                }
                MessageBox.Show("Добавлен новый клиент!");
                ageTxt.Clear();
                passporttxt.Clear();
                nameTxt.Clear();
                lastnameTxt.Clear();
                emailTxt.Clear();
                phoneTxt.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }

        }
    }
}
