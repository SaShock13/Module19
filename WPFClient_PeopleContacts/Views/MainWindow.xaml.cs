using GetDataLibrary;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WPFClient_PeopleContacts.Models;
using WPFClient_PeopleContacts.ViewModels;

namespace WPFClient_PeopleContacts
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        IGetData getDatas;
        //GetDataFromApi getData;
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
            //getData = new GetDataFromApi();
            //dgContacts.ItemsSource = getData.GetAll();
            //getDatas = new GetDataFromAPIControllers();
            //dgContacts.ItemsSource = getDatas.GetAll();

        }

        //private void Button_Click(object sender, RoutedEventArgs e)
        //{
        //    if (dgContacts.SelectedIndex!=-1)
        //    {
        //        int id = (dgContacts.SelectedItem as Person).Id;
        //        getDatas.DeleteById(id);
        //        UpdateDataGrid();
        //    }
        //}

        //private void Update_Click(object sender, RoutedEventArgs e)
        //{
        //    UpdateDataGrid();
        //}


        //private void UpdateDataGrid()
        //{
        //    dgContacts.ItemsSource = getDatas.GetAll();
        //}
    }
}