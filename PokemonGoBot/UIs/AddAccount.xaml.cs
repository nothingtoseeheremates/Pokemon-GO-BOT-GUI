using System.Linq;
using System.Windows;
using PokemonGo.RocketAPI.Enums;
using PokemonGoBot.Acc;

namespace PokemonGoBot.UIs
{
    /// <summary>
    ///     Interaction logic for AddAccount.xaml
    /// </summary>
    public partial class AddAccount
    {
        public AddAccount()
        {
            InitializeComponent();
        }

        private void addAccountButton_Click(object sender, RoutedEventArgs e)
        {
            var login = loginBox.Text;
            var password = passwordBox.Text;
            var authType = comboBox.SelectedIndex == 0 ? AuthType.Ptc : AuthType.Google;

            var sameAcc = Accounts.AccountsList.FirstOrDefault(a => a.Login == login);
            if (sameAcc == null)
            {
                Accounts.AccountsList.Add(new Account(login, password, authType, 10, 10 , 10));
            }
            else
            {
                MessageBox.Show("There is already an acc with that name");
                return;
            }

            Close();
        }
    }
}