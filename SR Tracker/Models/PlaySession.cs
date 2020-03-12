using System;
using System.Data;
using SQLite;

namespace Pekalicious.SrTracker.Models
{
    [Table("PlaySession")]
    public class PlaySession
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int GamesPlayed { get; set; }
        public int OverallDiff { get; set; }
        public int FinalSkillRating { get; set; }
        public int GameSeasonId { get; set; }
    }
}