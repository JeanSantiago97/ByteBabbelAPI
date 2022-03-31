using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ByteBabbelAPI.Models
{
    public class Matricula
    {
        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [ForeignKey("AlunoID")]
        public int AlunoID { get; set; }

        [ForeignKey("TurmaID")]
        public int TurmaID { get; set; }

        [Required]
        public Aluno Aluno { get ; set; }
        [Required]
        public Turma Turma { get; set; }
    }
    
}
