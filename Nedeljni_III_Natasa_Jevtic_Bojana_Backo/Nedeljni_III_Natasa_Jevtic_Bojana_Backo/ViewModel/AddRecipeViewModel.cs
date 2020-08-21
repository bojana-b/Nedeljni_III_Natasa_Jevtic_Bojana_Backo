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
    class AddRecipeViewModel : ViewModelBase
    {
        AddRecipeView addRecipe;
        Recipes recipes;
        Users users = new Users();

        public AddRecipeViewModel(AddRecipeView addRecipeOpen, vwUser userLogged)
        {
            addRecipe = addRecipeOpen;
            user = userLogged;
            recipes = new Recipes();
            recipe = new vwRecipe();
            recipe.UserId = user.UserId;
            recipe.Author = user.NameAndSurname;            
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
        // AddIngredients Button
        private ICommand addIngredients;
        public ICommand AddIngredients
        {
            get
            {
                if (addIngredients == null)
                {
                    addIngredients = new RelayCommand(param => AddIngredientsExecute(), param => CanAddIngredientsExecute());
                }
                return addIngredients;
            }
        }
        private void AddIngredientsExecute()
        {
            if (String.IsNullOrEmpty(Recipe.RecipeName) || String.IsNullOrEmpty(Recipe.Type) || String.IsNullOrEmpty(Recipe.NumberOfPersons.ToString()) ||
                Recipe.NumberOfPersons == 0 || String.IsNullOrEmpty(Recipe.Description))
            {
                MessageBox.Show("Please fill all fields.", "Notification");
            }
            else
            {
                try
                {
                    recipes.CreateRecipe(Recipe, out int id);
                    Recipe.RecipeId = id;
                    AddIngredientsView addIngredients = new AddIngredientsView(Recipe);
                    addRecipe.Close();
                    addIngredients.ShowDialog();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }            
        }
        private bool CanAddIngredientsExecute()
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
                    if (Recipe.NameAndSurname == "Administrator")

                    {
                        AdminView adminView = new AdminView();
                        addRecipe.Close();
                        adminView.ShowDialog();

                    }
                    else
                    {
                        UserView userView = new UserView(users.FindUser(recipe.UserId));
                        addRecipe.Close();
                        userView.ShowDialog();
                    }                    

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
