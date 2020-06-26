using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows;

namespace ToDoGver.OtherClasses
{
    class Shop
    {
        // Path to the user data file
        string filePath = @"E:\Project\C#\GverReminder\ToDoGver\GverReminder\ToDoGver\Data\ShopData.txt";

        // List to store data to save to the file
        public List<ItemTranslater> ListShopItems = new List<ItemTranslater>();

        // The list to load file text to parse to the ListOneTimeEvents
        private List<string> lines;

        public Shop()
        {
            LoadFile();
        }

        // Simple load data to ListShopItems from filePath, it separates
        private void LoadFile()
        {
            try
            {
                lines = File.ReadAllLines(filePath).ToList();
                foreach (var line in lines)
                {
                    string[] entries = line.Split(';');
                    ItemTranslater itemTranslater = new ItemTranslater();
                    // -->
                    // ID,ItemName,PriceG,PriceD,Stock,HPbonus,MBbonus,describtion,pictureURL
                    itemTranslater.Id = Int32.Parse(entries[0]);
                    itemTranslater.ItemName = entries[1];
                    itemTranslater.PriceGold = Int32.Parse(entries[2]);
                    itemTranslater.PriceDiamond = Int32.Parse(entries[3]);
                    itemTranslater.ItemStock = Int32.Parse(entries[4]);
                    itemTranslater.HpBonus = Int32.Parse(entries[5]);
                    itemTranslater.MBbonus = Int32.Parse(entries[6]);
                    itemTranslater.Describtion = entries[7];
                    itemTranslater.PictureURL = entries[8];
                    ListShopItems.Add(itemTranslater);
                }
            }
            catch
            {
                MessageBox.Show("Can´t load shop data file");
            }
        }

        public void AddProduct(string productName, int prodPriceG, int prodPriceD, int prodStock, int prodHpRec, int prodMbRec, string ProdDescribtion, string URL)
        {
            List<string> output = new List<string>();
            int[] arrayID = new int[99999];
            foreach(var index in ListShopItems)
            {
                int i = 0;
                arrayID[i] = index.Id;
                i++;
            }
            ListShopItems.Add(new ItemTranslater()
            {
                Id = arrayID.Max() + 1,
                ItemName = productName,
                PriceGold = prodPriceG,
                PriceDiamond = prodPriceD,
                ItemStock = prodStock,
                HpBonus = prodHpRec,
                MBbonus = prodMbRec,
                Describtion = ProdDescribtion,
                PictureURL = URL
            }
                );
            foreach (var item in ListShopItems)
            {
                output.Add($"{item.Id};{item.ItemName};{item.PriceGold};{item.PriceDiamond};{item.ItemStock};{item.HpBonus};{item.MBbonus};{item.Describtion};{item.PictureURL}");
                File.WriteAllLines(filePath, output);
            }
        }


        #region Finder function
        // These methods can find data like url, price, etc. with id
        public string findImageSource(int id)
        {
            string picture_id = "";
            foreach(var index in ListShopItems)
            {
                if(index.Id == id)
                {
                    picture_id = index.PictureURL;
                }
            }
            return picture_id;
        }

        public int findGoldPrice(int id)
        {
            int golds = -1;
            foreach (var index in ListShopItems)
            {
                if (index.Id == id)
                {
                    golds = index.PriceGold;
                }
            }
            return golds;
        }

        public int findDiaPrice(int id)
        {
            int dia = -1;
            foreach (var index in ListShopItems)
            {
                if (index.Id == id)
                {
                    dia = index.PriceDiamond;
                }
            }
            return dia;
        }

        public int findIteminStock(int id)
        {
            int stock = -1;
            foreach (var index in ListShopItems)
            {
                if (index.Id == id)
                {
                    stock = index.ItemStock;
                }
            }
            return stock;
        }

        public string findDescribtion(int id)
        {
            string picture_describtion = "";
            foreach (var index in ListShopItems)
            {
                if (index.Id == id)
                {
                    picture_describtion = index.Describtion;
                }
            }
            return picture_describtion;
        }
        #endregion
    }

    class ItemTranslater : IEquatable<ItemTranslater>
    {
        public int Id { get; set; }
        public string ItemName { get; set; }
        public int PriceGold { get; set; }
        public int PriceDiamond { get; set; }
        public int ItemStock { get; set; }
        public int HpBonus { get; set; }
        public int MBbonus { get; set; }
        public string Describtion { get; set; }
        public string PictureURL { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            ItemTranslater objAsPart = obj as ItemTranslater;
            if (objAsPart == null) return false;
            else return Equals(objAsPart);
        }

        public override int GetHashCode()
        {
            return Id;
        }

        public bool Equals(ItemTranslater other)
        {
            if (other == null) return false;
            return (this.Id.Equals(other.Id));
        }
    }
}
