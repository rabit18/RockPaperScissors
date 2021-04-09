using RockPaperScissors.Consts;
using RockPaperScissors.Enums;
using RockPaperScissors.Models;
using System;
using System.Collections.Generic;

namespace RockPaperScissors.UI
{
    static class BoardUI
    {
        /// <summary>
        /// Print welcome message
        /// </summary>
        public static void PrintWelcomeMessage()
        {
            Console.Clear();
            string text = ConstantsGame.WELCOME_MESSAGE;
            PrintTitleStart(text);
            Console.WriteLine(ConstantsGame.COPY);
            PrintTitleEnd(text.Length);
        }

        /// <summary>
        /// Print end progam message
        /// </summary>
        public static void PrintByeByeMessage()
        {
            Console.Clear();
            string text = ConstantsGame.BYE_MESSAGE;
            PrintTitleStart(text);
            PrintTitleEnd(text.Length);
            Console.ReadLine();
        }

        /// <summary>
        /// Read menu input selected
        /// </summary>
        /// <param name="menuOptions"></param>
        /// <returns></returns>
        public static MenuEnum PrintAndReadMenuOptions(Dictionary<MenuEnum, string> menuOptions)
        {
            int? option = null;

            while (!option.HasValue)
            {
                Console.WriteLine(ConstantsGame.ENTER_OPTION_NUMBER_MESSAGE);

                foreach (var entry in menuOptions)
                {
                    Console.WriteLine(string.Format("\t{0}. {1}", (int)entry.Key, entry.Value));
                }

                try
                {
                    option = int.Parse(Console.ReadLine());
                    if (!IsValidMenuOption(menuOptions, option.Value)) throw new Exception();
                }
                catch
                {
                    option = null;
                    PrintInvalidOption();
                }
            }

            Console.Clear();

            return (MenuEnum)option.Value;
        }

        /// <summary>
        /// Print players
        /// </summary>
        /// <param name="players"></param>
        public static void PrintPlayers(List<Player> players)
        {
            Console.Clear();
            PrintTitleStart(ConstantsGame.PLAYERS_TITLE);

            foreach (var player in players)
            {
                Console.WriteLine(player);
            }

            Console.ReadLine();
            Console.Clear();
        }

        /// <summary>
        /// Print items
        /// </summary>
        /// <param name="items"></param>
        public static void PrintItems(List<Item> items)
        {
            Console.Clear();
            PrintTitleStart(ConstantsGame.ITEMS_TITLE);

            foreach (var item in items)
            {
                Console.WriteLine(item);
            }

            Console.ReadLine();
            Console.Clear();
        }

        /// <summary>
        /// Read input new player
        /// </summary>
        /// <param name="players"></param>
        /// <returns></returns>
        public static Player ReadPlayer(List<Player> players)
        {
            Player player = new Player();
            int? option = null;

            Console.Clear();
            PrintTitleStart(ConstantsGame.NEW_PLAYER_TITLE);

            Console.WriteLine(ConstantsGame.ENTER_PLAYER_NAME_MESSAGE);
            while (player.Id == null)
            {
                player.Id = Console.ReadLine();

                if (players.Exists(p => p.Id.Equals(player.Id)))
                {
                    player.Id = null;
                    Console.WriteLine(ConstantsGame.PLAYER_ALREADY_EXISTS_MESSAGE);
                }
            }

            Console.WriteLine(ConstantsGame.ENTER_PLAYER_TYPE_MESSAGE);
            while (!option.HasValue)
            {
                try
                {
                    option = int.Parse(Console.ReadLine());
                    if (!(option.Value.Equals(ConstantsGame.PLAYER_HUMAN) || option.Value.Equals(ConstantsGame.PLAYER_COMPUTER))) throw new Exception();
                }
                catch
                {
                    option = null;
                    PrintInvalidOption();
                }
            }

            player.IsHuman = option.Value.Equals(ConstantsGame.PLAYER_HUMAN);
            player.IsRandom = false;

            option = null;

            if (!player.IsHuman)
            {
                Console.WriteLine(ConstantsGame.ENTER_PLAYER_RANDOM_MESSAGE);
                while (!option.HasValue)
                {
                    try
                    {
                        option = int.Parse(Console.ReadLine());
                        if (!(option.Value.Equals(ConstantsGame.PLAYER_RANDOM_YES) || option.Value.Equals(ConstantsGame.PLAYER_RANDOM_NO))) throw new Exception();
                    }
                    catch
                    {
                        option = null;
                        PrintInvalidOption();
                    }
                }
            }

            player.IsRandom = option.HasValue && option.Value.Equals(ConstantsGame.PLAYER_RANDOM_YES);

            Console.Clear();
            return player;
        }

