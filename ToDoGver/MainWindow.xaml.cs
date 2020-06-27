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
using System.Windows.Threading;
using ToDoGver.OtherClasses;
using ToDoGver.OtherWindows.EventsWindows;
using ToDoGver.OtherWindows.ShopWindows;

namespace ToDoGver
{
    /// <summary>
    /// Interakční logika pro MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        FastTask ft = new FastTask();
        UserData ud = new UserData();
        Shop sp = new Shop();
        

        // We need this to save OTE item id for remove
        int checkboxOTEid;

        public MainWindow()
        {
            InitializeComponent();
            loading_UserData();
            TickTimerCreator();
        }

        #region Functions
        // Loading data like gold, hp, etc.
        public void loading_UserData()
        {
            label_gold.Content = ud.Gold.ToString();
            label_diamond.Content = ud.Dia.ToString();
        }

        #region Shop functions
        // This loads data like price, stock, etc
        private void GetItemdatas(int tag)
        {
            try
            {
                BitmapImage ImageCache = new BitmapImage();
                ImageCache.BeginInit();
                ImageCache.UriSource = new Uri(sp.findImageSource(tag));
                ImageCache.EndInit();
                ShopImageBig.Source = ImageCache;
                label_GoldPrice.Content = sp.findGoldPrice(tag);
                label_DiaPrice.Content = sp.findDiaPrice(tag);
                TB_ShopItemDescribtion.Text = sp.findDescribtion(tag);
                label_itemStock.Content = sp.findIteminStock(tag);
            }
            catch
            {

            }
        }
        #endregion

        #region OTE_Functions
        void AddFastTask()
        {
            string nametask = TB_OnetimeTaskName.Text.ToString();
            if (!(nametask.Equals("") || nametask == null))
            {
                ft.AddItems(nametask);
                LB_OneTimeEvent.Items.Refresh();
            }
            else
            {
                MessageBox.Show("Please give a name for the event", "Attention", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        byte CompleteFastTask(int eventID)
        {
            MessageBoxResult dr;
            dr = MessageBox.Show("Complete task? +" + ud.OTE_gold.ToString() + " gold(s) +" + ud.OTE_dia.ToString() + " diamond(s)", "Complete task", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (dr == MessageBoxResult.Yes)
            {
                ft.DeleteEvent(eventID);
                LB_OneTimeEvent.Items.Refresh();
                ud.AddGold(ud.OTE_gold);
                ud.AddDiamond(ud.OTE_dia);
                label_gold.Content = ud.Gold;
                label_diamond.Content = ud.Dia;
                return 0;
            }
            else
            {
                return 1;
            }
        }

        public void RemoveFastTask(int eventID)
        {
            ft.DeleteEvent(eventID);
            LB_OneTimeEvent.Items.Refresh();
        }
        #endregion
        #endregion

        #region Events
        #region Onetask events
        // if check -> complete task
        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            string eventIndex = (sender as CheckBox).Tag.ToString();
            byte fail = CompleteFastTask(Int32.Parse(eventIndex));
            if (fail == 1)
            {
                (sender as CheckBox).IsChecked = false;
            }
        }

        // click btn to add trask
        private void BtnAddFtask_Click(object sender, RoutedEventArgs e)
        {
            AddFastTask();
        }

        // get and save OTE item id to checkboxOTEid
        private void CheckBoxOTE_RightClick(object sender, MouseButtonEventArgs e)
        {
            string eventIndex = (sender as CheckBox).Tag.ToString();
            checkboxOTEid = (Int32.Parse(eventIndex));
        }

        // event remove for content menu, it use checkboxOTEid to find item id and then remove.
        private void CB_Item_RightCLick(object sender, RoutedEventArgs e)
        {
            MessageBoxResult dr;
            dr = MessageBox.Show("Remove task?", "Remove", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if(dr == MessageBoxResult.Yes)
            {
                RemoveFastTask(checkboxOTEid);
            }
        }
        #endregion

        #region Shop events
        int SP_tag_index = -1;
        private void StackPanel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            StackPanel tagSpanel = sender as StackPanel;
            //MessageBox.Show(tagSpanel.Tag.ToString());
            int tag = Int32.Parse(tagSpanel.Tag.ToString());
            GetItemdatas(tag);
            SP_tag_index = tag;
        }
        #endregion

        #region Other events
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ft.LoadFile();
            LB_OneTimeEvent.ItemsSource = ft.ListOneTimeEvents;
            LB_ShopItems.ItemsSource = sp.ListShopItems;
        }





        #endregion

        #endregion
        AddShopItems ASI = new AddShopItems();
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            ASI.Show();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        public void ReloadShop()
        {
            LB_ShopItems.Items.Refresh();
        }

        private void TickTimerCreator()
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if(!ASI.ItemName.Equals("") && !ASI.PicLocation.Equals(""))
            {
                sp.AddProduct(ASI.ItemName, ASI.ItemPriceG, ASI.ItemPriceD, ASI.ItemStock, ASI.HP_Recover, ASI.Mb_Recover, ASI.ItemDescribtion, ASI.PicLocation);
                ASI.clearTB();
                ASI.ClearData();
                ReloadShop();
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //     string productName = sp._productName;
            //int prodPriceG = sp._prodPriceG;
            // int prodPriceD = sp._prodPriceD;
            // int prodStock = sp._prodStock;
            // int prodHpRec = sp._prodHpRec;
            // int prodMbRec = sp._prodMbRec;
            // string ProdDescribtion = sp._ProdDescribtion;
            // string URL = sp._URL;
           
            //sp.AddProduct("a", 1, 1, 1, 1, 1, "desc", "E:/Download/pixel drawing/Bin2Icon.png");  
            ReloadShop();
        }
        //
        //
        //
        // PROBLEM IN ADD ID, IS NOT UNIQUE
        // MESSAGE BOX FOR ASKING DELETE OR NOT

        //

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if(SP_tag_index >= 0)
            {
                sp.RemoveShopItem(SP_tag_index);
                ReloadShop();
            }
            else
            {
                MessageBox.Show("Select item to remove");
            }
        }
    }
}

#region Old codes
//void removeOneTimeEvent()
//{

//}

//void add_event()
//{
//    List<OneTimeEvent> ev = new List<OneTimeEvent>();
//    ev.Add(new OneTimeEvent() { ID = 1, event_name = "Head" });
//    //{
//    //    new OneTimeEvent()
//    //    {
//    //        event_name = "Head",
//    //        list_subevent = new List<SubEvent>()
//    //        {
//    //            new SubEvent(){sub_event = "1"},
//    //            new SubEvent(){sub_event = "2"}
//    //        }
//    //    }
//    //};
//    LB_OneTimeEvent.ItemsSource = ev;
//    //ev.RemoveAt(0);
//    //MessageBox.Show(ev[0].ID.ToString());
//}


//    public class OneTimeEvent
//{
//    public int ID { get; set; }
//    public string event_name { get; set; }
//    public List<SubEvent> list_subevent { get; set; }
//}

//public class SubEvent
//{
//    public string sub_event { get; set; }
//}

#endregion