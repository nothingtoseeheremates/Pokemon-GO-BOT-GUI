using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PokemonGo.RocketAPI.Enums;

namespace PokemonGoBot.Acc
{
    public class Account
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public AuthType AuthType { get; set; }
        public double Lat { get; set; }
        public double Lng { get; set; }
        public double Alt { get; set; }

        public AccountSettings Settings { get; set; }



        public Account(string login, string pass, AuthType type, double lat, double lng, double alt)
        {
            Login = login;
            if (type == AuthType.Google)
            {
                Login = "Google";
            }
            Password = pass;
            AuthType = type;
            Lat = lat;
            Lng = lng;
            Alt = alt;
        }
    }
}
