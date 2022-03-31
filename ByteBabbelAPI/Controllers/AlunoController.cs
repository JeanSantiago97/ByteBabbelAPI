using AutoMapper;
using ByteBabbelAPI.Data;
using ByteBabbelAPI.Data.Dtos;
using ByteBabbelAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ByteBabbelAPI.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class AlunoController : ControllerBase
    {
        BabbelContext context = new BabbelContext();
        private IMapper _mapper;

        public AlunoController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpPost("Adicionar Aluno")]
        public IActionResult AdicionarAluno([FromBody] CreateAlunoDto alunoDto, int NumeroTurma) // Cadastrar Aluno e dizer a turma - RN02
        {
            //Mapeamento do alunoDTO para o tipo Aluno
            Aluno aluno = _mapper.Map<Aluno>(alunoDto);

            //Verifica se o aluno já está cadastrado (CPF).
            Aluno _aluno = context.Alunos.FirstOrDefault(_aluno => _aluno.Cpf == aluno.Cpf);
            if (_aluno != null) return BadRequest($"O CPF {_aluno.Cpf} já está cadastrado");

            //Verifica se a turma existe.
            Turma _turma = context.Turmas.FirstOrDefault(_turma => _turma.Numero == NumeroTurma);
            if (_turma == null) return BadRequest(($"A Turma {NumeroTurma} não existe"));

            //Verifica se a turma já está cheia
            var turmaMat = context.Matriculas.Where(m => m.TurmaID == _turma.ID).ToList(); //Recebe toda as matriculas do aluno específico
            Console.WriteLine("Count: 5");
            if (turmaMat.Count >= 5) return BadRequest($"A turma {_turma.Numero} já está cheia.");

            //Cadastrar matricula
            Matricula matricula = new Matricula();
            matricula.Aluno = aluno;
            matricula.Turma = _turma;
            matricula.AlunoID = aluno.ID;
            matricula.TurmaID = _turma.ID;

            //Adicionar e salvar
            context.Alunos.Add(aluno);
            context.Matriculas.Add(matricula);
            context.SaveChanges();
            return CreatedAtAction(nameof(ConsultarAlunos), new { ID = aluno.ID }, aluno);
        }

        [HttpGet("Consultar Alunos")]
        public IEnumerable<ReadAlunoDto> ConsultarAlunos()
        {
            var alunos = context.Alunos;
            List<ReadAlunoDto> alunoDto = _mapper.Map<List<ReadAlunoDto>>(alunos);

            return alunoDto.ToArray();
        }

        [HttpPut("Alterar Aluno")]
        public IActionResult AlterarAluno(string cpf, [FromBody] UpdateAlunoDto novoAluno)
        {

            //Não deve ser igual o CPF de outro aluno.
            Aluno _aluno = context.Alunos.FirstOrDefault(_aluno => _aluno.Cpf == novoAluno.Cpf);
            if (_aluno != null) return BadRequest($"Já existe um aluno com o CPF:{_aluno.Cpf}.");

            //Verifica se o aluno existe.
            _aluno = context.Alunos.FirstOrDefault(_aluno2 => _aluno2.Cpf == cpf);
            if (_aluno == null) return BadRequest($"O aluno CPF:{cpf} não foi encontrado.");


            //Mudar o CPF do aluno deve alterar na classe matricula.
            var alunoMat = context.Matriculas.Where(m => m.Aluno.ID == _aluno.ID).ToList();
            foreach (var mat in alunoMat)
            {
                Matricula _matricula = context.Matriculas.FirstOrDefault(_matricula => _matricula.Aluno.ID == _aluno.ID);
                if (_matricula != null) _matricula.Aluno.Cpf = novoAluno.Cpf;

                context.SaveChanges();
            }

            _mapper.Map(novoAluno, _aluno);
            context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("Excluir Aluno")]
        public IActionResult ExcluirAluno(string cpf)
        {
            Aluno _aluno = context.Alunos.FirstOrDefault(_aluno => _aluno.Cpf == cpf);
            if (_aluno == null) return NotFound();

            //Remover o aluno das turmas e da Matricula
            var alunoMat = context.Matriculas.Where(m => m.Aluno.ID == _aluno.ID).ToList();
            foreach (var mat in alunoMat) context.Matriculas.Remove(mat);

            context.Alunos.Remove(_aluno);
            context.SaveChanges();
            return NoContent();
        }

    }
}
