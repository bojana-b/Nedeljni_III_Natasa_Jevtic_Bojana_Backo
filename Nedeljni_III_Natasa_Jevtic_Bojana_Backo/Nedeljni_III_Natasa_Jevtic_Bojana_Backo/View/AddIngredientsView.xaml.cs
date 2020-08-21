using Nedeljni_III_Natasa_Jevtic_Bojana_Backo.ViewModel;
using System.Windows;

namespace Nedeljni_III_Natasa_Jevtic_Bojana_Backo.View
{
    /// <summary>
    /// Interaction logic for AddIngredientsView.xaml
    /// </summary>
    public partial class AddIngredientsView : Window
    {
        public AddIngredientsView(vwRecipe recipeCreate)
        {
            InitializeComponent();
            this.DataContext = new AddIngredientsViewModel(this, recipeCreate);
        }
    }
}
