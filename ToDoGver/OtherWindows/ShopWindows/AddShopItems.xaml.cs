using Microsoft.Win32;
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
using System.Windows.Shapes;
using ToDoGver.OtherClasses;

namespace ToDoGver.OtherWindows.ShopWindows
{
    /// <summary>
    /// Interaction logic for AddShopItems.xaml
    /// </summary>
    public partial class AddShopItems : Window
    {
        string ImageURL = "";
        string ImageURLsplited = "";
        Shop Shop = new Shop();
        public AddShopItems()
        {
            InitializeComponent();
        }

        private void LoadImage()
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Select a picture";
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png";
            if (op.ShowDialog() == true)
            {
                ImageURL = new BitmapImage(new Uri(op.FileName)).ToString();
            }
            int startindex = 8;
            int length = ImageURL.Length - startindex;
            ImageURLsplited = ImageURL.Substring(startindex, length);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            LoadImage();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string ItemName = "null";
            if (!TB_ItemName.Text.Contains(";"))
                ItemName = TB_ItemName.Text;
            int ItemPriceG = 0;
            Int32.TryParse(TB_PriceGold.Text, out ItemPriceG);
            int ItemPriceD = 0;
            Int32.TryParse(TB_PriceDia.Text, out ItemPriceD);
            int ItemStock = 0;
            Int32.TryParse(TB_ItemStock.Text, out ItemStock);
            int HP_Recover = 0;
            Int32.TryParse(TB_HpRecover.Text, out HP_Recover);
            int Mb_Recover = 0;
            Int32.TryParse(TB_MbRecover.Text, out Mb_Recover);
            string ItemDescribtion = "null";
            if (!TB_ItemDescribtion.Text.Contains(";"))
                ItemDescribtion = TB_ItemDescribtion.Text;
            string PicLocation = "";
            PicLocation = ImageURLsplited;
            if (PicLocation.Equals("")) 
            {
                MessageBox.Show("Select a picture");
                return;
            }
            else if(PicLocation == null)
            {
                MessageBox.Show("Select a picture");
                return;
            }
            else if (PicLocation.Contains(";"))
            {
                MessageBox.Show("Select a picture");
                return;
            }
            Shop.AddProduct(ItemName,ItemPriceG,ItemPriceD,ItemStock,HP_Recover,Mb_Recover,ItemDescribtion,PicLocation);
            this.Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }
    }
}
