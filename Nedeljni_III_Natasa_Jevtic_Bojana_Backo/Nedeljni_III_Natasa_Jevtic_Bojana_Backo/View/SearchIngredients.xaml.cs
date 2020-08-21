using Nedeljni_III_Natasa_Jevtic_Bojana_Backo.ViewModel;
using System.Windows;

namespace Nedeljni_III_Natasa_Jevtic_Bojana_Backo.View
{
    /// <summary>
    /// Interaction logic for SearchIngredients.xaml
    /// </summary>
    public partial class SearchIngredients : Window
    {
        public SearchIngredients()
        {
            InitializeComponent();
            this.DataContext = new SearchIngredientsViewModel(this);
        }
    }
}
