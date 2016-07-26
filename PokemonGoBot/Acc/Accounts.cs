using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows;
using PokemonGo.RocketAPI.Enums;

namespace PokemonGoBot.Acc
{
    public static class Accounts
    {
        public static Account SelectedAccount;

        public static List<Account> AccountsList = new List<Account>();

        public static string FilePath => Assembly.GetExecutingAssembly().GetDirectoryPath() + "\\accounts.txt";

        public static string GetDirectoryPath(this Assembly assembly)
        {
            var filePath = new Uri(assembly.CodeBase).LocalPath;
            return Path.GetDirectoryName(filePath);
        }

        public static void GetAllSavedAccounts()
        {
            if (!File.Exists(FilePath))
            {
                MessageBox.Show("Error");
                return;
            }

            var lines = File.ReadAllLines(FilePath);
            /*
            foreach (var line in lines.Where(l => !l.Contains("--") && !string.IsNullOrEmpty(l)))
            {
                
                //TODO ADD more account info
                var split = line.Split('|');
                var login = split[0];
                var pass = split[1];
                var authMethod = GetAuthMethod(split[2]);
                var lat = double.Parse(split[3]);
                //var lng = double.Parse(split[4]);
                //var alt = double.Parse(split[5]);
                AccountsList.Add(new Account(login, pass, authMethod, lat, 10, 10));
            }*/
        }

        public static void SaveAccounts()
        {
            if (!File.Exists(FilePath))
            {
                MessageBox.Show("Error");
                return;
            }

            foreach (var acc in AccountsList)
            {
                var lines = File.ReadAllLines(FilePath);
                var sameAcc = lines.FirstOrDefault(l => l.Contains(acc.Login));
                if (sameAcc == null)
                {
                    using (var writer = new StreamWriter(FilePath, true))
                    {
                        const string sep = "|";
                        writer.WriteLine(acc.Login + sep + acc.Password + sep + acc.Lat + sep + acc.Lng + sep + acc.Alt);
                    }
                }
            }
        }

        private static AuthType GetAuthMethod(string x)
        {
            switch (x)
            {
                case "Google":
                    return AuthType.Google;
                case "Ptc":
                    return AuthType.Ptc;
            }
            return AuthType.Google;
        }
    }
}