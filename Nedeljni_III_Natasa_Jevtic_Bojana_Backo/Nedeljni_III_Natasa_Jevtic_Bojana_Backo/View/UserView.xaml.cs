using Nedeljni_III_Natasa_Jevtic_Bojana_Backo.Model;
using Nedeljni_III_Natasa_Jevtic_Bojana_Backo.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Media;

namespace Nedeljni_III_Natasa_Jevtic_Bojana_Backo.View
{
    /// <summary>
    /// Interaction logic for UserView.xaml
    /// </summary>
    public partial class UserView : Window
    {
        private GridViewColumnHeader listViewSortCol = null;
        private SortAdorner listViewSortAdorner = null;
        public static List<vwRecipe> filteredList = new List<vwRecipe>();
        public List<vwRecipe> items;
        public CollectionView view;
        public string ingredient;
        public List<string> ingredients;
        public vwRecipe recipe;
        public List<vwRecipe> filteredRecipes;
        public UserView(vwUser userLogged)
        {
            InitializeComponent();
            this.DataContext = new UserViewModel(this, userLogged);

            ingredient = "";

            recipe = new vwRecipe();
            Recipes recipes = new Recipes();
            items = new List<vwRecipe>();
            items = recipes.ViewUserRecipes(userLogged);
            lvUsers.ItemsSource = items;
            view = (CollectionView)CollectionViewSource.GetDefaultView(lvUsers.ItemsSource);
            view.Filter = UserFilter;
            filteredList = new List<vwRecipe>();
            filteredRecipes = new List<vwRecipe>();
        }
        private bool UserFilter(object item)
        {
            if (SearchIngredients.ingredientsToSearch != null && String.IsNullOrEmpty(txtFilter.Text) && String.IsNullOrEmpty(txtFilter1.Text))
            {
                return recipeIngredients(item);
            }
            else if (!String.IsNullOrEmpty(txtFilter1.Text) && String.IsNullOrEmpty(txtFilter.Text) && SearchIngredients.ingredientsToSearch == null)
            {
                return ((item as vwRecipe).Type.IndexOf(txtFilter1.Text, StringComparison.OrdinalIgnoreCase) >= 0);
            }
            else if (String.IsNullOrEmpty(txtFilter.Text) || txtFilter.Text.Length < 3)
            {
                return true;
            }
            else if (!String.IsNullOrEmpty(txtFilter.Text) && txtFilter.Text.Length >= 3 && String.IsNullOrEmpty(txtFilter1.Text) && SearchIngredients.ingredientsToSearch == null)
            {
                return ((item as vwRecipe).RecipeName.IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0);
            }
            else if (!String.IsNullOrEmpty(txtFilter.Text) && txtFilter.Text.Length >= 3 && !String.IsNullOrEmpty(txtFilter1.Text) && SearchIngredients.ingredientsToSearch == null)
            {
                return ((item as vwRecipe).Type.IndexOf(txtFilter1.Text, StringComparison.OrdinalIgnoreCase) >= 0 && ((item as vwRecipe).RecipeName.IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0));
            }
            else if (!String.IsNullOrEmpty(txtFilter.Text) && txtFilter.Text.Length >= 3 && String.IsNullOrEmpty(txtFilter1.Text) && SearchIngredients.ingredientsToSearch != null)
            {
                return ((item as vwRecipe).RecipeName.IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0 && recipeIngredients(item));
            }
            else if ((String.IsNullOrEmpty(txtFilter.Text) || txtFilter.Text.Length < 3) && !String.IsNullOrEmpty(txtFilter1.Text) && SearchIngredients.ingredientsToSearch != null)
            {
                return (recipeIngredients(item) && ((item as vwRecipe).Type.IndexOf(txtFilter1.Text, StringComparison.OrdinalIgnoreCase) >= 0));
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

        private void txtFilter_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(lvUsers.ItemsSource).Refresh();
            filteredList = items.Where(i => view.Filter(i)).ToList();
        }

        private void searchButtonClick(object sender, RoutedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(lvUsers.ItemsSource).Refresh();
            filteredList = items.Where(i => view.Filter(i)).ToList();
        }

        private void lvUsersColumnHeader_Click(object sender, RoutedEventArgs e)
        {
            GridViewColumnHeader column = (sender as GridViewColumnHeader);
            string sortBy = column.Tag.ToString();
            if (listViewSortCol != null)
            {
                AdornerLayer.GetAdornerLayer(listViewSortCol).Remove(listViewSortAdorner);
                lvUsers.Items.SortDescriptions.Clear();
            }

            ListSortDirection newDir = ListSortDirection.Ascending;
            if (listViewSortCol == column && listViewSortAdorner.Direction == newDir)
                newDir = ListSortDirection.Descending;

            listViewSortCol = column;
            listViewSortAdorner = new SortAdorner(listViewSortCol, newDir);
            AdornerLayer.GetAdornerLayer(listViewSortCol).Add(listViewSortAdorner);
            lvUsers.Items.SortDescriptions.Add(new SortDescription(sortBy, newDir));
        }
    }
    public class SortAdorner : Adorner
    {
        private static Geometry ascGeometry =
            Geometry.Parse("M 0 4 L 3.5 0 L 7 4 Z");

        private static Geometry descGeometry =
            Geometry.Parse("M 0 0 L 3.5 4 L 7 0 Z");

        public ListSortDirection Direction { get; private set; }

        public SortAdorner(UIElement element, ListSortDirection dir)
            : base(element)
        {
            this.Direction = dir;
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);

            if (AdornedElement.RenderSize.Width < 20)
                return;

            TranslateTransform transform = new TranslateTransform
                (
                    AdornedElement.RenderSize.Width - 15,
                    (AdornedElement.RenderSize.Height - 5) / 2
                );
            drawingContext.PushTransform(transform);

            Geometry geometry = ascGeometry;
            if (this.Direction == ListSortDirection.Descending)
                geometry = descGeometry;
            drawingContext.DrawGeometry(Brushes.Black, null, geometry);

            drawingContext.Pop();
        }
    }
}
