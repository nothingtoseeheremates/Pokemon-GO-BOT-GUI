using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PokemonGoBot
{
    internal class Updater
    {
        private static bool UpdaterEnabled;
        private static readonly WebClient WClient = new WebClient();

        private static bool NeedUpdate()
        {
            return false;
        }

        public static void Update()
        {
            if(!UpdaterEnabled || !NeedUpdate())return;

           

        }
}
}
