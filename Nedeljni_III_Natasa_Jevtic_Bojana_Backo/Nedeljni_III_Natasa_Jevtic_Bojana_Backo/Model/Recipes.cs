using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Nedeljni_III_Natasa_Jevtic_Bojana_Backo.Model
{
    class Recipes
    {
        /// <summary>
        /// This method adds new recipe to DbSet and then saves changes to database.
        /// </summary>
        /// <param name="recipeToAdd">Recipe to be added.</param>  
        /// <returns>True if created, false if not.</returns>
        public bool CreateRecipe(vwRecipe recipeToAdd, out int recipeId)
        {
            try
            {
                using (RecipesDBEntities context = new RecipesDBEntities())
                {
                    tblRecipe recipe = new tblRecipe
                    {
                        Author = recipeToAdd.Author,
                        DateOfCreation = DateTime.Now,
                        Description = recipeToAdd.Description,
                        NumberOfPersons = recipeToAdd.NumberOfPersons,
                        RecipeName = recipeToAdd.RecipeName,
                        Type = recipeToAdd.Type,
                        UserId = recipeToAdd.UserId
                    };
                    context.tblRecipes.Add(recipe);                    
                    context.SaveChanges();
                    recipeId = recipe.RecipeId;
                    return true;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
                recipeId = 0;
                return false;
            }
        }
        /// <summary>
        /// This method finds recipes of forwarder user.
        /// </summary>
        /// <param name="user">User.</param>
        /// <returns>List of recipes.</returns>
        public List<vwRecipe> ViewUserRecipes(vwUser user)
        {
            try
            {
                using (RecipesDBEntities context = new RecipesDBEntities())
                {
                    return context.vwRecipes.Where(x => x.UserId == user.UserId).ToList();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }
        /// <summary>
        /// This method creates a list of all recipes.
        /// </summary>
        /// <returns>List of all recipes.</returns>
        public List<vwRecipe> ViewAllRecipes()
        {
            try
            {
                using (RecipesDBEntities context = new RecipesDBEntities())
                {
                    return context.vwRecipes.ToList();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }
        /// <summary>
        /// This method edits data of recipe and save changes to database..
        /// </summary>
        /// <param name="recipeToEdit">Recipe to be edited.</param>
        /// <returns>True if edited, false if not.</returns>
        public bool EditRecipe(vwRecipe recipeToEdit)
        {
            try
            {
                using (RecipesDBEntities context = new RecipesDBEntities())
                {
                    tblRecipe recipe = context.tblRecipes.Where(x => x.RecipeId == recipeToEdit.RecipeId).FirstOrDefault();
                    recipe.RecipeName = recipeToEdit.RecipeName;
                    recipe.Author = recipeToEdit.Author;
                    recipe.DateOfCreation = DateTime.Now;
                    recipe.Description = recipeToEdit.Description;
                    recipe.NumberOfPersons = recipeToEdit.NumberOfPersons;
                    recipe.Type = recipeToEdit.Type;
                    recipe.UserId = recipeToEdit.UserId;
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
        /// This method deletes recipe and every ingredient of that recipe from database.
        /// </summary>
        /// <param name="recipeToDelete">Recipe to be deleted.</param>
        /// <returns>True if deleted, false if not.</returns>
        public bool DeleteRecipe(vwRecipe recipeToDelete)
        {
            try
            {
                using (RecipesDBEntities context = new RecipesDBEntities())
                {
                    //finding recipe with forwarded id
                    tblRecipe recipe = context.tblRecipes.Where(x => x.RecipeId == recipeToDelete.RecipeId).FirstOrDefault();
                    //finding ingredients with forwarded id
                    List<tblIngredient> ingredients = context.tblIngredients.Where(x => x.RecipeId == recipeToDelete.RecipeId).ToList();
                    //removing ingredients
                    foreach (var item in ingredients)
                    {
                        context.tblIngredients.Remove(item);
                        context.SaveChanges();
                    }
                    context.tblRecipes.Remove(recipe);
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
        ///  This method changes time of recept creation and saves changes to database.
        /// </summary>
        /// <param name="recipe">Recipe.</param>
        /// <returns>True if recipe is edited, false if not.</returns>
        public bool ConfirmRecipe(vwRecipe recipeToConfirm)
        {
            try
            {
                using (RecipesDBEntities context = new RecipesDBEntities())
                {
                    tblRecipe recipe = context.tblRecipes.Where(x => x.RecipeId == recipeToConfirm.RecipeId).FirstOrDefault();                    
                    recipe.DateOfCreation = DateTime.Now;                    
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
