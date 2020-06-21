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
using ToDoGver.OtherWindows.EventsWindows;

namespace ToDoGver
{
    /// <summary>
    /// Interakční logika pro MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        FastTask ft = new FastTask();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ft.LoadFile();
            LB_OneTimeEvent.ItemsSource = ft.ListOneTimeEvents;
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            string eventIndex = (sender as CheckBox).Tag.ToString();
            byte fail = RemoveFastTask(Int32.Parse(eventIndex));
            if(fail == 1)
            {
                (sender as CheckBox).IsChecked = false;
            }
        }

        private void BtnAddFtask_Click(object sender, RoutedEventArgs e)
        {
            AddFastTask();
        }

        void AddFastTask()
        {
            string nametask = TB_OnetimeTaskName.Text.ToString();
            if(!(nametask.Equals("") || nametask == null))
            {
                ft.AddItems(nametask);
                LB_OneTimeEvent.Items.Refresh();
            }
            else
            {
                MessageBox.Show("Please give a name for the event", "Attention",MessageBoxButton.OK,MessageBoxImage.Information);
            }        
        }

        byte RemoveFastTask(int eventID)
        {
            MessageBoxResult dr;
            dr = MessageBox.Show("Complete task? +N/A gold + N/A diamond","Complete task",MessageBoxButton.YesNo,MessageBoxImage.Question);
            if(dr == MessageBoxResult.Yes)
            {
                ft.DeleteEvent(eventID);
                LB_OneTimeEvent.Items.Refresh();
                return 0;
            }
            else
            {
                return 1;
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