using System;
using System.Collections.Generic;
using System.Data;
using SQLite;
using SQLiteNetExtensions.Attributes;

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
        [TextBlob("matchHistoryBlobbed")]
        public List<int> MatchHistory { get; set; }
        public string matchHistoryBlobbed { get; set; }

        public PlaySession()
        {
            MatchHistory = new List<int>();
        }
    }
}