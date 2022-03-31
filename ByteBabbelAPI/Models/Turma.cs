using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ByteBabbelAPI.Models
{
    public class Turma
    {
        [Key]
        [Required]
        public int ID { get; private set; }
        [Required(ErrorMessage = "O campo Numero é obrigatório")]
        public int Numero { get; set; }
        [Required(ErrorMessage = "O campo Anoletivo é obrigatório")]
        public int Anoletivo { get; set; }

        public ICollection<Matricula> Matriculas { get; set; }

    }
}
