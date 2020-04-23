using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace Pekalicious.SrTracker.Models
{
    [Table("User")]
    public class AppState
    {
        public const string LAST_USED_SEASON = "LAST_USED_SEASON";

        [PrimaryKey]
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
