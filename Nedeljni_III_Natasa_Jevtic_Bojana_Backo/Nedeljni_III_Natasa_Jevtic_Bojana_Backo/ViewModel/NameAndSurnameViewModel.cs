using Nedeljni_III_Natasa_Jevtic_Bojana_Backo.Commands;
using Nedeljni_III_Natasa_Jevtic_Bojana_Backo.Model;
using Nedeljni_III_Natasa_Jevtic_Bojana_Backo.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Nedeljni_III_Natasa_Jevtic_Bojana_Backo.ViewModel
{
    class NameAndSurnameViewModel : ViewModelBase
    {
        NameAndSurnameView NameAndSurnameView;
        Users users;

        public NameAndSurnameViewModel(NameAndSurnameView NameAndSurnameViewOpen, string username, string password)
        {
            NameAndSurnameView = NameAndSurnameViewOpen;
            user = new vwUser();
            user.Username = username;
            user.Password = password;
            users = new Users();
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
        #endregion

        // Save Button
        #region Commans
        private ICommand save;
        public ICommand Save
        {
            get
            {
                if (save == null)
                {
                    save = new RelayCommand(param => SaveExecute(), param => CanSaveExecute());
                }
                return save;
            }
        }
        private void SaveExecute()
        {
            if (String.IsNullOrEmpty(User.NameAndSurname))
            {
                MessageBox.Show("Please fill a field.", "Notification");
            }
            else
            {
                try
                {
                    bool isCreated = users.AddUser(User);
                    if (isCreated)
                    {
                        MessageBox.Show("Account created!", "Notification");
                        AddRecipeView recipeView = new AddRecipeView(User);
                        NameAndSurnameView.Close();
                        recipeView.ShowDialog();                        
                    }
                    else
                    {
                        MessageBox.Show("Account cannot be created.", "Notification");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }
        private bool CanSaveExecute()
        {
            return true;
        }

        // Cancel Button
        private ICommand cancel;
        public ICommand Cancel
        {
            get
            {
                if (cancel == null)
                {
                    cancel = new RelayCommand(param => CancelExecute(), param => CanCancelExecute());
                }
                return cancel;
            }
        }
        private void CancelExecute()
        {
            try
            {
                MessageBoxResult result = MessageBox.Show("Are you sure you want to cancel?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    LoginScreen login = new LoginScreen();
                    NameAndSurnameView.Close();
                    login.Show();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }            
        }
        private bool CanCancelExecute()
        {
            return true;
        }
        #endregion
    }
}
