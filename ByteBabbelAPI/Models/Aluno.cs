using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ByteBabbelAPI.Models
{
    public class Aluno
    {
        [Key]
        [Required]
        public int ID { get; set; }
        [Required(ErrorMessage = "O campo CPF é obrigatório")]
        public string Cpf { get; set; }
        [Required(ErrorMessage = "O campo Nome é obrigatório")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "O campo Email é obrigatório")]
        public string Email { get; set; }

        public ICollection<Matricula> Matriculas { get; set; }
    }
}
