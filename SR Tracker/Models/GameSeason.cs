using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace Pekalicious.SrTracker.Models
{
    [Table("GameSeason")]
    public class GameSeason
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; } = -1;
        public string Name { get; set; }
        public int HighestSkillRating { get; set; }
    }
}
