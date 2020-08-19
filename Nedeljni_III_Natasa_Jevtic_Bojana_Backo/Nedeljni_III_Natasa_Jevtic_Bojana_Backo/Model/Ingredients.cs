using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Nedeljni_III_Natasa_Jevtic_Bojana_Backo.Model
{
    class Ingredients
    {
        /// <summary>
        /// This method add ingredient of recipe.
        /// </summary>
        /// <param name="ingredient">Ingredient to be added.</param>  
        /// <returns>True if created, false if not.</returns>
        public bool AddIngredient(vwIngredient ingredientToAdd)
        {
            try
            {
                using (RecipesDBEntities context = new RecipesDBEntities())
                {
                    tblIngredient ingredient = new tblIngredient
                    {
                        IngredientName = ingredientToAdd.IngredientName,
                        Quantity = ingredientToAdd.Quantity,
                        RecipeId = ingredientToAdd.RecipeId
                    };
                    context.tblIngredients.Add(ingredient);
                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
                return false;
            }
        }
        /// <summary>
        /// This method creates a list of ingredients of recipes.
        /// </summary>
        /// <param name="recipe">Recipe.</param>
        /// <returns>List of ingredients.</returns>
        public List<vwIngredient> GetRecipeIngredients(vwRecipe recipe)
        {
            try
            {
                using (RecipesDBEntities context = new RecipesDBEntities())
                {
                    return context.vwIngredients.Where(x => x.RecipeId == recipe.RecipeId).ToList();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }
        /// <summary>
        /// This method deletes ingredient.
        /// </summary>
        /// <param name="ingredient">Ingredient to be deleted.</param>
        public bool DeleteIngredient(vwIngredient ingredient)
        {
            try
            {
                using (RecipesDBEntities context = new RecipesDBEntities())
                {
                    tblIngredient ingredientToDelete = context.tblIngredients.Where(x => x.IngredientId == ingredient.IngredientId).FirstOrDefault();
                    context.tblIngredients.Remove(ingredientToDelete);
                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
                return false;
            }
        }
        /// <summary>
        /// This method edits data of ingredient.
        /// </summary>
        /// <param name="ingredientToEdit">Ingredient to be edited.</param>
        /// <returns>True if edited, false if not.</returns>
        public bool EditIngredient(vwIngredient ingredientToEdit)
        {
            try
            {
                using (RecipesDBEntities context = new RecipesDBEntities())
                {
                    tblIngredient ingredient = context.tblIngredients.Where(x => x.IngredientId == ingredientToEdit.IngredientId).FirstOrDefault();
                    ingredient.IngredientName = ingredientToEdit.IngredientName;
                    ingredient.Quantity = ingredientToEdit.Quantity;
                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
                return false;
            }
        }
    }
}
