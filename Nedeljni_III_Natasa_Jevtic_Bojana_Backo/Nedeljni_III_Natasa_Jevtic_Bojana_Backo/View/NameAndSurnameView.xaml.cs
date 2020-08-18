using Nedeljni_III_Natasa_Jevtic_Bojana_Backo.ViewModel;
using System.Windows;

namespace Nedeljni_III_Natasa_Jevtic_Bojana_Backo.View
{
    /// <summary>
    /// Interaction logic for NameAndSurnameView.xaml
    /// </summary>
    public partial class NameAndSurnameView : Window
    {
        public NameAndSurnameView(string username, string password)
        {
            InitializeComponent();
            this.DataContext = new NameAndSurnameViewModel(this, username, password);
        }
    }
}
