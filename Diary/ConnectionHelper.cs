using Diary.Properties;

namespace Diary
{
    public class ConnectionHelper
    {
        public string NewServerAddress { get; set; }
        public string NewServerName { get; set; }
        public string NewNameOfDatabase { get; set; }
        public string NewUsername { get; set; }
        public string NewPassword { get; set; }
        public string ServerAddress
        {
            get
            {
                return Settings.Default.ServerAddress;
            }                    
            set
            {
                Settings.Default.ServerAddress = value;
            }
        }
        public string ServerName
        {
            get
            {
                return Settings.Default.ServerName;
            }

            set
            {
                Settings.Default.ServerName = value;
            }
        }        
        public string NameOfDatabase
        {
            get
            {
                return Settings.Default.NameOfDatabase;
            }
            set
            {
                Settings.Default.NameOfDatabase = value;
            }
        }
        public string Username
        {
            get
            {
                return Settings.Default.Username;
            }

            set
            {
                Settings.Default.Username = value;
            }
        }
        public string Password
        {
            get
            {
                return Settings.Default.Password;
            }

            set
            {
                Settings.Default.Password = value;
            }
        }
    }
}
