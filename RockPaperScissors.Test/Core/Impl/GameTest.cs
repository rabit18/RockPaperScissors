using NUnit.Framework;
using RockPaperScissors.Core.Impl;
using RockPaperScissors.Models;
using System.Collections.Generic;
using System.Linq;

namespace RockPaperScissors.Test.Core.Impl
{
    /// <summary>
    /// Tests for Game Class
    /// </summary>
    public class GameTest
    {
        private static Game Game;

        [SetUp]
        public void Setup()
        {
            Game = new Game();
            Game.AddPlayer(new Player() { Id = "Diego", IsHuman = true });
            Game.AddPlayer(new Player() { Id = "Laura", IsHuman = true });
        }

        [Test]
        public void TestInitialValues()
        {
            Assert.AreEqual(false, Game.IsEndGame());
            Assert.AreEqual(0, Game.GetRoundNumber());
            Assert.AreEqual(0, Game.GetRoundNumber());
            Assert.AreEqual(3, Game.GetItems().Count);
        }

        [Test]
        public void TestPlayer()
        {
            foreach (var player in Game.GetPlayers())
            {
                Assert.AreEqual(null, player.CurrentPlay);
                Assert.AreEqual(0, player.Points);
            }
        }

        [Test]
        public void TestAddPlayer()
        {
            Game.AddPlayer(new Player() { Id = "Leo", IsHuman = true });
            Assert.AreEqual(3, Game.GetPlayers().Count);
        }

        [Test]
        public void TestRemovePlayer()
        {
            Game.RemovePlayer("Diego");
            Assert.AreEqual(1, Game.GetPlayers().Count);
        }

        [Test]
        public void TestItems()
        {
            int currentItems = Game.GetItems().Count;
            Game.AddItem(new Item() { Id = "myItem" });
            Assert.AreEqual(currentItems + 1, Game.GetItems().Count);

            Game.RemoveItem("myItem");
            Assert.AreEqual(currentItems, Game.GetItems().Count);
        }

        [Test]
        public void TestSetPlays_RoundWinner_RoundNumber()
        {
            List<Player> players = Game.GetPlayers();
            players[0].CurrentPlay = Game.GetItems().First(x => x.Id.Equals("Scissors"));
            players[1].CurrentPlay = Game.GetItems().First(x => x.Id.Equals("Paper"));

            Game.SetPlays(players);
            string winner = Game.GetRoundWinner();

            Assert.AreEqual(1, Game.GetRoundNumber());
            Assert.AreEqual("Diego", winner);
            Assert.AreEqual(1, Game.GetPlayers()[0].Points);
            Assert.AreEqual(1, Game.GetPlayers()[0].Points);
            Assert.AreEqual(0, Game.GetPlayers()[1].Points);
        }

        [Test]
        public void TestValidateEndGame()
        {
            List<Player> players = Game.GetPlayers();
            players[1].CurrentPlay = Game.GetItems().First(x => x.Id.Equals("Scissors"));
            players[0].CurrentPlay = Game.GetItems().First(x => x.Id.Equals("Paper"));

            Game.SetPlays(players);
            Game.GetRoundWinner();
            Game.SetPlays(players);
            Game.GetRoundWinner();
            Game.SetPlays(players);
            Game.GetRoundWinner();

            string winner = Game.ValidateEndGame();

            Assert.AreEqual("Laura", winner);
            Assert.IsTrue(Game.IsEndGame());
        }

        [Test]
        public void TestThreePlayersGame()
        {
            List<Player> players = Game.GetPlayers();
            Game.AddPlayer(new Player() { Id = "Jose", IsHuman = true });

            players[0].CurrentPlay = Game.GetItems().First(x => x.Id.Equals("Scissors"));
            players[1].CurrentPlay = Game.GetItems().First(x => x.Id.Equals("Paper"));
            players[2].CurrentPlay = Game.GetItems().First(x => x.Id.Equals("Rock"));

            Game.SetPlays(players);
            string winner = Game.GetRoundWinner();
            players = Game.GetPlayers();
            Game.ValidateEndGame();

            Assert.AreEqual("TIE", winner);
            Assert.AreEqual(0, players[0].Points);
            Assert.AreEqual(0, players[1].Points);
            Assert.AreEqual(0, players[2].Points);

            players[0].CurrentPlay = Game.GetItems().First(x => x.Id.Equals("Scissors"));
            players[1].CurrentPlay = Game.GetItems().First(x => x.Id.Equals("Scissors"));
            players[2].CurrentPlay = Game.GetItems().First(x => x.Id.Equals("Rock"));

            Game.SetPlays(players);
            winner = Game.GetRoundWinner();
            players = Game.GetPlayers();
            Game.ValidateEndGame();

            Assert.AreEqual("Jose", winner);
            Assert.AreEqual(0, players[0].Points);
            Assert.AreEqual(0, players[1].Points);
            Assert.AreEqual(1, players[2].Points);

            players[0].CurrentPlay = Game.GetItems().First(x => x.Id.Equals("Scissors"));
            players[1].CurrentPlay = Game.GetItems().First(x => x.Id.Equals("Scissors"));
            players[2].CurrentPlay = Game.GetItems().First(x => x.Id.Equals("Paper"));

            Game.SetPlays(players);
            winner = Game.GetRoundWinner();
            players = Game.GetPlayers();
            Game.ValidateEndGame();

            Assert.IsTrue(winner.Contains(","));
            Assert.AreEqual(1, players[0].Points);
            Assert.AreEqual(1, players[1].Points);
            Assert.AreEqual(1, players[2].Points);

            players[0].CurrentPlay = Game.GetItems().First(x => x.Id.Equals("Rock"));
            players[1].CurrentPlay = Game.GetItems().First(x => x.Id.Equals("Rock"));
            players[2].CurrentPlay = Game.GetItems().First(x => x.Id.Equals("Rock"));

            Game.SetPlays(players);
            winner = Game.GetRoundWinner();
            players = Game.GetPlayers();
            Game.ValidateEndGame();

            Assert.AreEqual("TIE", winner);
            Assert.AreEqual(1, players[0].Points);
            Assert.AreEqual(1, players[1].Points);
            Assert.AreEqual(1, players[2].Points);

            players[0].CurrentPlay = Game.GetItems().First(x => x.Id.Equals("Scissors"));
            players[1].CurrentPlay = Game.GetItems().First(x => x.Id.Equals("Scissors"));
            players[2].CurrentPlay = Game.GetItems().First(x => x.Id.Equals("Rock"));

            Game.SetPlays(players);
            winner = Game.GetRoundWinner();
            players = Game.GetPlayers();
            Game.ValidateEndGame();

            Assert.AreEqual("Jose", winner);
            Assert.AreEqual(1, players[0].Points);
            Assert.AreEqual(1, players[1].Points);
            Assert.AreEqual(2, players[2].Points);
            Assert.IsFalse(Game.IsEndGame());

            Game.SetPlays(players);
            winner = Game.GetRoundWinner();
            players = Game.GetPlayers();
            Game.ValidateEndGame();

            Assert.AreEqual("Jose", winner);
            Assert.AreEqual(1, players[0].Points);
            Assert.AreEqual(1, players[1].Points);
            Assert.AreEqual(3, players[2].Points);
            Assert.IsTrue(Game.IsEndGame());
        }
    }
}