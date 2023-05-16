using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace SalesPageAPI.Models
{
    public class SalesListModel
    {
        [Key]
        public int Id { get; set; }

        public int Type { get; set; }

        public string ISO8601Date { get; set; }

        public string Product { get; set; }

        public double Value { get; set; }

        public string Seller { get; set; }
    }
}
