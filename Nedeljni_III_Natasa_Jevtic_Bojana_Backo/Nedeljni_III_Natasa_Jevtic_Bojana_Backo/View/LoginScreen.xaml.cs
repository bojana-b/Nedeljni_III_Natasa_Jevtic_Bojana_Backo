using Nedeljni_III_Natasa_Jevtic_Bojana_Backo.ViewModel;
using System.Windows;

namespace Nedeljni_III_Natasa_Jevtic_Bojana_Backo.View
{
    /// <summary>
    /// Interaction logic for LoginScreen.xaml
    /// </summary>
    public partial class LoginScreen : Window
    {
        public LoginScreen()
        {
            InitializeComponent();
            this.DataContext = new LoginScreenViewModel(this);
        }
    }
}
