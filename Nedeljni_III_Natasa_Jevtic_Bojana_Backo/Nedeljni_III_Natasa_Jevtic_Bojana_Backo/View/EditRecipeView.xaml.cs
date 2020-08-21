using Nedeljni_III_Natasa_Jevtic_Bojana_Backo.ViewModel;
using System.Windows;


namespace Nedeljni_III_Natasa_Jevtic_Bojana_Backo.View
{
    /// <summary>
    /// Interaction logic for EditRecipeView.xaml
    /// </summary>
    public partial class EditRecipeView : Window
    {
        public EditRecipeView(vwUser user, vwRecipe recipeToEdit)
        {
            InitializeComponent();
            this.DataContext = new EditRecipeViewModel(this, user, recipeToEdit);
        }
    }
}
