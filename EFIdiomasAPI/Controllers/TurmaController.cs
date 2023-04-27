using EFIdiomasAPI.Entities;
using EFIdiomasAPI.Services.Interfaces;
using EFIdiomasAPI.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

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
		public async Task<ActionResult<Turma>> Create([DefaultValue("0")] CreateTurmaDto turmaRequest)
		{
			try
			{
				var novaTurma = await _turmaService.Create(turmaRequest);
				if (novaTurma == null)
				{
					return BadRequest();
				}
				return novaTurma;
			}
			catch (Exception ex)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
			}
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<Turma>>> GetAll()
		{
			try
			{
				var turmas = await _turmaService.GetAll();
				if (turmas == null)
				{
					return NotFound();
				}
				return Ok(turmas);
			}
			catch (Exception ex)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
			}
		}

		[HttpGet("{numero}")]
		public async Task<ActionResult<Turma>> Get(string numero)
		{
			try
			{
				var turma = await _turmaService.Get(numero);
				if (turma == null)
				{
					return NotFound();
				}
				return Ok(turma);
			}
			catch (Exception ex)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
			}
		}

		[HttpPut("{numero}")]
		public async Task<ActionResult<Turma>> Put(UpdateTurmaDto turmaRequest, string numero)
		{
			try
			{
				var turma = await _turmaService.Update(turmaRequest, numero);
				if (turma == null)
				{
					return BadRequest();
				}

				return Ok(turma);
			}
			catch (Exception ex)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
			}
		}

		[HttpDelete("{numero}")]
		public async Task<ActionResult> Delete(string numero)
		{
			try
			{
				var turma = await _turmaService.Delete(numero);
				if (turma == null)
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
