using Nedeljni_III_Natasa_Jevtic_Bojana_Backo.Model;
using Nedeljni_III_Natasa_Jevtic_Bojana_Backo.ViewModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;

namespace Nedeljni_III_Natasa_Jevtic_Bojana_Backo.View
{
    /// <summary>
    /// Interaction logic for AdminView.xaml
    /// </summary>
    public partial class AdminView : Window
    {
        public string ingredient;
        public List<string> ingredients;
        public vwRecipe recipe;
        public List<vwRecipe> filteredRecipes;
        public static List<vwRecipe> filteredList;
        public List<vwRecipe> items;
        public CollectionView view;

        public AdminView()
        {            
            InitializeComponent();
            this.DataContext = new AdminViewModel(this);
            ingredient = "";

            recipe = new vwRecipe();
            filteredList = new List<vwRecipe>();
            SearchIngredients.ingredientsToSearch = new List<string>();
            items = new List<vwRecipe>();
            Recipes recipes = new Recipes();
            items = recipes.ViewAllRecipes();
            DataGridResults.ItemsSource = items;
            view = (CollectionView)CollectionViewSource.GetDefaultView(DataGridResults.ItemsSource);
            view.Filter = UserFilter;
        }

        protected void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(DataGridResults.ItemsSource).Refresh();
            filteredList = items.Where(i => view.Filter(i)).ToList();
        }

        private void searchButtonClick(object sender, RoutedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(DataGridResults.ItemsSource).Refresh();
            filteredList = items.Where(i => view.Filter(i)).ToList();
        }

        private bool UserFilter(object item)
        {
            if (SearchIngredients.ingredientsToSearch.Count != 0 && String.IsNullOrEmpty(txtFilter.Text) && String.IsNullOrEmpty(txtFilter1.Text))
            {
                return recipeIngredients(item);
            }
            else if (!String.IsNullOrEmpty(txtFilter1.Text) && String.IsNullOrEmpty(txtFilter.Text) && SearchIngredients.ingredientsToSearch.Count == 0)
            {
                return ((item as vwRecipe).Type.IndexOf(txtFilter1.Text, StringComparison.OrdinalIgnoreCase) >= 0);
            }

            else if (!String.IsNullOrEmpty(txtFilter.Text) && txtFilter.Text.Length >= 3 && String.IsNullOrEmpty(txtFilter1.Text) && SearchIngredients.ingredientsToSearch.Count == 0)
            {
                return ((item as vwRecipe).RecipeName.IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0);
            }
            else if (!String.IsNullOrEmpty(txtFilter.Text) && txtFilter.Text.Length >= 3 && !String.IsNullOrEmpty(txtFilter1.Text) && SearchIngredients.ingredientsToSearch.Count == 0)
            {
                return ((item as vwRecipe).Type.IndexOf(txtFilter1.Text, StringComparison.OrdinalIgnoreCase) >= 0 && ((item as vwRecipe).RecipeName.IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0));
            }
            else if (!String.IsNullOrEmpty(txtFilter.Text) && txtFilter.Text.Length >= 3 && String.IsNullOrEmpty(txtFilter1.Text) && SearchIngredients.ingredientsToSearch.Count != 0)
            {
                return ((item as vwRecipe).RecipeName.IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0 && recipeIngredients(item));
            }
            else if ((String.IsNullOrEmpty(txtFilter.Text) || txtFilter.Text.Length < 3) && !String.IsNullOrEmpty(txtFilter1.Text) && SearchIngredients.ingredientsToSearch.Count != 0)
            {
                return (recipeIngredients(item) && ((item as vwRecipe).Type.IndexOf(txtFilter1.Text, StringComparison.OrdinalIgnoreCase) >= 0));
            }
            else if (String.IsNullOrEmpty(txtFilter.Text) || txtFilter.Text.Length < 3)
            {
                return true;
            }
            else
            {
                return ((item as vwRecipe).RecipeName.IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0
                    && (item as vwRecipe).Type.IndexOf(txtFilter1.Text, StringComparison.OrdinalIgnoreCase) >= 0 &&
                    recipeIngredients(item));
            }
        }

        public bool recipeIngredients(object item)
        {
            recipe = item as vwRecipe;
            ingredients = new List<string>();
            try
            {
                using (RecipesDBEntities context = new RecipesDBEntities())
                {
                    for (int i = 0; i < SearchIngredients.ingredientsToSearch.Count; i++)
                    {
                        ingredient = SearchIngredients.ingredientsToSearch[i];
                        ingredients.Add(ingredient);
                    }
                    List<string> ingredientsInRecipe = context.vwIngredients.Where(x => x.RecipeId == recipe.RecipeId).Select(x => x.IngredientName).ToList();

                    if (ingredients.All((x => ingredientsInRecipe.Contains(x))))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
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
