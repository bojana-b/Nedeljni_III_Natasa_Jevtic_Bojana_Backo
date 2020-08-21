using Nedeljni_III_Natasa_Jevtic_Bojana_Backo.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nedeljni_III_Natasa_Jevtic_Bojana_Backo.ViewModel
{
    class SearchIngredientsViewModel : ViewModelBase
    {
        SearchIngredients searchIngredients;

        public SearchIngredientsViewModel(SearchIngredients searchIngredientsOpen, List<vwRecipe> list)
        {
            searchIngredients = searchIngredientsOpen;
            //recipeList = UserView.filteredList;
            RecipeList = list;
        }

        #region Properties
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
        #endregion
    }
}
