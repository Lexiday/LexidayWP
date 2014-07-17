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

            RichTextBox myRTB = rtb;
            Run myRun1 = new Run();
            myRun1.Text = "the action or fact of persuading someone or of being persuaded to do or believe something.";

            Paragraph myPara = new Paragraph();
            myPara.Inlines.Add(myRun1);
            myRTB.Blocks.Add(myPara);
            
        }

        private async void LoadData()
        {
            var query = ParseObject.GetQuery("Word").WhereLessThanOrEqualTo("createdAt", DateTime.Now.ToUniversalTime()).OrderBy("createdAt").OrderByDescending("createdAt");
            query = query.Limit(5);
            IEnumerable<ParseObject> results = await query.FindAsync();


        }
        // Sample code for building a localized ApplicationBar
        private void BuildLocalizedApplicationBar()
        {
            // Set the page's ApplicationBar to a new instance of ApplicationBar.
            ApplicationBar = new ApplicationBar();

            // Create a new button and set the text value to the localized string from AppResources.
            ApplicationBarIconButton appBarButton = new ApplicationBarIconButton(new Uri("/Assets/AppBar/appbar.add.rest.png", UriKind.Relative));
            appBarButton.Text = AppResources.AppBarButtonText;
            ApplicationBar.Buttons.Add(appBarButton);

            // Create a new menu item with the localized string from AppResources.
            ApplicationBarMenuItem appBarMenuItem = new ApplicationBarMenuItem(AppResources.AppBarMenuItemText);
            ApplicationBar.MenuItems.Add(appBarMenuItem);
        }

    }
}