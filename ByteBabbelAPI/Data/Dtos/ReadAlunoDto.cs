using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ByteBabbelAPI.Data.Dtos
{

    public class ReadAlunoDto
    {
        public int Id { get; private set; }
        [Required(ErrorMessage = "O campo CPF é obrigatório")]
        public string Cpf { get; set; }
        [Required(ErrorMessage = "O campo Nome é obrigatório")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "O campo Email é obrigatório")]
        public string Email { get; set; }

    }
}
