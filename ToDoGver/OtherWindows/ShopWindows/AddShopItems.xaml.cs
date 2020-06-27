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

        public string ItemName = "null";
        public int ItemPriceG = 0;
        public int ItemPriceD = 0;
        public int ItemStock = 0;
        public int HP_Recover = 0;
        public int Mb_Recover = 0;
        public string ItemDescribtion = "null";
        public string PicLocation = "";

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //string ItemName = "null";
            //int ItemPriceG = 0;
            //int ItemPriceD = 0;
            //int ItemStock = 0;
            //int HP_Recover = 0;
            //int Mb_Recover = 0;
            //string ItemDescribtion = "null";
            //string PicLocation = "";
            
            if (!TB_ItemName.Text.Contains(";"))
                ItemName = TB_ItemName.Text;
            
            Int32.TryParse(TB_PriceGold.Text, out ItemPriceG);
            if(ItemPriceG >= 10000000)
            {
                ItemPriceG = 9999999;
            }
            Int32.TryParse(TB_PriceDia.Text, out ItemPriceD);
            if (ItemPriceD >= 1000000)
            {
                ItemPriceD = 999999;
            }
            Int32.TryParse(TB_ItemStock.Text, out ItemStock);
            if (ItemStock >= 10000)
            {
                ItemStock = 9999;
            }

            Int32.TryParse(TB_HpRecover.Text, out HP_Recover);
            if (HP_Recover >= 1000)
            {
                HP_Recover = 1000;
            }

            Int32.TryParse(TB_MbRecover.Text, out Mb_Recover);
            if (Mb_Recover >= 300)
            {
                Mb_Recover = 300;
            }

            if (!TB_ItemDescribtion.Text.Contains(";"))
                ItemDescribtion = TB_ItemDescribtion.Text;
            
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
            //Shop.AddProduct(ItemName,ItemPriceG,ItemPriceD,ItemStock,HP_Recover,Mb_Recover,ItemDescribtion,PicLocation);
            this.Hide();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        public void clearTB()
        {
            TB_ItemName.Text = "";
            TB_PriceGold.Text = "";
            TB_PriceDia.Text = "";
            TB_ItemStock.Text = "";
            TB_ItemDescribtion.Text = "";
            TB_HpRecover.Text = "";
            TB_MbRecover.Text = "";
        }

        public void ClearData()
        {
            ItemName = "";
            ItemPriceG = 0;
            ItemPriceD = 0;
            ItemStock = 0;
            ItemDescribtion = "";
            HP_Recover = 0;
            Mb_Recover = 0;
            PicLocation = "";
        }
    }
}
