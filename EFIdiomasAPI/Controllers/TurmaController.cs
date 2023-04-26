using EFIdiomasAPI.Entities;
using EFIdiomasAPI.Services.Interfaces;
using EFIdiomasAPI.DTOs;
using EFIdiomasAPI.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EFIdiomasAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TurmaController : ControllerBase
    {

        private readonly ITurmaService _turmaService;

        public TurmaController(ITurmaService turmaService)
        {
            _turmaService = turmaService;
        }

        [HttpPost]
        public async Task<ActionResult<Turma>> Create(CreateTurmaDto turmaRequest)
        {
            var novaTurma = await _turmaService.Create(turmaRequest);
            if (novaTurma == null)
            {
                return BadRequest();
            }

            return novaTurma;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Turma>>> GetAll()
        {
            var turmas = await _turmaService.GetAll();

            return Ok(turmas);
        }

        [HttpGet("{numero}")]
        public async Task<ActionResult<Turma>> Get(string numero)
        {
            var turma = await _turmaService.Get(numero);

            if (turma == null)
            {
                return NotFound();
            }

            return Ok(turma);
        }

        [HttpPut("{numero}")]
        public async Task<ActionResult<Turma>> Put(UpdateTurmaDto turmaRequest, string numero)
        {
            var turma = await _turmaService.Update(turmaRequest, numero);
            if (turma == null)
            {
                return BadRequest();
            }

            return turma;
        }

        [HttpDelete("{numero}")]
        public async Task<ActionResult> Delete(string numero)
        {
            var turma = await _turmaService.Delete(numero);
            if (turma == null)
            {
                return NotFound();
            }

            return NoContent();
        }
    }

}
