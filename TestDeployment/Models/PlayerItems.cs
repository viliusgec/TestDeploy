using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestDeployment.Models
{
    [Table("PlayerItems", Schema = "Clicker")]
    public class PlayerItems
    {
        [Key]
        public int Id { get; set; }
        public int ItemId { get; set; }
        public string PlayerName { get; set; }
    }
}
