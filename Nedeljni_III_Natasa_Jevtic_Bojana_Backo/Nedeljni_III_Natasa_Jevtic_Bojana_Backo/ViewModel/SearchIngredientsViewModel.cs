using Nedeljni_III_Natasa_Jevtic_Bojana_Backo.Commands;
using Nedeljni_III_Natasa_Jevtic_Bojana_Backo.View;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace Nedeljni_III_Natasa_Jevtic_Bojana_Backo.ViewModel
{
    class SearchIngredientsViewModel : ViewModelBase
    {
        SearchIngredients searchIngredients;

        private ObservableCollection<string> ingredientList;
        public ObservableCollection<string> IngredientList
        {
            get
            {
                return ingredientList;
            }
            set
            {
                ingredientList = value;
                OnPropertyChanged("IngredientList");
            }
        }

        private string ingredient;
        public string Ingredient
        {
            get
            {
                return ingredient;
            }
            set
            {
                ingredient = value;
                OnPropertyChanged("Ingredient");
            }
        }

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

        private ICommand search;

        public ICommand Search
        {
            get
            {
                if (search == null)
                {
                    search = new RelayCommand(param => SearchExecute(), param => CanSearchExecute());
                }
                return search;
            }
        }

        private ICommand addIngredient;

        public ICommand AddIngredient
        {
            get
            {
                if (addIngredient == null)
                {
                    addIngredient = new RelayCommand(param => AddIngredientExecute(), param => CanAddIngredientExecute());
                }
                return addIngredient;
            }
        }

        private ICommand removeIngredient;

        public ICommand RemoveIngredient
        {
            get
            {
                if (removeIngredient == null)
                {
                    removeIngredient = new RelayCommand(param => RemoveIngredientExecute(), param => CanRemoveIngredientExecute());
                }
                return removeIngredient;
            }
        }


        public SearchIngredientsViewModel(SearchIngredients searchIngredientsOpen)
        {
            searchIngredients = searchIngredientsOpen;
            ingredientList = new ObservableCollection<string>();
        }

        private void CancelExecute()
        {
            try
            {
                MessageBoxResult result = MessageBox.Show("Are you sure you want cancel?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    searchIngredients.Close();
                }
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

        private void AddIngredientExecute()
        {
            if (Ingredient == null || String.IsNullOrEmpty(Ingredient))
            {
                MessageBox.Show("Please enter name of ingredient.", "Notification");
            }
            else if (IngredientList != null && IngredientList.Contains(Ingredient))
            {
                MessageBox.Show("You already added this ingredient.", "Notification");
            }
            else
            {
                IngredientList.Add(Ingredient);
            }
        }

        private bool CanAddIngredientExecute()
        {
            return true;
        }

        private void SearchExecute()
        {
            if (IngredientList == null || IngredientList.Count == 0)
            {
                MessageBox.Show("Please add ingredients.", "Notification");
            }
            else
            {
                try
                {
                    MessageBoxResult result = MessageBox.Show("Next click on button for search.", "Notification");
                    foreach (var item in IngredientList)
                    {
                        SearchIngredients.ingredientsToSearch.Add(item);
                    }
                    searchIngredients.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }
        private bool CanSearchExecute()
        {
            return true;
        }

        public void RemoveIngredientExecute()
        {
            try
            {
                if (Ingredient != null)
                {
                    IngredientList.Remove(Ingredient);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public bool CanRemoveIngredientExecute()
        {
            return true;
        }
    }
}
