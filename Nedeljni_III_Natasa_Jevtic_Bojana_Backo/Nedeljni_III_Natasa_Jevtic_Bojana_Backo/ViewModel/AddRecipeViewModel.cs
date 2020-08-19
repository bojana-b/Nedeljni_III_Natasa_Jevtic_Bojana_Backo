using Nedeljni_III_Natasa_Jevtic_Bojana_Backo.Commands;
using Nedeljni_III_Natasa_Jevtic_Bojana_Backo.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Nedeljni_III_Natasa_Jevtic_Bojana_Backo.ViewModel
{
    class AddRecipeViewModel : ViewModelBase
    {
        AddRecipeView addRecipe;

        public AddRecipeViewModel(AddRecipeView addRecipeOpen, vwUser userLogged)
        {
            addRecipe = addRecipeOpen;
            user = userLogged;
        }
        #region Properties
        private vwUser user;
        public vwUser User
        {
            get
            {
                return user;
            }
            set
            {
                user = value;
                OnPropertyChanged("User");
            }
        }
        #endregion

        #region Commands
        // AddIngredients Button
        //private ICommand addIngredients;
        //public ICommand AddIngredients
        //{
        //    // Ostavljam tebi da implementiras
            
        //}

        // Cancel Button
        private ICommand cancel;
        public ICommand Cancel
        {
            get
            {
                if (cancel == null)
                {
                    cancel = new RelayCommand(param => CancelExecute(), param => CanCancelExecute());
                }
                return cancel;
            }
        }
        private void CancelExecute()
        {
            try
            {
                AdminView admin = new AdminView();
                addRecipe.Close();
                admin.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private bool CanCancelExecute()
        {
            return true;
        }
        #endregion
    }
}
