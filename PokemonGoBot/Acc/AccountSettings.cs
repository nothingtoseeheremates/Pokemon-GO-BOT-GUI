using System.Collections;
using System.Collections.Generic;
using PokemonGo.RocketAPI;
using PokemonGo.RocketAPI.Enums;
using PokemonGo.RocketAPI.GeneratedCode;

namespace PokemonGoBot.Acc
{
    public class AccountSettings : ISettings
    {
        public AuthType AuthType { get; } = AuthType.Ptc;
        public double DefaultLatitude { get; } = 52.379189;
        public double DefaultLongitude { get; } = 4.899431;
        public double DefaultAltitude { get; } = 10;
        public string GoogleRefreshToken { get; set; }
        public string PtcPassword { get; }
        public string PtcUsername { get; }
        public float KeepMinIVPercentage { get; }
        public int KeepMinCP { get; }
        public double WalkingSpeedInKilometerPerHour { get; }
        public bool EvolveAllPokemonWithEnoughCandy { get; }
        public bool TransferDuplicatePokemon { get; }
        public int DelayBetweenPokemonCatch { get; }
        public bool UsePokemonToNotCatchFilter { get; }
        public int KeepMinDuplicatePokemon { get; }
        public ICollection<KeyValuePair<ItemId, int>> ItemRecycleFilter { get; }
        public ICollection<PokemonId> PokemonsToEvolve { get; }
        public ICollection<PokemonId> PokemonsNotToTransfer { get; }
        public ICollection<PokemonId> PokemonsNotToCatch { get; }
    }
}