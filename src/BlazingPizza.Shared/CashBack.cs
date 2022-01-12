using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BlazingPizza
{
    public class CashBack
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string UserId { get; set; }
        public int OrderId { get; set; }
        public decimal Value { get; set; }
        public List<Order> Pedidos { get; set; } = new List<Order>();
        public string GetFormattedTotalValue() => Value.ToString("0.00");
    }
}
