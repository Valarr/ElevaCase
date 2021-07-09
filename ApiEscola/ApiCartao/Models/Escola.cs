using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiEscola.Models
{
    public class Escola
    {
        public int escolaId { get; set; }

        [Required]
        [Column(TypeName = "varchar(100)")]
        public string nomeEscola { get; set; }

        [Required]
        [Column(TypeName = "varchar(50)")]
        public string enderecoEscola { get; set; }

        [Required]
        [Column(TypeName = "varchar(15)")]
        public string telefoneEscola { get; set; }
    }
}
