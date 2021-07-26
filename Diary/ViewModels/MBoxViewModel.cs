using Diary.Commands;
using Diary.Properties;
using Diary.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Diary.ViewModels
{
    public class MBoxViewModel : ViewModelBase
    {
        private string _message;
        private string _title;
        public MBoxViewModel()
        {
            Message = "Błąd ładowania bazy danych,\nczy chcesz przejść do ustawień?";
            Title = "Błąd ładowania bazy danych!";
            YesCommand = new RelayCommand(Yes);
            NoCommand = new RelayCommand(No);
        }                    
        public string Title
        {
            get { return _title; }
            set 
            { 
                _title = value;
                OnPropertyChanged();
            }
        }

        public string Message 
        {
            get
            {
                return _message;
            }
            set
            {
                _message = value;
                OnPropertyChanged();
            }
        }

        public ICommand YesCommand { get; set; }
        public ICommand NoCommand { get; set; }
        private void CloseWindow(Window window)
        {
            window.Close();

        }
        private void No(object obj)
        {
            Application.Current.Shutdown();
        }

        private void Yes(object obj)
        {
            var settingschanger = new SettingsChangerView();
            CloseWindow(obj as Window);
            settingschanger.ShowDialog();
        }        
    }
}
