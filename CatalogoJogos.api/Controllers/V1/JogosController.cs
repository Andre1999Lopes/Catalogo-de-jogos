using CatalogoJogos.api.InputModel;
using CatalogoJogos.api.Services;
using CatalogoJogos.api.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogoJogos.api.Controllers.V1
{
	[Route("api/v1/[controller]")]
	[ApiController]
	public class JogosController : ControllerBase
	{
		private readonly IJogoService _jogoService;

		public JogosController(IJogoService jogoService)
		{
			_jogoService = jogoService;
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<JogoViewModel>>> Get(
			[FromQuery, Range(1, int.MaxValue)] int pagina = 1,
			[FromQuery, Range(1, 50)] int quantidade = 5
		)
		{
			var jogos = await _jogoService.Get(pagina, quantidade);

			if (jogos.Count() == 0)
			{
				return NoContent();
			}
			return Ok(jogos);
		}

		[HttpGet("{idJogo:guid}")]
		public async Task<ActionResult<JogoViewModel>> GetGame([FromRoute] Guid idJogo)
		{
			var jogo = await _jogoService.GetGame(idJogo);
			if (jogo == null)
			{
				return NoContent();
			}
			return Ok(jogo);
		}

		[HttpPost]
		public async Task<ActionResult<JogoViewModel>> InsertGame([FromBody] JogoInputModel novoJogo)
		{
			try
			{
				var jogo = await _jogoService.InsertGame(novoJogo);
				return Ok(jogo);
			}
			catch (Exception)
			{
				return UnprocessableEntity("Já exite um jogo com esse nome para esta produtora");
			}
		}

		[HttpPut("{idJogo:guid}")]
		public async Task<ActionResult> UpdateGame([FromRoute] Guid idJogo, [FromBody] JogoInputModel jogo)
		{
			try
			{
				await _jogoService.UpdateGame(idJogo, jogo);
				return Ok();
			}
			catch (Exception)
			{
				return NotFound("O jogo não existe");
			}
		}

		[HttpPatch("{idJogo:guid}/preco/{preco:double}")]
		public async Task<ActionResult> UpdateGame([FromRoute] Guid idJogo, [FromRoute] double preco)
		{
			try
			{
				await _jogoService.UpdateGame(idJogo, preco);
				return Ok();
			}
			catch (Exception)
			{
				return NotFound("O jogo não existe");
			}
			
		}

		[HttpDelete("{idJogo:guid}")]
		public async Task<ActionResult> DeleteGame([FromRoute] Guid idJogo)
		{
			try
			{
				await _jogoService.DeleteGame(idJogo);
				return Ok();
			}
			catch (Exception)
			{
				return NotFound("O jogo não existe");
			}
		}
	}
}
