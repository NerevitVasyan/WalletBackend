using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SpendingsWebAPI.Entities
{
    public class Spending
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        [Column(TypeName ="Money")]
        public decimal Value { get; set; }

        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }

        [Required]
        public string UserId { get; set; }
        public virtual User User { get; set; }

        public virtual ICollection<SpendingTag> Tags { get; set; }
    }
}
