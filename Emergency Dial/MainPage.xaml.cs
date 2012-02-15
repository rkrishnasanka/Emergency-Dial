using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using System.Windows.Media.Imaging;
using Microsoft.Phone.Tasks;
using Microsoft.Phone.Shell;

namespace Emergency_Dial
{
    public partial class MainPage : PhoneApplicationPage
    {
        List<contact> EmergencyContacts;
        // Constructor
        public MainPage()
        {
            InitializeComponent();

            EmergencyContacts = new List<contact>();
            EmergencyContacts.Add(new contact { Name = "Fire", Phnumber="04-997", Img="Images\\911icon-fire-01.png"});             
            EmergencyContacts.Add(new contact { Name = "Ambulance", Phnumber = "04-998",Img="Images\\911icon-ambulance-01.png" });
            EmergencyContacts.Add(new contact { Name = "Emergency", Phnumber = "04-998" ,Img="Images\\911icon-emergency-01.png"});
            
           // this.DataContext = EmergencyContacts;
            ListBox_Contacts.ItemsSource = EmergencyContacts;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("haha");
        }

        private void editButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("haha");
        }

        private void pinButton_Click(object sender, RoutedEventArgs e)
        {
            Button b = sender as Button;
            contact c = b.DataContext as contact;
            //var tile = new ShellTile(
            ShellTile tile = ShellTile.ActiveTiles.FirstOrDefault(x => x.NavigationUri.ToString().Contains(c.Phnumber));
            if (tile == null)
            {
                StandardTileData tiledata = new StandardTileData { Title = "", BackgroundImage = new Uri(c.Img, UriKind.Relative) ,BackBackgroundImage=null ,BackContent=null , BackTitle = null , Count = null };
                ShellTile.Create(new Uri("/MainPage.xaml?Name="+c.Name+"&PhNumber="+c.Phnumber, UriKind.Relative),tiledata);
            }
            else
            {
                MessageBox.Show("Tile exists");
            }
        }


        private void ListBox_Contacts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBox lb = sender as ListBox;
            if (lb.SelectedItem != null)
            {
                contact c = lb.SelectedItem as contact;
                c.call();
            }
            lb.SelectedItem = null;
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e) 
        {
            if(NavigationContext.QueryString.Count != 0)
            {
                string Name = NavigationContext.QueryString["Name"];
                string Phnumber = NavigationContext.QueryString["PhNumber"];
                
                    PhoneCallTask call = new PhoneCallTask { DisplayName = Name + " Services", PhoneNumber = Phnumber };
                    call.Show();
                
            }
            base.OnNavigatedTo(e); 
        }
    }

    public class contact
    {
        private string _img;

        public string Img
        {
            get { return _img; }
            set { _img = value; }
        }
        private string _name;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        private string _phnumber;

        public string Phnumber
        {
            get { return _phnumber; }
            set { _phnumber = value; }
        }

        public void call()
        {
            PhoneCallTask task = new PhoneCallTask();
            task.PhoneNumber = this._phnumber;
            task.DisplayName = this.Name + " services";
            task.Show();
        }
    }
}