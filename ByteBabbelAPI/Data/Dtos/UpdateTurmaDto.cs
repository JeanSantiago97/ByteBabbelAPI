using System.ComponentModel.DataAnnotations;

namespace ByteBabbelAPI.Data.Dtos
{
    public class UpdateTurmaDto
    {
        [Required(ErrorMessage = "O campo Numero é obrigatório")]
        public int Numero { get; set; }
        [Required(ErrorMessage = "O campo Anoletivo é obrigatório")]
        public int Anoletivo { get; set; }
    }
}
