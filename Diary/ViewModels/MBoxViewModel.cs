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
    public class MBoxParams
    {
        public string Message { get; set; }
        public string Title { get; set; }
        public Action Accept { get; set; }
        public Action Rejection { get; set; }
    }

    public class MBoxViewModel : ViewModelBase
    {
        private string _message;
        private string _title;
        private readonly Action _accept;
        private readonly Action _rejection;

        public MBoxViewModel(MBoxParams @params)
        {
            Message = @params.Message;
            Title = @params.Title;
            _accept = @params.Accept;
            _rejection = @params.Rejection;


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
            _rejection.Invoke();
        }

        private void Yes(object obj)
        {
            
            CloseWindow(obj as Window);
            _accept.Invoke();
        }
        

    }
}
