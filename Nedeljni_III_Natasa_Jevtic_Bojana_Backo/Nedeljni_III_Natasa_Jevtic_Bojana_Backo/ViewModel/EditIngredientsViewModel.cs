using Nedeljni_III_Natasa_Jevtic_Bojana_Backo.Commands;
using Nedeljni_III_Natasa_Jevtic_Bojana_Backo.Model;
using Nedeljni_III_Natasa_Jevtic_Bojana_Backo.View;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace Nedeljni_III_Natasa_Jevtic_Bojana_Backo.ViewModel
{
    class EditIngredientsViewModel : ViewModelBase
    {
        EditIngredientsView editIngredients;
        Ingredients ingredients = new Ingredients();
        Recipes recipes = new Recipes();
        Users users = new Users();

        public EditIngredientsViewModel(EditIngredientsView editIngredientsOpen, vwRecipe recipeCreated)
        {
            editIngredients = editIngredientsOpen;
            Recipe = recipeCreated;
            Ingredient = new vwIngredient();
            ingredient.RecipeId = recipeCreated.RecipeId;
            IngredientList = ingredients.GetRecipeIngredients(Recipe);
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

        public List<vwIngredient> AddedIngredientList { get; set; } = new List<vwIngredient>();
        public List<vwIngredient> DeletedIngredientList { get; set; } = new List<vwIngredient>();

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

        private ICommand saveIngredients;

        public ICommand SaveIngredients
        {
            get
            {
                if (saveIngredients == null)
                {
                    saveIngredients = new RelayCommand(param => SaveIngredientsExecute(), param => CanSaveIngredientsExecute());
                }
                return saveIngredients;
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
                    //removing from collection of added ingredients
                    AddedIngredientList.Remove(Ingredient);
                    //added to collection of deleted ingredients
                    DeletedIngredientList.Add(Ingredient);
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
                    ingredients.AddIngredient(Ingredient, out int id);
                    Ingredient.IngredientId = id;
                    //add to collection of added ingredients
                    AddedIngredientList.Add(Ingredient);
                    //remove from collection of deleted ingredients
                    DeletedIngredientList.Remove(Ingredient);
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
        /// This method invokes method for closing window for editing ingredients.
        /// </summary>
        public void CancelRecipeExecute()
        {
            try
            {
                MessageBoxResult result = MessageBox.Show("Are you sure you want to cancel editing ingredients?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    if (AddedIngredientList != null || AddedIngredientList.Count > 0)
                    {
                        foreach (var ingredient in AddedIngredientList)
                        {
                            ingredients.DeleteIngredient(ingredient);
                        }
                    }
                    if (DeletedIngredientList != null || DeletedIngredientList.Count > 0)
                    {
                        foreach (var ingredient in DeletedIngredientList)
                        {
                            ingredients.AddIngredient(ingredient, out int id);
                            ingredient.IngredientId = id;
                        }
                    }
                    editIngredients.Close();
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

        public void SaveIngredientsExecute()
        {
            if (IngredientList == null || IngredientList.Count == 0)
            {
                MessageBox.Show("Please first add ingredients.", "Notification");
            }
            else
            {
                try
                {
                    MessageBoxResult result = MessageBox.Show("Are you sure you want to save the ingredients?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (result == MessageBoxResult.Yes)
                    {
                        foreach (var ingredient in IngredientList)
                        {
                            ingredients.EditIngredient(ingredient);
                        }
                        editIngredients.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        public bool CanSaveIngredientsExecute()
        {
            return true;
        }
    }
}