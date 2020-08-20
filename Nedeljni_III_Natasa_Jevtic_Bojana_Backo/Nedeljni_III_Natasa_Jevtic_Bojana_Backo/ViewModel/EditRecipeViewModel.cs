using Nedeljni_III_Natasa_Jevtic_Bojana_Backo.Commands;
using Nedeljni_III_Natasa_Jevtic_Bojana_Backo.Model;
using Nedeljni_III_Natasa_Jevtic_Bojana_Backo.View;
using System;
using System.Windows;
using System.Windows.Input;

namespace Nedeljni_III_Natasa_Jevtic_Bojana_Backo.ViewModel
{
    class EditRecipeViewModel : ViewModelBase
    {
        EditRecipeView editRecipe;
        Recipes recipes = new Recipes();
        Users users = new Users();

        public string OldAuthor { get; set; }

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
       
        private ICommand editIngredients;
        public ICommand EditIngredients
        {
            get
            {
                if (editIngredients == null)
                {
                    editIngredients = new RelayCommand(param => EditIngredientsExecute(), param => CanEditIngredientsExecute());
                }
                return editIngredients;
            }
        }

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

        public EditRecipeViewModel(EditRecipeView editRecipe, vwUser userLogged, vwRecipe recipeToEdit)
        {
            this.editRecipe = editRecipe;
            User = userLogged;
            Recipe = recipeToEdit;
            OldAuthor = recipeToEdit.Author;
            
        }

        private void EditIngredientsExecute()
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
                    EditIngredientsView editIngredients = new EditIngredientsView(Recipe);                    
                    editIngredients.ShowDialog();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }
        private bool CanEditIngredientsExecute()
        {
            return true;
        }

        private void SaveExecute()
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
                    MessageBoxResult result = MessageBox.Show("Are you sure you want to save the recipe?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (result == MessageBoxResult.Yes)
                    {
                        //changing id and author of recipe is user is administrator
                        Recipe.Author = User.NameAndSurname;
                        Recipe.UserId = User.UserId;
                        Recipe.NameAndSurname = User.NameAndSurname;
                        bool isEdited = recipes.EditRecipe(Recipe);
                        if (isEdited == true)
                        {
                            string message = String.Format("Recipe {0} of user {1} is edited.", recipe.RecipeName, OldAuthor);
                            MessageBox.Show(message, "Notification", MessageBoxButton.OK);
                            if (Recipe.NameAndSurname == "Administrator")
                            {
                                AdminView adminView = new AdminView();
                                editRecipe.Close();
                                adminView.ShowDialog();

                            }
                            else
                            {

                                UserView userView = new UserView(users.FindUser(recipe.UserId));
                                editRecipe.Close();
                                userView.ShowDialog();
                            }
                        }
                        else
                        {
                            MessageBox.Show("Recipe cannot be edited.", "Notification", MessageBoxButton.OK);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        public bool CanSaveExecute()
        {
            return true;
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
                        editRecipe.Close();
                        adminView.ShowDialog();

                    }
                    else
                    {

                        UserView userView = new UserView(users.FindUser(recipe.UserId));
                        editRecipe.Close();
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
    }
}
