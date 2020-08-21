using Nedeljni_III_Natasa_Jevtic_Bojana_Backo.Commands;
using Nedeljni_III_Natasa_Jevtic_Bojana_Backo.Model;
using Nedeljni_III_Natasa_Jevtic_Bojana_Backo.View;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace Nedeljni_III_Natasa_Jevtic_Bojana_Backo.ViewModel
{
    class UserViewModel : ViewModelBase
    {
        UserView userView;
        Users users;
        Recipes recipes;

        public UserViewModel(UserView userViewOpen, vwUser userLogged)
        {
            userView = userViewOpen;
            users = new Users();
            recipes = new Recipes();
            user = userLogged;
            recipeList = UserView.filteredList;
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
                OnPropertyChanged("filteredList");
            }
        }

        private vwRecipe recipe;
        public vwRecipe Recipe
        {
            get
            {
                return recipe;
            }
            set
            {
                recipe = value;
                OnPropertyChanged("Recipe");
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
                    userView.Close();
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
                userView.Close();
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
        // SearchByIngedients Button
        private ICommand searchByIngedients;
        public ICommand SearchByIngedients
        {
            get
            {
                if (searchByIngedients == null)
                {
                    searchByIngedients = new RelayCommand(param => SearchByIngedientsExecute(), param => CanSearchByIngedientsExecute());
                }
                return searchByIngedients;
            }
        }
        private void SearchByIngedientsExecute()
        {
            try
            {
                SearchIngredients searchIngredients = new SearchIngredients(UserView.filteredList);
                userView.Close();
                searchIngredients.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        
        private bool CanSearchByIngedientsExecute()
        {
            return true;
            }

        private ICommand edit;
        public ICommand Edit
        {
            get
            {
                if (edit == null)
                {
                    edit = new RelayCommand(param => EditExecute(), param => CanEditExecute());
                }
                return edit;
            }
        }

        private void EditExecute()
        {
            try
            {
                EditRecipeView editRecipeView = new EditRecipeView(User, Recipe);
                userView.Close();
                editRecipeView.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private bool CanEditExecute()
        {
            if (Recipe != null)
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
