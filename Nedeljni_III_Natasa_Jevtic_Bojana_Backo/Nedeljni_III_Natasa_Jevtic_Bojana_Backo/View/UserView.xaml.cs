using Nedeljni_III_Natasa_Jevtic_Bojana_Backo.ViewModel;
using System.Windows;

namespace Nedeljni_III_Natasa_Jevtic_Bojana_Backo.View
{
    /// <summary>
    /// Interaction logic for UserView.xaml
    /// </summary>
    public partial class UserView : Window
    {
        public UserView(vwUser userLogged)
        {
            InitializeComponent();
            this.DataContext = new UserViewModel(this, userLogged);
        }
    }
}
