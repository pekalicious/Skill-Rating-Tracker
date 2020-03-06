using System;
using SQLite;

namespace Pekalicious.SrTracker.Models
{
    public class PlaySession
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int GamesPlayed { get; set; }
        public int OverallDiff { get; set; }
        public int FinalSkillRating { get; set; }
    }
}