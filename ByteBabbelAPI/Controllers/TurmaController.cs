using AutoMapper;
using ByteBabbelAPI;
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
    public class TurmaController : ControllerBase
    {

        BabbelContext context = new BabbelContext();
        private IMapper _mapper;

        public TurmaController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpPost("Adicionar Turma")]
        public IActionResult AdicionarTurma([FromBody] CreateTurmaDto turmaDto)
        {

            Turma turma = _mapper.Map<Turma>(turmaDto);

            //Verificar se a turma existe.
            Turma _turma = context.Turmas.FirstOrDefault(_turma => _turma.Numero == turma.Numero);
            if (_turma != null) return BadRequest($"A Turma {turma.Numero} já existe");


            context.Turmas.Add(turma);
            context.SaveChanges();
            return NoContent();
        }

        [HttpPut(("Alterar Turma"))]
        public IActionResult AlterarTurma(int numero, [FromBody] UpdateTurmaDto novaTurma)
        {


            //Não pode mudar para o nome de uma turma já existente.
            Turma _turma = context.Turmas.FirstOrDefault(_turma => _turma.Numero == novaTurma.Numero);
            if (_turma != null) return NotFound($"Já existe uma turma com o numero {novaTurma.Numero}!");

            //Verificar se a turma existe.
             _turma = context.Turmas.FirstOrDefault(_turma => _turma.Numero == numero);
            if(_turma == null) return NotFound($"Turma {numero} não encontrada!");

            //Mudar o Numero da turma deve alterar na classe matricula.
            var turmaMat = context.Matriculas.Where(m => m.Turma.ID == _turma.ID).ToList();
            foreach (var mat in turmaMat)
            {
                Matricula _matricula = context.Matriculas.FirstOrDefault(_matricula => _matricula.Turma.ID == _turma.ID);
                if (_matricula != null) _matricula.Turma.Numero = novaTurma.Numero; //VERIFICAR -> _matricula.AlunoID.ID = novoAluno.ID

                context.SaveChanges();
            }

            _mapper.Map(novaTurma, _turma);

            context.SaveChanges();
            return NoContent();
        }

        [HttpGet("Consultar Turmas")]
        public IEnumerable<ReadTurmaDto> ConsultarTurmas()
        {
            var turmas = context.Turmas;

            List<ReadTurmaDto> turmaDto = _mapper.Map<List<ReadTurmaDto>>(turmas);

            foreach (var item in turmaDto)
            {
                var turmaMat = context.Matriculas.Where(m => m.Turma.ID == item.Id).ToList();
                item.AlunosCadastrados = turmaMat.Count;

            }

            return turmaDto.ToArray();
        }

        [HttpDelete("Excluir Turma")]
        public IActionResult ExcluirTurma(int numero)
        {
            //Validação se a turma existe.
            Turma _turma = context.Turmas.FirstOrDefault(turma => turma.Numero == numero);
            if (_turma == null) return BadRequest($"A Turma {numero} não existe");

            //Verificar se a turma tem alunos
            Matricula _matricula = context.Matriculas.FirstOrDefault(_matricula => _matricula.TurmaID == _turma.ID);
            if (_matricula != null) return BadRequest("Turma tem alunos, não pode ser deletada.");


            context.Turmas.Remove(_turma);
            context.SaveChanges();
            return NoContent();
        }

    }
}
