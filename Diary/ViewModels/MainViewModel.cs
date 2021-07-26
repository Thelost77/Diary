using Diary.Commands;
using Diary.Models;
using Diary.Models.Domains;
using Diary.Models.Wrappers;
using Diary.Properties;
using Diary.Views;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace Diary.ViewModels
{
    class MainViewModel : ViewModelBase
    {
        private static string _connectionString = $"Server=({Settings.Default.ServerAddress})\\{Settings.Default.ServerName};Database={Settings.Default.NameOfDatabase}" + $";User Id={Settings.Default.Username};Password={Settings.Default.Password}";
        private Repository _repository = new Repository();
        private SqlHelper _helper = new SqlHelper();
        public MainViewModel()
        {
            RefreshStudentsCommand = new RelayCommand(RefreshStudents);
            AddStudentsCommand = new RelayCommand(AddEditStudents);
            EditStudentsCommand = new RelayCommand(AddEditStudents, CanEditDeleteStudent);
            DeleteStudentsCommand = new AsyncRelayCommand(DeleteStudents, CanEditDeleteStudent);
            ChangeSettingsCommand = new RelayCommand(EditSettings);

            CheckConnection();
            InitGroups();
            RefreshDiary();
        }

        public ICommand RefreshStudentsCommand { get; set; }
        public ICommand AddStudentsCommand { get; set; }
        public ICommand EditStudentsCommand { get; set; }
        public ICommand DeleteStudentsCommand { get; set; }
        public ICommand ChangeSettingsCommand { get; set; }


        private StudentWrapper _selectedStudent;

        public StudentWrapper SelectedStudent
        {
            get { return _selectedStudent; }
            set
            {
                _selectedStudent = value;
                OnPropertyChanged();
            }
        }
        private ObservableCollection<StudentWrapper> _students;

        public ObservableCollection<StudentWrapper> Students
        {
            get { return _students; }
            set
            {
                _students = value;
                OnPropertyChanged();
            }
        }

        private int _selectedGroupId;
        public int SelectedGroupId
        {
            get { return _selectedGroupId; }
            set
            {
                _selectedGroupId = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Group> _group;

        public ObservableCollection<Group> Groups
        {
            get { return _group; }
            set
            {
                _group = value;
                OnPropertyChanged();
            }
        }
        private void RefreshStudents(object obj)
        {
            RefreshDiary();
        }

        private void InitGroups()
        {
            var groups = _repository.GetGroups();
            groups.Insert(0, new Group { Id = 0, Name = "Wszystkie" });

            Groups = new ObservableCollection<Group>(groups);

            SelectedGroupId = 0;
        }
        private void AddEditStudents(object obj)
        {
            var addEditStudentWindow = new AddEditStudentView(obj as StudentWrapper);
            addEditStudentWindow.Closed += AddEditStudentsWindow_Closed;
            addEditStudentWindow.ShowDialog();
        }
        private void EditSettings(object obj)
        {
            var settingsChanger = new SettingsChangerView();
            settingsChanger.Closed += SettingsView_Closed;
            settingsChanger.ShowDialog();
        }

        private async Task DeleteStudents(object obj)
        {
            var metroWindow = Application.Current.MainWindow as MetroWindow;
            var dialog = await metroWindow.ShowMessageAsync("Usuwanie ucznia", $"Czy napewno chcesz usunąć ucznia {SelectedStudent.FirstName} {SelectedStudent.LastName}?", MessageDialogStyle.AffirmativeAndNegative);

            if (dialog != MessageDialogResult.Affirmative)
                return;
            _repository.DeleteStudent(_selectedStudent.Id);
            RefreshDiary();
        }
        private bool CanEditDeleteStudent(object obj)
        {
            return SelectedStudent != null; ;
        }

        private void RefreshDiary()
        {
            Students = new ObservableCollection<StudentWrapper>(_repository.GetStudents(_selectedGroupId));
        }


        private void CheckConnection()
        {
            if (!_helper.IsConnected(_connectionString))
            {
                var messageBox = new CustomMessageBox();
                messageBox.ShowDialog();
            }
        }

        //    var result = MessageBox.Show("Błąd łączenia z bazą danych, czy chcesz uruchomić ustawienia?", "Błąd ładowania bazy danych", MessageBoxButton.YesNo);
        //    if (result == MessageBoxResult.Yes)
        //    {
        //        var settingschanger = new SettingsChangerView();
        //        settingschanger.ShowDialog();
        //    }
        //    else
        //    {
        //        Application.Current.Shutdown();


        private void AddEditStudentsWindow_Closed(object sender, EventArgs e)
        {

        }
        private void SettingsView_Closed(object sender, EventArgs e)
        {

        }
    }
}
