using Nedeljni_III_Natasa_Jevtic_Bojana_Backo.Commands;
using Nedeljni_III_Natasa_Jevtic_Bojana_Backo.Model;
using Nedeljni_III_Natasa_Jevtic_Bojana_Backo.View;
using System;
using System.Collections.Generic;
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
                EditRecipeView editRecipeView = new EditRecipeView(User,Recipe);
                adminView.Close();
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

        private ICommand delete;
        public ICommand Delete
        {
            get
            {
                if (delete == null)
                {
                    delete = new RelayCommand(param => DeleteExecute(), param => CanDeleteExecute());
                }
                return delete;
            }
        }
        public void DeleteExecute()
        {
            try
            {
                if (Recipe != null)
                {
                    MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this recipe?", "Confirmation", MessageBoxButton.YesNo);
                    if (result == MessageBoxResult.Yes)
                    {
                        bool isDeleted = recipes.DeleteRecipe(Recipe);

                        if (isDeleted == true)
                        {
                            MessageBox.Show("Recipe is deleted.", "Notification", MessageBoxButton.OK);
                            RecipeList = recipes.ViewAllRecipes();
                        }
                        else
                        {
                            MessageBox.Show("Recipe cannot be deleted.", "Notification", MessageBoxButton.OK);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        public bool CanDeleteExecute()
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
                SearchIngredients searchIngredients = new SearchIngredients();
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
        #endregion
    }
}
