using Nedeljni_III_Natasa_Jevtic_Bojana_Backo.ViewModel;
using System.Collections.Generic;
using System.Windows;

namespace Nedeljni_III_Natasa_Jevtic_Bojana_Backo.View
{
    /// <summary>
    /// Interaction logic for SearchIngredients.xaml
    /// </summary>
    public partial class SearchIngredients : Window
    {
        public static List<string> ingredientsToSearch;
        public SearchIngredients()
        {
            InitializeComponent();
            ingredientsToSearch = new List<string>();
            this.DataContext = new SearchIngredientsViewModel(this);            
        }        
    }
}
