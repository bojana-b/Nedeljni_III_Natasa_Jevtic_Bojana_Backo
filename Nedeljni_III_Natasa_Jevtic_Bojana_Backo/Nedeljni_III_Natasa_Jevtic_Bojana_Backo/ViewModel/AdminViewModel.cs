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
    class AdminViewModel : ViewModelBase
    {
        AdminView adminView;
        Users users;
        Recipes recipes;

        public AdminViewModel(AdminView adminViewOpen)
        {
            adminView = adminViewOpen;
            users = new Users();
            recipes = new Recipes();
            user = users.FindUser("Admin");
            recipeList = recipes.ViewAllRecipes();
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
        private List<vwRecipe> recipeList;
        public List<vwRecipe> RecipeList
        {
            get
            {
                return recipeList;
            }
            set
            {
                recipeList = value;
                OnPropertyChanged("RecipeList");
            }
        }
        #endregion

        #region Commands
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
                MessageBoxResult result = MessageBox.Show("Are you sure you want to log out?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    LoginScreen login = new LoginScreen();
                    adminView.Close();
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
        // AddNewRecipe Button
        private ICommand addNewRecipe;
        public ICommand AddNewRecipe
        {
            get
            {
                if (addNewRecipe == null)
                {
                    addNewRecipe = new RelayCommand(param => AddNewRecipeExecute(), param => CanAddNewRecipeExecute());
                }
                return addNewRecipe;
            }
        }
        private void AddNewRecipeExecute()
        {
            try
            {
                AddRecipeView addRecipe = new AddRecipeView(User);
                adminView.Close();
                addRecipe.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private bool CanAddNewRecipeExecute()
        {
            return true;
        }
        #endregion
    }
}
