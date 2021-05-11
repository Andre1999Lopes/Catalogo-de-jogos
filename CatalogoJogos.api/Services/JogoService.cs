using CatalogoJogos.api.Entities;
using CatalogoJogos.api.Exceptions;
using CatalogoJogos.api.InputModel;
using CatalogoJogos.api.Repositories;
using CatalogoJogos.api.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogoJogos.api.Services
{
	public class JogoService : IJogoService
	{
		private readonly IJogoRepository _jogoRepository;

		public JogoService(IJogoRepository jogoRepository)
		{
			_jogoRepository = jogoRepository;
		}

		public async Task<List<JogoViewModel>> Get(int pagina, int quantidade)
		{
			var jogos = await _jogoRepository.Get(pagina, quantidade);

			return jogos.Select(jogo => new JogoViewModel
			{
				Id = jogo.Id,
				Nome = jogo.Nome,
				Produtora = jogo.Produtora,
				Preco = jogo.Preco
			}).ToList();
		}

		public async Task<JogoViewModel> GetGame(Guid idJogo)
		{
			var jogo = await _jogoRepository.GetById(idJogo);

			if (jogo == null)
			{
				return null;
			}

			return new JogoViewModel
			{
				Id = jogo.Id,
				Nome = jogo.Nome,
				Produtora = jogo.Produtora,
				Preco = jogo.Preco
			};
		}

		public async Task<JogoViewModel> InsertGame(JogoInputModel jogo)
		{
			var entidadeJogo = await _jogoRepository.GetByName(jogo.Nome, jogo.Produtora);
			if (entidadeJogo.Count > 0)
			{
				throw new JogoJaCadastradoException();
			}

			var novoJogo = new Jogo()
			{
				Id = Guid.NewGuid(),
				Nome = jogo.Nome,
				Produtora = jogo.Produtora,
				Preco = jogo.Preco
			};

			await _jogoRepository.Insert(novoJogo);

			return new JogoViewModel()
			{
				Id = novoJogo.Id,
				Nome = novoJogo.Nome,
				Produtora = novoJogo.Produtora,
				Preco = novoJogo.Preco
			};
		}

		public async Task UpdateGame(Guid idJogo, JogoInputModel jogo)
		{
			var entidadeJogo = await _jogoRepository.GetById(idJogo);

			if (entidadeJogo == null)
			{
				throw new JogoNaoCadastradoException();
			}

			entidadeJogo.Nome = jogo.Nome;
			entidadeJogo.Preco = jogo.Preco;
			entidadeJogo.Produtora = jogo.Produtora;

			await _jogoRepository.Update(entidadeJogo);
		}

		public async Task UpdateGame(Guid idJogo, double preco)
		{
			var entidadeJogo = await _jogoRepository.GetById(idJogo);

			if (entidadeJogo == null)
			{
				throw new JogoNaoCadastradoException();
			}

			entidadeJogo.Preco = preco;

			await _jogoRepository.Update(entidadeJogo);
		}

		public async Task DeleteGame(Guid idJogo)
		{
			var jogo = await _jogoRepository.GetById(idJogo);

			if (jogo == null)
			{
				throw new JogoNaoCadastradoException();
			}

			await _jogoRepository.Delete(idJogo);
		}

		public void Dispose()
		{
			_jogoRepository?.Dispose();
		}
	}
}
