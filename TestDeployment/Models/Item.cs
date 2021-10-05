using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestDeployment.Models
{
    public class Item
    {
        [Key]
        public int Id { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string Name { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string Requirements { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string Description { get; set; }

        [Column(TypeName = "int)")]
        public int Ammount { get; set; }

        [Column(TypeName = "bool)")]
        public bool IsEquiped { get; set; }
    }
}
