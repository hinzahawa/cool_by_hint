using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Shapes;
using cool_cal_by_hint.masterData;
using static System.Net.Mime.MediaTypeNames;

namespace cool_cal_by_hint
{
    /// <summary>
    /// Interaction logic for tableProduct.xaml
    /// </summary>
    public partial class TableProduct : Window
    {
        public TableProduct()
        {
            InitializeComponent();
            productList.ItemsSource = SpecProducts.specProductList ;
        }


        private void productList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = productList.SelectedIndex;
            if (index > -1)
            {
                SpecProducts.SetSelectedProduct(index);
                if (SpecProducts.selectedProduct != null)
                {
                    string imagePath_1 = SpecProducts.selectedProduct.image_1;
                    string imagePath_2 = SpecProducts.selectedProduct.image_2;
                    Uri imageUri_1 = new Uri(imagePath_1, UriKind.Relative);
                    Uri imageUri_2 = new Uri(imagePath_2, UriKind.Relative);
                    
                    BitmapImage bitmap1 = new BitmapImage(imageUri_1);
                    BitmapImage bitmap2 = new BitmapImage(imageUri_2);

                    imageFan1.Source = bitmap1;
                    imageFan2.Source = bitmap2;
                } ;
            }
        }
    }
}
