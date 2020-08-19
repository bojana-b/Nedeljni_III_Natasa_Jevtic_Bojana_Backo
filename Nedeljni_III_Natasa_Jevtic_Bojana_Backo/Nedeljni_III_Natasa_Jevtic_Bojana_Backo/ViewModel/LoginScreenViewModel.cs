using Nedeljni_III_Natasa_Jevtic_Bojana_Backo.Commands;
using Nedeljni_III_Natasa_Jevtic_Bojana_Backo.HelperMethods;
using Nedeljni_III_Natasa_Jevtic_Bojana_Backo.Model;
using Nedeljni_III_Natasa_Jevtic_Bojana_Backo.Validations;
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
            user = new vwUser();
        }
        #region Properties
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
                    if (!users.IsUser("Admin"))
                    {
                        User.Username = UserName;
                        User.Password = password;
                        User.NameAndSurname = "Administrator";
                        users.AddUser(User);
                        AdminView admin = new AdminView();
                        loginScreen.Close();
                        admin.ShowDialog();
                    }
                    else
                    {
                        AdminView admin = new AdminView();
                        loginScreen.Close();
                        admin.ShowDialog();
                    }
                }
                else if (users.IsUser(UserName))
                {
                    User = users.FindUser(UserName);
                    if (SecurePasswordHasher.Verify(password, User.Password))
                    {
                        UserView userWindow = new UserView(User);
                        loginScreen.Close();
                        userWindow.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("Wrong password!");
                    }
                }
                else
                {
                    if (PasswordValidation.PasswordOk(password))
                    {
                        NameAndSurnameView nameAndSurnameView = new NameAndSurnameView(UserName, password);
                        loginScreen.Close();
                        nameAndSurnameView.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("Password must contain at least 5 characters!");
                    }
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
