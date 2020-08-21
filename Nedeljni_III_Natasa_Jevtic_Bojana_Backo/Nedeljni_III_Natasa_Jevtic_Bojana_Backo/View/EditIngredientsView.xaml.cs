using Nedeljni_III_Natasa_Jevtic_Bojana_Backo.ViewModel;
using System.Windows;

namespace Nedeljni_III_Natasa_Jevtic_Bojana_Backo.View
{
    /// <summary>
    /// Interaction logic for EditIngredientsView.xaml
    /// </summary>
    public partial class EditIngredientsView : Window
    {
        public EditIngredientsView(vwRecipe recipeToEdit)
        {
            InitializeComponent();
            this.DataContext = new EditIngredientsViewModel(this, recipeToEdit);
        }
    }
}