        /// <summary>
        /// Read input new item
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        public static Item ReadItem(List<Item> items)
        {
            Item item = new Item();
            item.Beats = new HashSet<string>();

            string itemsBeat = null;

            Console.Clear();
            PrintTitleStart(ConstantsGame.NEW_ITEM_TITLE);

            Console.WriteLine(ConstantsGame.ENTER_ITEM_NAME_MESSAGE);
            while (item.Id == null)
            {
                item.Id = Console.ReadLine();

                if (items.Exists(i => i.Id.Equals(item.Id)))
                {
                    item.Id = null;
                    Console.WriteLine(ConstantsGame.ITEM_ALREADY_EXISTS_MESSAGE);
                }
            }

            Console.WriteLine();
            for (int i = 0; i < items.Count; i++)
            {
                Console.WriteLine(string.Format("{0}) {1}", i + 1, items[i]));
            }

            Console.WriteLine(ConstantsGame.ENTER_BEATS_NUMBERS_MESSAGE);
            while (itemsBeat == null)
            {
                itemsBeat = Console.ReadLine();
                string[] stringSplit = itemsBeat.Trim().Split(',');

                try
                {
                    foreach (var s in stringSplit)
                    {
                        int position = int.Parse(s);
                        item.Beats.Add(items[position - 1].Id);
                    }
                }
                catch
                {
                    itemsBeat = null;
                    item.Beats = new HashSet<string>();
                    PrintInvalidOption();
                }
            }

            Console.Clear();

            return item;
        }

        /// <summary>
        /// Read input for remove item
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        public static string ReadRemoveItem(List<Item> items)
        {
            Console.Clear();
            PrintTitleStart(ConstantsGame.REMOVE_ITEM_TITLE);

            for (int i = 0; i < items.Count; i++)
            {
                Console.WriteLine(string.Format("{0}) {1}", i + 1, items[i]));
            }

            Console.WriteLine(ConstantsGame.ENTER_ITEM_NUMBER_MESSAGE);

            try
            {
                int option = int.Parse(Console.ReadLine());
                return items[option - 1].Id;
            }
            catch
            {
                PrintInvalidOption();
                Console.ReadLine();
                return null;
            }
            finally
            {
                Console.Clear();
            }
        }

        /// <summary>
        /// Read input for remove player
        /// </summary>
        /// <param name="players"></param>
        /// <returns></returns>
        public static string ReadRemovePlayer(List<Player> players)
        {
            Console.Clear();
            PrintTitleStart(ConstantsGame.REMOVE_PLAYER_TITLE);

            for (int i = 0; i < players.Count; i++)
            {
                Console.WriteLine(string.Format("{0}) {1}", i + 1, players[i]));
            }

            Console.WriteLine(ConstantsGame.ENTER_PLAYER_NUMBER_MESSAGE);

            try
            {
                int option = int.Parse(Console.ReadLine());
                return players[option - 1].Id;
            }
            catch
            {
                PrintInvalidOption();
                Console.ReadLine();
                return null;
            }
            finally
            {
                Console.Clear();
            }
        }

