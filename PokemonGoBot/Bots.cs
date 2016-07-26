using System.Collections.Generic;
using System.Linq;
using System.Threading;
using PokemonGo.RocketAPI;
using PokemonGoBot.Acc;

namespace PokemonGoBot
{
    public static class Bots
    {
        public static Dictionary<Account, Thread> RunningBots = new Dictionary<Account, Thread>();

        public static void StartBot(this Account acc)
        {
            var client = new Client(acc.Settings);

            var thread = new Thread(async () => await BotLogic.Run());
            RunningBots.Add(acc, thread);
            thread.Start();
        }

        public static void StopBot(this Account acc)
        {
            var findAcc = RunningBots.Keys.FirstOrDefault(c => c.Login == acc.Login && c.Password == acc.Password);
            if (findAcc == null) return;
            var thread = FindThread(acc);
            if (thread == null) return;
            thread.Abort();
            RunningBots.Remove(findAcc);
        }

        private static Thread FindThread(Account acc)
        {
            var account = RunningBots.Keys.FirstOrDefault(c => c.Login == acc.Login && c.Password == acc.Password);
            if (account == null) return null;
            var thread = RunningBots.FirstOrDefault(k => k.Key == account).Value;
            return thread;
        }
    }
}