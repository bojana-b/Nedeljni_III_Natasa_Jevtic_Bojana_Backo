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
    class AddIngredientsViewModel : ViewModelBase
    {
        AddIngredientsView addIngredients;
        Ingredients ingredients = new Ingredients();
        Recipes recipes = new Recipes();
        Users users = new Users();

        public AddIngredientsViewModel(AddIngredientsView addIngredientsOpen, vwRecipe recipeCreated)
        {
            addIngredients = addIngredientsOpen;
            recipe = recipeCreated;
            Ingredient = new vwIngredient();
            ingredient.RecipeId = recipeCreated.RecipeId;
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

        private vwIngredient ingredient;
        public vwIngredient Ingredient
        {
            get
            {
                return ingredient;
            }
            set
            {
                ingredient = value;
                OnPropertyChanged("Ingredient");
            }
        }

        private List<vwIngredient> ingredientList;
        public List<vwIngredient> IngredientList
        {
            get
            {
                return ingredientList;
            }
            set
            {
                ingredientList = value;
                OnPropertyChanged("IngredientList");
            }
        }

        private ICommand removeIngredient;

        public ICommand RemoveIngredient
        {
            get
            {
                if (removeIngredient == null)
                {
                    removeIngredient = new RelayCommand(param => RemoveIngredientExecute(), param => CanRemoveIngredientExecute());
                }
                return removeIngredient;
            }
        }

        private ICommand addIngredient;

        public ICommand AddIngredient
        {
            get
            {
                if (addIngredient == null)
                {
                    addIngredient = new RelayCommand(param => AddIngredientExecute(), param => CanAddIngredientExecute());
                }
                return addIngredient;
            }
        }

        private ICommand cancelRecipe;

        public ICommand CancelRecipe
        {
            get
            {
                if (cancelRecipe == null)
                {
                    cancelRecipe = new RelayCommand(param => CancelRecipeExecute(), param => CanCancelRecipeExecute());
                }
                return cancelRecipe;
            }
        }

        private ICommand saveRecipe;

        public ICommand SaveRecipe
        {
            get
            {
                if (saveRecipe == null)
                {
                    saveRecipe = new RelayCommand(param => SaveRecipeExecute(), param => CanSaveRecipeExecute());
                }
                return saveRecipe;
            }
        }

        public void RemoveIngredientExecute()
        {
            try
            {
                if (Ingredient != null)
                {
                    //invokes method to delete ingredient
                    ingredients.DeleteIngredient(Ingredient);
                    //invokes method to update list of ingredients
                    IngredientList = ingredients.GetRecipeIngredients(Recipe);
                    Ingredient = new vwIngredient();
                    Ingredient.RecipeId = Recipe.RecipeId;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public bool CanRemoveIngredientExecute()
        {
            return true;
        }
        /// <summary>
        /// This method invokes method for adding ingredient to recipe.
        /// </summary>
        public void AddIngredientExecute()
        {
            if (String.IsNullOrEmpty(Ingredient.IngredientName) || String.IsNullOrEmpty(Ingredient.Quantity.ToString()) || Ingredient.Quantity == 0)
            {
                MessageBox.Show("Please fill all fields.", "Notification");
            }
            else
            {
                try
                {
                    ingredients.AddIngredient(Ingredient);
                    //invokes method to update a list of ingredients
                    IngredientList = ingredients.GetRecipeIngredients(Recipe);
                    Ingredient = new vwIngredient();
                    Ingredient.RecipeId = Recipe.RecipeId;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }            
        }

        public bool CanAddIngredientExecute()
        {
            return true;
        }
        /// <summary>
        /// This method invokes method for deleting order.
        /// </summary>
        public void CancelRecipeExecute()
        {
            try
            {
                MessageBoxResult result = MessageBox.Show("Are you sure you want to cancel creating the recipe?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    bool isCanceled = recipes.DeleteRecipe(Recipe);
                    if (isCanceled == true)
                    {
                        MessageBox.Show("Creating recipe is canceled.", "Notification", MessageBoxButton.OK);
                        addIngredients.Close();
                    }
                    else
                    {
                        MessageBox.Show("Creating recipe cannot be canceled.", "Notification", MessageBoxButton.OK);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public bool CanCancelRecipeExecute()
        {
            return true;
        }

        public void SaveRecipeExecute()
        {
            if (IngredientList == null)
            {
                MessageBox.Show("Please first add ingredients.", "Notification");
            }
            else
            {
                try
                {
                    MessageBoxResult result = MessageBox.Show("Are you sure you want to save the recipe?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (result == MessageBoxResult.Yes)
                    {
                        bool isConfirmed = recipes.ConfirmRecipe(Recipe);
                        if (isConfirmed == true)
                        {
                            MessageBox.Show("Recipe is created.", "Notification", MessageBoxButton.OK);                            
                            if (Recipe.NameAndSurname == "Administrator")
                            {
                                AdminView adminView = new AdminView();
                                addIngredients.Close();
                                adminView.ShowDialog();
                                
                            }
                            else
                            {

                                UserView userView = new UserView(users.FindUser(recipe.UserId));
                                addIngredients.Close();
                                userView.ShowDialog();
                            }
                        }
                        else
                        {
                            MessageBox.Show("Recipe cannot be created.", "Notification", MessageBoxButton.OK);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }            
        }

        public bool CanSaveRecipeExecute()
        {
            return true;
        }
    }
}
