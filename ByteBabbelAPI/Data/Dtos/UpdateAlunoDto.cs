using System.ComponentModel.DataAnnotations;

namespace ByteBabbelAPI.Data.Dtos
{
    public class UpdateAlunoDto
    {
        [Required(ErrorMessage = "O campo CPF é obrigatório")]
        public string Cpf { get; set; }
        [Required(ErrorMessage = "O campo Nome é obrigatório")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "O campo Email é obrigatório")]
        public string Email { get; set; }
    }
}
