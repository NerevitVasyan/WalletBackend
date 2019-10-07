using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpendingsWebAPI.Entities
{
    public class SpendingTag
    {
        public int SpendingId { get; set; }
        public virtual Spending Spending { get; set; }

        public int TagId { get; set; }
        public virtual Tag Tag { get; set; }
    }
}
