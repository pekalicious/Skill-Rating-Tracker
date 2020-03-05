using System;

namespace SR_Tracker.Models
{
    public class PlaySession
    {
        public DateTime Date { get; set; }
        public int GamesPlayed;
        public int OverallDiff;
        public int SkillRating { get; set; }
        public string Id { get; set; }
        public string Text { get; set; }
        public string Description { get; set; }
    }
}