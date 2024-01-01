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

namespace WPFClient_PeopleContacts.Views
{
    /// <summary>
    /// Логика взаимодействия для AddContactWindow.xaml
    /// </summary>
    public partial class AddContactWindow : Window
    {
        public AddContactWindow()
        {
            InitializeComponent();
            
        }

        private void btnOk(object sender, RoutedEventArgs e)
        {
            if (tbLastName.Text.Length>3&tbFirstName.Text.Length > 2 & tbPhoneNumber.Text.Length >= 6)
            {

                DialogResult = true;
            }
           
        }
    }
}
