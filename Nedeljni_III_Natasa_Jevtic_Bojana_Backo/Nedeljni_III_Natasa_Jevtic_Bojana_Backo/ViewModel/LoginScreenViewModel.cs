using Nedeljni_III_Natasa_Jevtic_Bojana_Backo.Commands;
using Nedeljni_III_Natasa_Jevtic_Bojana_Backo.HelperMethods;
using Nedeljni_III_Natasa_Jevtic_Bojana_Backo.Model;
using Nedeljni_III_Natasa_Jevtic_Bojana_Backo.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Nedeljni_III_Natasa_Jevtic_Bojana_Backo.ViewModel
{
    class LoginScreenViewModel : ViewModelBase
    {
        LoginScreen loginScreen;
        Users users;

        public LoginScreenViewModel(LoginScreen loginScreenOpen)
        {
            loginScreen = loginScreenOpen;
            users = new Users();
        }
        #region Properties
        // 
        private vwUser user;
        public vwUser User
        {
            get
            {
                return user;
            }
            set
            {
                user = value;
                OnPropertyChanged("User");
            }
        }

        private string userName;
        public string UserName
        {

            get
            {
                return userName;
            }
            set
            {
                userName = value;
                OnPropertyChanged("UserName");
            }
        }
        #endregion
        #region Commands
        private ICommand submit;
        public ICommand Submit
        {
            get
            {
                if (submit == null)
                {
                    submit = new RelayCommand(SubmitCommandExecute, CanSubmitCommandExecute);
                }
                return submit;
            }
        }

        private void SubmitCommandExecute(object obj)
        {
            try
            {
                string password = (obj as PasswordBox).Password;
                if (UserName.Equals("Admin") && password.Equals("Admin123"))
                {
                    MainWindow master = new MainWindow();
                    loginScreen.Close();
                    master.ShowDialog();
                }
                else if (users.IsUser(UserName))
                {
                    User = users.FindUser(UserName);
                    if (SecurePasswordHasher.Verify(password, User.Password))
                    {
                        MainWindow managerWindow = new MainWindow();
                        loginScreen.Close();
                        managerWindow.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("Wrong password!");
                    }
                }
                else
                {
                    NameAndSurnameView nameAndSurnameView = new NameAndSurnameView(UserName, password);
                    loginScreen.Close();
                    nameAndSurnameView.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private bool CanSubmitCommandExecute(object obj)
        {
            string password = (obj as PasswordBox).Password;
            if (!String.IsNullOrEmpty(UserName) && !String.IsNullOrEmpty(password))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion 
    }
}
