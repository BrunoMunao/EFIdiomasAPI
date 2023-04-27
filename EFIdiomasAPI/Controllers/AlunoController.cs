using EFIdiomasAPI.Data;
using EFIdiomasAPI.Entities;
using Microsoft.AspNetCore.Mvc;
using EFIdiomasAPI.Services.Interfaces;
using EFIdiomasAPI.DTOs;



namespace EFIdiomasAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlunoController : ControllerBase
    {
        private readonly IAlunoService _alunoService;

        public AlunoController(IAlunoService alunoService, DataContext context)
        {
            _alunoService = alunoService;
        }

		[HttpPost]
        public async Task<ActionResult<Aluno>> Create(CreateAlunoDto alunoRequest)
        {
			try
			{
				var aluno = await _alunoService.Create(alunoRequest);
				return Ok(aluno);
			}
			catch (ArgumentException ex)
			{
				return BadRequest(ex.Message);
			}
			catch (Exception ex)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
			}
		}

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Aluno>>> GetAll()
        {
            try
            {
                var alunos = await _alunoService.GetAll();
				if (alunos == null)
				{
					return NotFound();
				}
				return Ok(alunos);
			}
			catch (Exception ex)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
			}
		}


        [HttpGet("{cpf}")]
        public async Task<ActionResult<Aluno>> Get(string cpf)
        {
            try
            {
				var aluno = await _alunoService.Get(cpf);
				if (aluno == null)
				{
					return NotFound();
				}
				return Ok(aluno);
			}
			catch (Exception ex)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
			}

		}

        [HttpPut("{cpf}")]
        public async Task<ActionResult<Aluno>> Put(UpdateAlunoDto alunoRequest, string cpf)
        {
            try 
			{
				var aluno = await _alunoService.Update(alunoRequest, cpf);
				if (aluno == null)
				{
					return BadRequest();
				}
				return Ok(aluno);
			}
			catch (Exception ex)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
			}
        }

        [HttpDelete("{cpf}")]
        public async Task<ActionResult> Delete(string cpf)
        {
			try
			{
				var aluno = await _alunoService.Delete(cpf);
				if (aluno == null)
				{
					return NotFound();
				}
				return NoContent();
			}
			catch (Exception ex)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
			}
		}

    }
}
