using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestDeployment.Models
{
    [Table("Skills", Schema = "Clicker")]
    public class Skill
    {
        [Key]
        public int Id { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string SkillName { get; set; }

        [Column(TypeName = "int)")]
        public int Experience { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string PlayerName { get; set; }
    }
}
