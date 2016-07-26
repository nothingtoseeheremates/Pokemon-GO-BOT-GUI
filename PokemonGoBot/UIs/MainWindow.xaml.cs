using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;
using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsPresentation;
using PokemonGo.RocketAPI.Enums;
using PokemonGoBot.Acc;

namespace PokemonGoBot.UIs
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private AddAccount addAccountWindow;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            #region Map

            map.MapProvider = GMapProviders.GoogleMap;
            map.Manager.Mode = AccessMode.ServerOnly;
            map.Position = new PointLatLng(40.785091, -73.968285);
            var marker = new GMapMarker(new PointLatLng(40.785091, -73.968285));
            marker.Shape = new Ellipse
            {
                Width = 16,
                Height = 16,
                Stroke = Brushes.Red,
                StrokeThickness = 3,
                //Fill = Brushes.Red
            };

            map.Markers.Add(marker);

            map.MinZoom = 17;
            map.MaxZoom = 17;

            map.MouseWheelZoomType = MouseWheelZoomType.MousePositionWithoutCenter;
            map.CenterCrossPen = new Pen();

            //Set zoom
            map.Zoom = 17;
            map.ShowCenter = true;

            #endregion Map

            var timer = new DispatcherTimer
            {
                Interval = new TimeSpan(0, 0, 1)
            };
            timer.Start();
            timer.Tick += delegate
            {
                #region Delete Old Logs

                if (Regex.Split(textBlock.Text, "\r\n|\r|\n").Length >= 100)
                {
                    var lines = Regex.Split(textBlock.Text, "\r\n|\r|\n").Skip(2);
                    var newText =  string.Join(Environment.NewLine, lines.ToArray());
                    textBlock.Text = newText;
                }
                #endregion Delete Old Logs


                AddLog("Used a pokestop and got");

                #region SelectedAccountTick

                if (!string.IsNullOrEmpty(accountsComboBox.Text))
                {
                    if (Accounts.SelectedAccount == null)
                    {
                        var account = Accounts.AccountsList.FirstOrDefault(acc => acc.Login == accountsComboBox.Text);
                        if (account != null)
                        {
                            Accounts.SelectedAccount = account;
                            accountLabel.Content = account.Login;
                        }
                    }
                    else
                    {
                        if (Accounts.SelectedAccount.Login != accountsComboBox.Text)
                        {
                            var account = Accounts.AccountsList.FirstOrDefault(acc => acc.Login == accountsComboBox.Text);
                            if (account != null)
                            {
                                Accounts.SelectedAccount = account;
                                accountLabel.Content = account.Login;
                            }
                        }
                    }
                }

                #endregion SelectedAccountTick

            };

            #region SavedAccs

            Accounts.GetAllSavedAccounts();

            foreach (var acc in Accounts.AccountsList)
            {
                var sameOnList = dataGrid.Items.Cast<DataGridItem>().FirstOrDefault(i => i.Login == acc.Login);
                if (sameOnList == null)
                {
                    dataGrid.Items.Add(new DataGridItem
                    {
                        Login = acc.Login,
                        Password = acc.Password,
                        Auth = acc.AuthType
                    });
                }
            }

            #endregion SavedAccs
        }

        private void addAccountButton_Click(object sender, RoutedEventArgs e)
        {
            addAccountWindow = new AddAccount();

            addAccountWindow.Show();

            addAccountWindow.Closed += AddAccountWindow_Closed;
        }

        private void AddAccountWindow_Closed(object sender, EventArgs e)
        {
            foreach (var acc in Accounts.AccountsList)
            {
                var sameOnList = dataGrid.Items.Cast<DataGridItem>().FirstOrDefault(i => i.Login == acc.Login);
                if (sameOnList == null)
                {
                    dataGrid.Items.Add(new DataGridItem
                    {
                        Login = acc.Login,
                        Password = acc.Password,
                        Auth = acc.AuthType
                    });
                }
            }
            addAccountWindow.Closed -= AddAccountWindow_Closed;
        }

        //Delete context button
        private void MenuItem_Click_Delete(object sender, RoutedEventArgs e)
        {
            var item = (DataGridItem) dataGrid.SelectedItem;

            if (item == null)
            {
                MessageBox.Show("You didnt click on an account");
                return;
            }

            var accToDelete = Accounts.AccountsList.FirstOrDefault(a => a.Login == item.Login);

            if (accToDelete == null) return;

            Accounts.AccountsList.Remove(accToDelete);
            dataGrid.Items.Remove(item);
        }

        private void startButton_Click(object sender, RoutedEventArgs e)
        {
            foreach (var acc in Accounts.AccountsList)
            {
                //var a = new Account(acc.Login, acc.Password, AuthType.Google);
                //a.StartBot();
            }
        }

        private void refreshButton_Click(object sender, RoutedEventArgs e)
        {
            foreach (var item in accountsComboBox.Items)
            {
                accountsComboBox.Items.Remove(item);
            }

            foreach (var acc in Accounts.AccountsList)
            {
                accountsComboBox.Items.Add(acc.Login);
            }
        }

        private class DataGridItem
        {
            public string Login { get; set; }
            public string Password { get; set; }
            public AuthType Auth { get; set; }
        }

        public void AddLog(string text, string time = null)
        {
            if (time == null)
            {
                time = DateTime.Now.ToLongTimeString();
            }
            textBlock.Text += "[" + time + "] : " + text + "\n";
            if (scrollViewer.IsMouseOver) return;
            scrollViewer.ScrollToEnd();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            Accounts.SaveAccounts();
        }

        private void pokemonButton_Click(object sender, RoutedEventArgs e)
        {
            var pokeWindow = new PokemonWindow();
            pokeWindow.Show();
        }

        private void itemsButton_Click(object sender, RoutedEventArgs e)
        {
            var itemWindow = new ItemsWindow();
            itemWindow.Show();
        }
    }
}