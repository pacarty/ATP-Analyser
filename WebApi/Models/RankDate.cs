using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models
{
    public class RankDate
    {
        public int RankDateId { get; set; }
        public int RankingNumber { get; set; }
        public DateTime Date { get; set; }
        public string PlayerId { get; set; }
    }
}
