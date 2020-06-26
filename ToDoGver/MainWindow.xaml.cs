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
        private void StackPanel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            StackPanel tagSpanel = sender as StackPanel;
            //MessageBox.Show(tagSpanel.Tag.ToString());
            int tag = Int32.Parse(tagSpanel.Tag.ToString());
            GetItemdatas(tag);
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddShopItems ASI = new AddShopItems();
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