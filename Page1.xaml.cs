using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Dilanova41
{
    /// <summary>
    /// Логика взаимодействия для Page1.xaml
    /// </summary>
    public partial class Page1 : Page
    {
        private List<Product> allProducts; // Хранение всех продуктов

        public Page1()
        {
            InitializeComponent();
            LoadProducts();
        }

        private void LoadProducts()
        {
            allProducts = dilanova41Entities.GetContext().Product.ToList(); // Сохранение всех продуктов
            ProductListView.ItemsSource = allProducts;
            ComboType.SelectedIndex = 0;

            // Устанавливаем общее количество продуктов
            TBProductCountMaxRecords.Text = allProducts.Count.ToString();
            UpdatePages();
        }

        private void UpdatePages()
        {
            // Создание копии allProducts для фильтрации
            var filteredProducts = allProducts.ToList();

            // Фильтрация по типу
            if (ComboType.SelectedIndex == 1)
            {
                filteredProducts = filteredProducts.Where(p => p.ProductDiscountAmount >= 0 && p.ProductDiscountAmount <= 9.99).ToList();
            }
            else if (ComboType.SelectedIndex == 2)
            {
                filteredProducts = filteredProducts.Where(p => p.ProductDiscountAmount >= 10 && p.ProductDiscountAmount <= 14.99).ToList();
            }
            else if (ComboType.SelectedIndex == 3)
            {
                filteredProducts = filteredProducts.Where(p => p.ProductDiscountAmount >= 15).ToList();
            }

            // Сортировка
            if (RButtonUp.IsChecked == true)
            {
                filteredProducts = filteredProducts.OrderBy(p => p.ProductCost).ToList();
            }
            else if (RButtonDown.IsChecked == true)
            {
                filteredProducts = filteredProducts.OrderByDescending(p => p.ProductCost).ToList();
            }

            // Поиск
            filteredProducts = filteredProducts.Where(p => p.ProductName.ToLower().Contains(TBpxSearch.Text.ToLower())).ToList();

            // Обновление интерфейса
            ProductListView.ItemsSource = filteredProducts; // Отображение отфильтрованных продуктов

            // Устанавливаем количество отфильтрованных продуктов
            TBProductCountRecords.Text = filteredProducts.Count.ToString(); // Только количество отфильтрованных продуктов
        }





        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddEditPage());  
        }

        private void TBpxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdatePages();
        }

        private void ComboType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdatePages();
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void RButtonUp_Checked(object sender, RoutedEventArgs e)
        {
            UpdatePages();
        }

        private void RButtonDown_Checked(object sender, RoutedEventArgs e)
        {
            UpdatePages();
        }
    }
}
