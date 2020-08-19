using Nedeljni_III_Natasa_Jevtic_Bojana_Backo.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nedeljni_III_Natasa_Jevtic_Bojana_Backo.ViewModel
{
    class AddIngredientsViewModel : ViewModelBase
    {
        AddIngredientsView addIngredients;

        public AddIngredientsViewModel(AddIngredientsView addIngredientsOpen, vwRecipe recipeCreated)
        {
            addIngredients = addIngredientsOpen;
            recipe = recipeCreated;

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
    }
}
