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

namespace LexidayWP8
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();

            // Sample code to localize the ApplicationBar
            //BuildLocalizedApplicationBar();
        }
        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {

            while (NavigationService.CanGoBack)
            {
                NavigationService.RemoveBackEntry();
            }
            if (ParseUser.CurrentUser != null)
            {
                NavigationService.Navigate(new Uri("/HomePage.xaml", UriKind.RelativeOrAbsolute));
            }
        }


        private void userNameBox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                passwordBox.Focus();
            }
        }

        private void passwordBox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                this.Focus();
                loginButton_Click(sender, e);
            }
        }

        private async void loginButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                await ParseUser.LogInAsync(userNameBox.Text, passwordBox.Password);
                NavigationService.Navigate(new Uri("/HomePage.xaml", UriKind.RelativeOrAbsolute));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Login failed");
                // The login failed. Check the error to see why.
            }
        }

        private async void signupButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var user = new ParseUser()
                {
                    Username = userNameBox.Text,
                    Password = passwordBox.Password,
                    Email = userNameBox.Text
                };


                await user.SignUpAsync();
                MessageBox.Show("Sign Up was Successful");
            }
            catch
            {
                MessageBox.Show("Sign Up Failed");
            }
        }

        // Sample code for building a localized ApplicationBar
        //private void BuildLocalizedApplicationBar()
        //{
        //    // Set the page's ApplicationBar to a new instance of ApplicationBar.
        //    ApplicationBar = new ApplicationBar();

        //    // Create a new button and set the text value to the localized string from AppResources.
        //    ApplicationBarIconButton appBarButton = new ApplicationBarIconButton(new Uri("/Assets/AppBar/appbar.add.rest.png", UriKind.Relative));
        //    appBarButton.Text = AppResources.AppBarButtonText;
        //    ApplicationBar.Buttons.Add(appBarButton);

        //    // Create a new menu item with the localized string from AppResources.
        //    ApplicationBarMenuItem appBarMenuItem = new ApplicationBarMenuItem(AppResources.AppBarMenuItemText);
        //    ApplicationBar.MenuItems.Add(appBarMenuItem);
        //}
    }
}