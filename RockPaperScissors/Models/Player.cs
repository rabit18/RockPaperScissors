namespace RockPaperScissors.Models
{
    public class Player : Base
    {
        /// <summary>
        /// Indicate if is human player
        /// </summary>
        public bool IsHuman { get; set; }

        /// <summary>
        /// Indicate if is random player
        /// </summary>
        public bool IsRandom { get; set; }

        /// <summary>
        /// Current play for the player
        /// </summary>
        public Item CurrentPlay { get; set; }

        /// <summary>
        /// Points of the player
        /// </summary>
        public int Points { get; set; }

        public void Reset()
        {
            CurrentPlay = null;
            Points = 0;
        }

        public override string ToString()
        {
            return string.Format("{0} ({1}) {2}", Id, IsHuman ? "Human" : "Computer", IsHuman ? "" : IsRandom ? "plays random" : "does not play random");
        }
    }
}
