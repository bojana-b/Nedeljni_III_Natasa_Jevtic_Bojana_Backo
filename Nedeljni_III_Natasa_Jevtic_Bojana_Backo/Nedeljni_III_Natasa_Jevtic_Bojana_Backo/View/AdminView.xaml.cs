using Nedeljni_III_Natasa_Jevtic_Bojana_Backo.ViewModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;

namespace Nedeljni_III_Natasa_Jevtic_Bojana_Backo.View
{
    /// <summary>
    /// Interaction logic for AdminView.xaml
    /// </summary>
    public partial class AdminView : Window
    {
        public AdminView()
        {
            InitializeComponent();
            this.DataContext = new AdminViewModel(this);
        }
        protected void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var view = CollectionViewSource.GetDefaultView((DataContext as AdminViewModel).RecipeList);
            view.Filter = o => (o as vwRecipe).RecipeName.Contains((sender as TextBox).Text);
        }
        protected void TextBox1_TextChanged(object sender, TextChangedEventArgs e)
        {
            var view = CollectionViewSource.GetDefaultView((DataContext as AdminViewModel).RecipeList);
            view.Filter = o => (o as vwRecipe).Type.Contains((sender as TextBox).Text);
        }
    }
}
