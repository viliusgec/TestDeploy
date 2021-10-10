using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestDeployment.Models
{
    [Table("Players", Schema = "Clicker")]
    public class Player
    {
        [Key]
        public string Username { get; set; }

        [Column(TypeName = "int")]
        public int InventorySpace { get; set; }

        [Column(TypeName = "int")]
        public int Money { get; set; }

        [Column(TypeName = "bool")]
        public bool BattleState { get; set; }
    }
}
