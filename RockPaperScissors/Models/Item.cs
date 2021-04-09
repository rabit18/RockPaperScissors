using System.Collections.Generic;
using System.Linq;

namespace RockPaperScissors.Models
{
    /// <summary>
    /// Item class for game item options ej: (paper, scissors, rock)
    /// </summary>
    public class Item : Base
    {
        public Item() { }

        public Item(string id, HashSet<string> beats)
        {
            Id = id;
            Beats = beats;
        }

        /// <summary>
        /// Items that can be beated
        /// </summary>
        public HashSet<string> Beats { get; set; }

        public override string ToString()
        {
            return string.Format("{0} \t\t beats -> {1}", Id.ToUpper(),
                Beats.Count > 0 ? Beats.Aggregate((i, j) => i.ToLower() + ", " + j.ToLower()) : "");
        }
    }
}
