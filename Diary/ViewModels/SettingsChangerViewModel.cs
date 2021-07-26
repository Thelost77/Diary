using Diary.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Diary.Views;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using Diary.Properties;

namespace Diary.ViewModels
{
    class SettingsChangerViewModel : ViewModelBase
    {
        private ConnectionHelper _connectionHelper;

        public ConnectionHelper ConnectionHelper
        {
            get { return _connectionHelper; }
            set
            {
                _connectionHelper = value;
                OnPropertyChanged();
            }
        }

        public SettingsChangerViewModel()
        {
            _connectionHelper = new ConnectionHelper();
            CloseCommand = new RelayCommand(Close);
            ConfirmCommand = new AsyncRelayCommand(Confirm);
            SetTextBoxes();
        }

        public ICommand CloseCommand { get; set; }
        public ICommand ConfirmCommand { get; set; }

        private void Close(object obj)
        {
            CloseWindow(obj as Window);
        }

        private void CloseWindow(Window window)
        {
            window.Close();
        }
        private async Task Confirm(object obj)
        {
            string connectionString = $"Server=({Settings.Default.ServerAddress})\\{Settings.Default.ServerName};Database={Settings.Default.NameOfDatabase}" + $";User Id={Settings.Default.Username};Password={Settings.Default.Password}";
            var metroWindow = obj as MetroWindow;
            var dialog = await metroWindow.ShowMessageAsync("Zmienianie ustawień", $"Czy napewno chcesz zapisać zmiany?", MessageDialogStyle.AffirmativeAndNegative);

            if (dialog != MessageDialogResult.Affirmative)
                return;

            ChangeProperties();
            RestartApp();


        }
        private void SetTextBoxes()
        {
            ConnectionHelper.NewNameOfDatabase = ConnectionHelper.NameOfDatabase;
            ConnectionHelper.NewPassword = ConnectionHelper.Password;
            ConnectionHelper.NewServerAddress = ConnectionHelper.ServerAddress;
            ConnectionHelper.NewServerName = ConnectionHelper.ServerName;
            ConnectionHelper.NewUsername = ConnectionHelper.Username;
        }

        private void ChangeProperties()
        {
            ConnectionHelper.NameOfDatabase = ConnectionHelper.NewNameOfDatabase;
            ConnectionHelper.Password = ConnectionHelper.NewPassword;
            ConnectionHelper.Username = ConnectionHelper.NewUsername;
            ConnectionHelper.ServerAddress = ConnectionHelper.NewServerAddress;
            ConnectionHelper.ServerName = ConnectionHelper.NewServerName;
            Settings.Default.Save();
        }

        private void RestartApp()
        {
            System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
            Application.Current.Shutdown();
        }
    }
}
