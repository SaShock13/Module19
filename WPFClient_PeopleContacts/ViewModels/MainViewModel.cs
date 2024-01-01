using GetDataLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WPFClient_PeopleContacts.Views;

namespace WPFClient_PeopleContacts.ViewModels
{
    internal class MainViewModel:INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));

        }
        #region ПОЛЯ
        IGetData getDatas = new GetDataFromAPIControllers();
        private List<Person> persons;

        public List<Person> Persons
        {
            get { return persons; }
            set { persons = value; OnPropertyChanged(); }
        }
        private Person selectedPerson;

        public Person SelectedPerson
        {
            get { return selectedPerson; }
            set { selectedPerson = value; OnPropertyChanged("SelectedPerson"); }
        }

        private Person tempPerson;

        public Person TempPerson
        {
            get { return tempPerson ; }
            set { tempPerson = value; OnPropertyChanged("TempPerson"); }
        }
        #endregion

        #region КОМАНДЫ
        private AppCommand addContactCommand;
        public AppCommand AddContactCommand
        {
            get
            {
                return addContactCommand = new AppCommand(obj =>
                {
                    TempPerson = new Person()
                    {
                        LastName = "Иванов",
                        FirstName = "Иван",
                        SurName = "Иванович",
                        Description = "Человек обыкновенный",
                        PhoneNumber = "15-14-13",
                        PostalAddress = "Калуга"
                    };
                    AddContactWindow addContactWindow = new AddContactWindow();
                    addContactWindow.DataContext = this;
                    addContactWindow.Title = "Добавление контакта";
                    addContactWindow.ShowDialog();

                    if (addContactWindow.DialogResult==true)
                    {

                    getDatas.AddPerson(TempPerson);
                    UpdateList();
                    }
                   
                },
                    (obj) => true);


            }
        }

        private AppCommand deleteContactCommand;
        public AppCommand DeleteContactCommand
        {
            get
            {
                return deleteContactCommand = new AppCommand(obj =>
                {
                    int id = SelectedPerson.Id;
                    getDatas.DeleteById(id);
                    UpdateList();                
            
                },
                    (obj) => SelectedPerson!=null);
            }
        }

        private AppCommand changeContactCommand;
        public AppCommand ChangeContactCommand
        {
            get
            {
                return changeContactCommand = new AppCommand(obj =>
                {
                    AddContactWindow addContactWindow = new AddContactWindow();
                    addContactWindow.DataContext = this;
                    TempPerson = SelectedPerson;
                    addContactWindow.Title = "Изменение контакта";
                    
                    addContactWindow.ShowDialog();

                    if (addContactWindow.DialogResult == true)
                    {                       
                        getDatas.ChangePerson(TempPerson);
                        UpdateList();
                    }
                    TempPerson = new Person();

                },
                    (obj) => SelectedPerson != null);


            }
        }
        private AppCommand updateListCommand;
        public AppCommand UpdateListCommand
        {
            get
            {
                return updateListCommand = new AppCommand(obj =>
                {
                    UpdateList();
                },
                    (obj) => true);


            }
        }
        #endregion

        public MainViewModel()
        {
            
            UpdateList();
        }


        void UpdateList()
        {
            Persons = getDatas.GetAll().ToList();
        }
        


    }
}
