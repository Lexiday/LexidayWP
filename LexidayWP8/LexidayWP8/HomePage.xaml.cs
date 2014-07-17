using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using LexidayWP8.Resources;

using Parse;
using System.Windows.Documents;
using System.Windows.Media;

namespace LexidayWP8
{
    public partial class HomePage : PhoneApplicationPage
    {
        public HomePage()
        {
            InitializeComponent();
            BuildLocalizedApplicationBar();
            LoadData();
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {

            while (NavigationService.CanGoBack)
            {
                NavigationService.RemoveBackEntry();
            }
            if (ParseUser.CurrentUser == null)
            {
                NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.RelativeOrAbsolute));
            }

            
            
        }

        private async void LoadData()
        {
            var query = ParseObject.GetQuery("Words").WhereLessThanOrEqualTo("createdAt", DateTime.Now.ToUniversalTime()).OrderBy("createdAt").OrderByDescending("createdAt");
            query = query.Limit(1);
            IEnumerable<ParseObject> wordQ = await query.FindAsync();
            ParseObject wordF = wordQ.ElementAt(0);

            string word = wordF.Get<string>("word");
            string pron = wordF.Get<string>("pron");
            string typeOfWord = wordF.Get<string>("typeOfWord");
            string definition = wordF.Get<string>("definition");

            wordOfTheDay.Text = word;
            lexidaySay.Text = pron;
            lexidayPoS.Text = typeOfWord;



            RichTextBox myRTB = rtb;
            Run myRun1 = new Run();
            myRun1.Text = definition;

            Paragraph myPara = new Paragraph();
            myPara.Inlines.Add(myRun1);
            myRTB.Blocks.Add(myPara);


        }
        // Sample code for building a localized ApplicationBar
        private void BuildLocalizedApplicationBar()
        {
            // Set the page's ApplicationBar to a new instance of ApplicationBar.
            ApplicationBar = new ApplicationBar();
            ApplicationBar.ForegroundColor = Colors.White;

            ApplicationBar.BackgroundColor = Color.FromArgb(255, 138, 3, 3);

            // Create a new menu item with the localized string from AppResources.
            ApplicationBarMenuItem logout = new ApplicationBarMenuItem("log out");
            logout.Click += logout_Click;
            ApplicationBar.MenuItems.Add(logout);
        }

        void logout_Click(object sender, EventArgs e)
        {
            ParseUser.LogOut();
            var currentUser = ParseUser.CurrentUser;
            NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
        }

    }
}