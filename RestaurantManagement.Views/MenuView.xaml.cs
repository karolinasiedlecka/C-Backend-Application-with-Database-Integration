using System;
using System.Linq;
using System.Windows;
using RestaurantManagement.Models;
using RestaurantManagement.ViewModel;
using RestaurantManagement.DAL;

namespace RestaurantManagement.Views
{
    public partial class MenuView : Window
    {
        private Restaurant _restaurant;

        public MenuView(Restaurant selectedRestaurant)
        {
            InitializeComponent();
            _restaurant = selectedRestaurant;
            this.DataContext = new MenuViewModel(selectedRestaurant);
        }

        private void AddMenu_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = (MenuViewModel)this.DataContext;

            string priceInput = PriceTxt.Text.Replace(".", ",");

            if (!string.IsNullOrWhiteSpace(ItemNameTxt.Text) && float.TryParse(priceInput, out float price))
            {
                using (var db = new ApplicationDbContext())
                {
                    try
                    {
                        var newItem = new MenuItem
                        {
                            Name = ItemNameTxt.Text,
                            Description = string.IsNullOrWhiteSpace(DescriptionTxt.Text) ? "No description" : DescriptionTxt.Text,
                            Price = price,
                        };

                        db.MenuItem.Add(newItem);
                        db.SaveChanges();

                        viewModel.MenuItems.Add(newItem);

                        MenuDataGrid.ItemsSource = null;
                        MenuDataGrid.ItemsSource = viewModel.MenuItems;

                        ItemNameTxt.Clear();
                        DescriptionTxt.Clear();
                        PriceTxt.Clear();

                        MessageBox.Show("Dish has been successfully saved!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    catch (Exception ex)
                    {
                        string error = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                        MessageBox.Show("Database Error: " + error, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please type correct dish name and price.", "Database Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void DeleteMenu_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = (MenuViewModel)this.DataContext;
            var selected = MenuDataGrid.SelectedItem as MenuItem;

            if (selected != null)
            {
                MessageBoxResult result = MessageBox.Show($"Are you sure you want to delete '{selected.Name}'?", "Yes", MessageBoxButton.YesNo);

                if (result == MessageBoxResult.Yes)
                {
                    using (var db = new ApplicationDbContext())
                    {
                        try
                        {
                            // Znajdujemy rekord w bazie po ID
                            var itemInDb = db.MenuItem.FirstOrDefault(m => m.Id == selected.Id);
                            if (itemInDb != null)
                            {
                                db.MenuItem.Remove(itemInDb);
                                db.SaveChanges();
                            }

                            // Usuwamy z listy widocznej na ekranie
                            viewModel.MenuItems.Remove(selected);

                            // Odświeżenie tabeli
                            MenuDataGrid.ItemsSource = null;
                            MenuDataGrid.ItemsSource = viewModel.MenuItems;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Failure while adding: " + ex.Message);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Click on the dish you want to delete", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}