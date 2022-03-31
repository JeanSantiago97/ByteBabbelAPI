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
    public class MatriculaController : ControllerBase
    {
        BabbelContext context = new BabbelContext();
        private IMapper _mapper;

        public MatriculaController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpPost("Adicionar Matricula")]
        public IActionResult AdicionarMatricula(string alunoCpf, int numeroTurma)
        {

            //Validar se o aluno e turma existe
            Aluno _aluno = context.Alunos.FirstOrDefault(_aluno => _aluno.Cpf == alunoCpf);
            Turma _turma = context.Turmas.FirstOrDefault(_turma => _turma.Numero == numeroTurma);
            if (_aluno == null || _turma == null) return NotFound("Turma e/ou Aluno não existe");

            //Validar se o Aluno já está na turma
            Matricula _matricula = context.Matriculas.FirstOrDefault(_matricula => Equals(_matricula.Aluno.Cpf, _aluno.Cpf)
             && Equals(_matricula.Turma.Numero, _turma.Numero));
            if (_matricula != null) return BadRequest("O aluno não pode se matricular na mesma turma");

            //Verificar se a turma está cheia
            var turmaMat = context.Matriculas.Where(m => m.TurmaID == _turma.ID).ToList();
            if (turmaMat.Count >= 5) return BadRequest($"A turma{_turma.Numero} já está cheia.");

            //Cadastrar matricula
            Matricula mat = new Matricula();
            mat.Aluno = _aluno;
            mat.Turma = _turma;

            //Adicionar e salvar
            context.Matriculas.Add(mat);
            context.SaveChanges();
            return NoContent();
        }

        [HttpGet("Consultar Matricula")]
        public IEnumerable<ReadMatriculaDto> ConsultarMatriculas()
        {
            var matriculas = context.Matriculas;

            List<ReadMatriculaDto> matriculaDto = _mapper.Map<List<ReadMatriculaDto>>(matriculas);

            return matriculaDto.ToArray();
        }
        
        [HttpDelete("Excluir Matricula")]
        public IActionResult ExcluirMatricula(int id)
        {
            //Verifica se a matrícula existe.
            Matricula _matricula = context.Matriculas.FirstOrDefault(_matricula => _matricula.ID == id);
            if (_matricula == null) return NotFound("Matrícula não encontrada");

            //Remover o aluno da turma
            context.Matriculas.Remove(_matricula);


            context.SaveChanges();
            return NoContent();
        }
    }
}
