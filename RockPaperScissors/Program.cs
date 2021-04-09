using RockPaperScissors.Core;
using RockPaperScissors.Core.Impl;
using RockPaperScissors.Enums;
using RockPaperScissors.Models;
using RockPaperScissors.UI;

namespace RockPaperScissors
{
    class Program
    {
        /// <summary>
        /// Core game instance
        /// </summary>
        private static IGame Game = new Game();

        /// <summary>
        /// Started program method
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            BoardUI.PrintWelcomeMessage();

            MenuEnum option;

            do
            {
                option = BoardUI.PrintAndReadMenuOptions(Game.GetMenuOptions());

                switch (option)
                {
                    case MenuEnum.StartGame:
                        StartGame();
                        BoardUI.PrintWelcomeMessage();
                        break;
                    case MenuEnum.ListPlayers:
                        BoardUI.PrintPlayers(Game.GetPlayers());
                        break;
                    case MenuEnum.AddPlayer:
                        Game.AddPlayer(BoardUI.ReadPlayer(Game.GetPlayers()));
                        break;
                    case MenuEnum.ListItems:
                        BoardUI.PrintItems(Game.GetItems());
                        break;
                    case MenuEnum.AddItem:
                        Game.AddItem(BoardUI.ReadItem(Game.GetItems()));
                        break;
                    case MenuEnum.RemoveItem:
                        Game.RemoveItem(BoardUI.ReadRemoveItem(Game.GetItems()));
                        break;
                    case MenuEnum.RemovePlayer:
                        Game.RemovePlayer(BoardUI.ReadRemovePlayer(Game.GetPlayers()));
                        break;
                    case MenuEnum.DefaultPlayers:
                        SetTestPlayers();
                        break;
                    case MenuEnum.ResetGame:
                        Game = new Game();
                        break;
                    default:
                        break;
                }

            } while (!option.Equals(MenuEnum.EndGame));

            BoardUI.PrintByeByeMessage();
        }

        /// <summary>
        /// Ingame logic
        /// </summary>
        private static void StartGame()
        {
            if (Game.GetPlayers().Count.Equals(0) || Game.GetItems().Count.Equals(0))
            {
                BoardUI.PrintNoData();
                return;
            }

            Game.StartGame();
            do
            {
                // 1. SCORE
                BoardUI.PrintScore(Game.GetPlayers(), Game.GetRoundNumber());

                // 2. PLAY
                Game.SetPlays(BoardUI.ReadHumanPlays(Game.GetPlayers(), Game.GetItems()));

                // 3. PRINT
                BoardUI.PrintPlays(Game.GetPlayers(), Game.GetRoundNumber(), Game.GetRoundWinner());

                // 4. VALIDATE
                string winners = Game.ValidateEndGame();
                if (winners != null)
                {
                    BoardUI.PrintWinner(winners);
                }
            } while (!Game.IsEndGame());
        }

        /// <summary>
        /// Set test players for fast test
        /// </summary>
        private static void SetTestPlayers()
        {
            if (Game.GetPlayers().Count.Equals(0))
            {
                Game.AddPlayer(new Player() { Id = "Gest", IsHuman = true, IsRandom = false });
                Game.AddPlayer(new Player() { Id = "Siri", IsHuman = false, IsRandom = true });
            }
        }
    }
}
