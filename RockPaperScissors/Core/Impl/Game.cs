using RockPaperScissors.Consts;
using RockPaperScissors.Enums;
using RockPaperScissors.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RockPaperScissors.Core.Impl
{
    /// <summary>
    /// Game implementation
    /// </summary>
    public class Game : IGame
    {
        /// <summary>
        /// Dictionary for menu options
        /// </summary>
        private readonly Dictionary<MenuEnum, string> MenuOptions = new Dictionary<MenuEnum, string>()
        {
            { MenuEnum.StartGame, "Start new game" },
            { MenuEnum.ListPlayers, "List players" },
            { MenuEnum.AddPlayer, "Add player" },
            { MenuEnum.RemovePlayer, "Remove player" },
            { MenuEnum.ListItems, "List items" },
            { MenuEnum.AddItem, "Add item" },
            { MenuEnum.RemoveItem, "Remove item" },
            { MenuEnum.ResetGame, "Reset config" },
            { MenuEnum.DefaultPlayers, "Set default players for test" },
            { MenuEnum.EndGame, "End game\n" }
        };

        /// <summary>
        /// Players list
        /// </summary>
        private List<Player> Players = new List<Player>();

        /// <summary>
        /// Item lists
        /// </summary>
        private List<Item> Items = new List<Item>();

        /// <summary>
        /// Indicate if game is over
        /// </summary>
        private bool EndGame;

        /// <summary>
        /// Roung number
        /// </summary>
        private int RoundNumber;

        public Game()
        {
            EndGame = false;
            RoundNumber = 0;
            LoadInitialConfig();
            ResetPlayers();
        }

        public void StartGame()
        {
            EndGame = false;
            RoundNumber = 0;
            ResetPlayers();
        }

        public void AddPlayer(Player player)
        {
            Players.Add(player);
        }

        public bool IsEndGame()
        {
            return EndGame;
        }

        public void RemovePlayer(string id)
        {
            Players.RemoveAll(i => i.Id.Equals(id));
        }

        public Dictionary<MenuEnum, string> GetMenuOptions()
        {
            return MenuOptions;
        }

        public List<Player> GetPlayers()
        {
            return Players;
        }

        public List<Item> GetItems()
        {
            return Items;
        }

        public void AddItem(Item item)
        {
            Items.Add(item);
        }

        public void RemoveItem(string id)
        {
            Items.RemoveAll(i => i.Id.Equals(id));

            foreach (var item in Items)
            {
                item.Beats.Remove(id);
            }
        }

        public void SetPlays(List<Player> humanPlays)
        {
            Random random = new Random();

            foreach (var player in Players)
            {
                if (player.IsHuman)
                {
                    player.CurrentPlay = humanPlays.First(p => p.Id.Equals(player.Id)).CurrentPlay;
                }
                else
                {
                    if (player.CurrentPlay == null || player.IsRandom)
                    {
                        player.CurrentPlay = Items[random.Next(Items.Count)];
                    }
                    else
                    {
                        List<Item> itemsCanBeatCurrent = Items.FindAll(x => x.Beats.Contains(player.CurrentPlay.Id));

                        player.CurrentPlay = itemsCanBeatCurrent.Count > 0 ? itemsCanBeatCurrent[random.Next(itemsCanBeatCurrent.Count)] : Items[random.Next(Items.Count)];
                    }
                }
            }

            RoundNumber++;
        }

        public int GetRoundNumber()
        {
            return RoundNumber;
        }

        public string GetRoundWinner()
        {
            List<string> winners = new List<string>();

            for (int i = 0; i < Players.Count; i++)
            {
                bool winSomeone = false;
                bool lostSomeone = false;

                for (int j = 0; j < Players.Count; j++)
                {
                    if (i != j)
                    {
                        if (Players[j].CurrentPlay.Beats.Contains(Players[i].CurrentPlay.Id))
                        {
                            lostSomeone = true;
                            break;
                        }

                        if (Players[i].CurrentPlay.Beats.Contains(Players[j].CurrentPlay.Id))
                        {
                            winSomeone = true;
                        }
                    }
                }

                if (winSomeone && !lostSomeone)
                {
                    Players[i].Points++;
                    winners.Add(Players[i].Id);
                }
            }

            return winners.Count.Equals(0) ? "TIE" : winners.Aggregate((x, y) => x + ", " + y);
        }

        public string ValidateEndGame()
        {
            if (Players.Exists(p => p.Points >= ConstantsGame.POINTS_TO_WIN))
            {
                EndGame = true;
                return Players.FindAll(p => p.Points >= ConstantsGame.POINTS_TO_WIN).Select(x => x.Id).Aggregate((x, y) => x + ", " + y);
            }

            return null;
        }

        private void ResetPlayers()
        {
            foreach (var player in Players)
            {
                player.Reset();
            }
        }

        /// <summary>
        /// Load initial configuration
        /// </summary>
        private void LoadInitialConfig()
        {
            Items.Add(new Item("Rock", new HashSet<string>(new string[] { "Scissors" })));
            Items.Add(new Item("Paper", new HashSet<string>(new string[] { "Rock" })));
            Items.Add(new Item("Scissors", new HashSet<string>(new string[] { "Paper" })));
        }

    }
}
