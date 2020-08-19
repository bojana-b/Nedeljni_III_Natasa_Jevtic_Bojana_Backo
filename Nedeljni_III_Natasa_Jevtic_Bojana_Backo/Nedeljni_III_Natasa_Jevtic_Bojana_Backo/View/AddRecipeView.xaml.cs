using Nedeljni_III_Natasa_Jevtic_Bojana_Backo.ViewModel;
using System.Windows;

namespace Nedeljni_III_Natasa_Jevtic_Bojana_Backo.View
{
    /// <summary>
    /// Interaction logic for AddRecipeView.xaml
    /// </summary>
    public partial class AddRecipeView : Window
    {
        public AddRecipeView(vwUser userLogged)
        {
            InitializeComponent();
            this.DataContext = new AddRecipeViewModel(this, userLogged);
        }
    }
}
