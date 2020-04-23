using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace Pekalicious.SrTracker.Models
{
    [Table("GameSeason")]
    public class GameSeason
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; } = -1;
        public string Name { get; set; }
        public int HighestSkillRating { get; set; }
        public int LastSkillRating { get; set; }
        [OneToMany]
        public List<PlaySession> SessionHistory { get; set; }

        public GameSeason()
        {
            SessionHistory = new List<PlaySession>();
        }
    }
}