        /// <summary>
        /// Read every human plays in a round
        /// </summary>
        /// <param name="players"></param>
        /// <param name="items"></param>
        /// <returns>playes with plays in CurrentPlay onject</returns>
        public static List<Player> ReadHumanPlays(List<Player> players, List<Item> items)
        {
            for (int i = 0; i < items.Count; i++)
            {
                Console.WriteLine(string.Format("{0}) {1}", i + 1, items[i]));
            }

            foreach (var player in players)
            {
                if (player.IsHuman)
                {
                    int? option = null;

                    Console.WriteLine(string.Format(ConstantsGame.ENTER_PLAY_NUMBER_MESSAGE, player.Id));

                    while (!option.HasValue)
                    {
                        try
                        {
                            option = int.Parse(Console.ReadLine());
                            player.CurrentPlay = items[option.Value - 1];
                        }
                        catch
                        {
                            option = null;
                            PrintInvalidOption();
                        }
                    }
                }
            }

            return players;
        }

        /// <summary>
        /// Print plays in a round and the winner
        /// </summary>
        /// <param name="players"></param>
        /// <param name="roundNumber"></param>
        /// <param name="winner"></param>
        public static void PrintPlays(List<Player> players, int roundNumber, string winner)
        {
            PrintTitleStart(string.Format(ConstantsGame.ROUND_TITLE, roundNumber));

            foreach (var player in players)
            {
                Console.WriteLine(string.Format(ConstantsGame.ROUND_PLAYER_PLAY_MESSAGE, player.Id, player.CurrentPlay.Id));
            }

            Console.WriteLine(string.Format(ConstantsGame.ROUND_WINNER_MESSAGE, winner));

            PrintPressAKey();
            Console.Clear();
        }

        /// <summary>
        /// Print score board
        /// </summary>
        /// <param name="players"></param>
        /// <param name="roundNumber"></param>
        public static void PrintScore(List<Player> players, int roundNumber)
        {
            string title = string.Format(ConstantsGame.SCORE_TITLE, roundNumber);
            PrintTitleStart(title);

            foreach (var player in players)
            {
                Console.WriteLine(string.Format(ConstantsGame.SCORE_PLAYER_MESSAGE, player.Id, player.Points));
            }

            PrintTitleEnd(title.Length);
        }

        /// <summary>
        /// Print winner of the game
        /// </summary>
        /// <param name="winner"></param>
        public static void PrintWinner(string winner)
        {
            Console.Clear();
            string title = ConstantsGame.WINNER_TITLE;
            PrintTitleStart(title);

            Console.WriteLine(string.Format("\t{0}", winner));

            PrintTitleEnd(title.Length);
            PrintPressAKey();
        }

        /// <summary>
        /// Print titles
        /// </summary>
        /// <param name="title"></param>
        private static void PrintTitleStart(string title)
        {
            Console.WriteLine(string.Format(ConstantsGame.TITLE_FORMAT, title));
        }

        /// <summary>
        /// Print end line titles
        /// </summary>
        /// <param name="length"></param>
        private static void PrintTitleEnd(int length)
        {
            Console.WriteLine("\n" + new string(ConstantsGame.TITLE_FORMAT_CHAT, length + ConstantsGame.TITLE_END_LENGTH) + "\n");
        }

        /// <summary>
        /// Print a press key message
        /// </summary>
        private static void PrintPressAKey()
        {
            Console.WriteLine(ConstantsGame.PRESS_KEY_MESSAGE);
            Console.ReadLine();
        }

        /// <summary>
        /// Print a invalid option message
        /// </summary>
        private static void PrintInvalidOption()
        {
            Console.WriteLine(ConstantsGame.INVALID_OPTION_MESSAGE);
        }

        /// <summary>
        /// Validate if the menu option input is valid
        /// </summary>
        /// <param name="menuOptions"></param>
        /// <param name="option">option input</param>
        /// <returns></returns>
        private static bool IsValidMenuOption(Dictionary<MenuEnum, string> menuOptions, int option)
        {
            return menuOptions.ContainsKey((MenuEnum)option);
        }
    }
}
