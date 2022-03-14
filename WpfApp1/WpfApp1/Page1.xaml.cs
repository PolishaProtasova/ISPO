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
    /// Логика взаимодействия для Page1.xaml
    /// </summary>
    public partial class Page1 : Page
    {
        public Page1()
        {
            InitializeComponent();
        }

        private void RoomBtn_Click(object sender, RoutedEventArgs e)
        {
            txt.Clear();
            using (HotelEntities db = new HotelEntities())
            {
                var rooms = db.Rooms;
                foreach (var r in rooms)
                {
                    txt.Text += string.Format("Номер {0}. Количество человек: {1} Количество комнат: {2} Цена: {3}\n", r.Id, r.NumberOfPersons, r.NumberOfRooms, r.Price);
                }
            }
        }

        private void ClientBtn_Click(object sender, RoutedEventArgs e)
        {
            txt.Clear();
            using (HotelEntities db = new HotelEntities())
            {
                var cust = db.Customers;
                foreach (var c in cust)
                {
                    txt.Text += string.Format("Фамилия: {0} Имя: {1} Возраст: {2} Email: {3} Телефон: {4}\n", c.LastName, c.FirstName, c.Age, c.Email, c.Phone);
                }
            }
        }

        private void BookingBtn_Click(object sender, RoutedEventArgs e)
        {
            txt.Clear();
            using (HotelEntities db = new HotelEntities())
            {
                var bookings = from Booking in db.Booking
                               join customer in db.Customers on Booking.CustomerId equals customer.Id
                               join room in db.Rooms
on Booking.RoomId equals room.Id
                               select new
                               {
                                   Name = customer.LastName,
                                   NumberOfPersons = room.NumberOfPersons,
                                   Price = room.Price,
                                   ArrivalDate = Booking.ArrivalDate,
                                   DepartureDate = Booking.DepartureDate
                               };
                foreach (var bk in bookings)
                {
                    txt.Text += string.Format("Фамилия: {0},  Количество человек: {1}\nЦена: {2}\n Дата приезда: {3},  Дата отъезда:{4} \n" +
                        "___________________________________________________________________________\n", bk.Name, bk.NumberOfPersons,
    bk.Price, bk.ArrivalDate, bk.DepartureDate);
                }
            }
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddBooking());
        }

        private void DelBtn_Click_1(object sender, RoutedEventArgs e)
        {
            using (HotelEntities db = new HotelEntities())
            {
                Customers c = db.Customers.Where(cust => cust.LastName == delTxt.Text).FirstOrDefault();
                if (c != null)
                {
                    db.Customers.Remove(c);
                    db.SaveChanges();
                    MessageBox.Show("Клиент удален!");
                }
                else
                {
                    MessageBox.Show("Клиент не найден!");

                }
            }
        }
    }
}
