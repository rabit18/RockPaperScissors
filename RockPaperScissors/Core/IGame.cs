using RockPaperScissors.Enums;
using RockPaperScissors.Models;
using System.Collections.Generic;

namespace RockPaperScissors.Core
{
    /// <summary>
    /// Game Interface
    /// </summary>
    public interface IGame
    {
        /// <summary>
        /// Start the game
        /// </summary>
        public void StartGame();

        /// <summary>
        /// Indicates if the game is over
        /// </summary>
        /// <returns></returns>
        public bool IsEndGame();

        /// <summary>
        /// Add player to the game 
        /// </summary>
        /// <param name="player"></param>
        public void AddPlayer(Player player);

        /// <summary>
        /// Get menu options
        /// </summary>
        /// <returns></returns>
        public Dictionary<MenuEnum, string> GetMenuOptions();

        /// <summary>
        /// Get players
        /// </summary>
        /// <returns></returns>
        public List<Player> GetPlayers();

        /// <summary>
        /// Get items
        /// </summary>
        /// <returns></returns>
        public List<Item> GetItems();

        /// <summary>
        /// Add a item to the game
        /// </summary>
        /// <param name="item"></param>
        public void AddItem(Item item);

        /// <summary>
        /// Remove a item to the game
        /// </summary>
        /// <param name="id"></param>
        public void RemoveItem(string id);

        /// <summary>
        /// Remove a player to the game
        /// </summary>
        /// <param name="id"></param>
        public void RemovePlayer(string id);

        /// <summary>
        /// Set the plays for the players, every round
        /// </summary>
        /// <param name="humanPlays"></param>
        public void SetPlays(List<Player> humanPlays);

        /// <summary>
        /// Get row number
        /// </summary>
        /// <returns></returns>
        public int GetRoundNumber();

        /// <summary>
        /// Get round winners
        /// </summary>
        /// <returns>name players concatenated by comma</returns>
        public string GetRoundWinner();

        /// <summary>
        /// Validate if the game has winner
        /// </summary>
        /// <returns>name winners concatenated by comma, null if nobody has won</returns>
        public string ValidateEndGame();
    }
}
