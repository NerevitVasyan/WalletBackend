using System;
using System.Collections.Generic;

namespace DTOLayer
{
    public class SpendingDto
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public decimal Value { get; set; }
        public string Category { get; set; }
        public string UserId { get; set; }
        public List<string> Tags { get; set; }
    }
}
