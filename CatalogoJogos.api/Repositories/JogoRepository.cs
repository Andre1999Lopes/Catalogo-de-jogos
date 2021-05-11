using CatalogoJogos.api.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogoJogos.api.Repositories
{
	public class JogoRepository : IJogoRepository
	{
		private static Dictionary<Guid, Jogo> jogos = new Dictionary<Guid, Jogo>
		{
			{ Guid.Parse("f1ec1deb-83ed-4f9a-b7e6-554efee551ef"), new Jogo { Id = Guid.Parse("f1ec1deb-83ed-4f9a-b7e6-554efee551ef"), Nome = "Fifa 20", Produtora = "EA", Preco = 200 } },
			{ Guid.Parse("8c02729f-3289-46fc-a49d-0e48a8bc95d9"), new Jogo { Id = Guid.Parse("8c02729f-3289-46fc-a49d-0e48a8bc95d9"), Nome = "Fifa 19", Produtora = "EA", Preco = 190 } },
			{ Guid.Parse("b9780b9f-a754-4b24-bfda-86eccce9ba65"), new Jogo { Id = Guid.Parse("b9780b9f-a754-4b24-bfda-86eccce9ba65"), Nome = "Fifa 18", Produtora = "EA", Preco = 180 } },
			{ Guid.Parse("22bb12b7-c7be-4f49-9b01-fe3f9deaad70"), new Jogo { Id = Guid.Parse("22bb12b7-c7be-4f49-9b01-fe3f9deaad70"), Nome = "Dark Souls 3", Produtora = "From Software", Preco = 150 } }
		};

		public Task<List<Jogo>> Get(int pagina, int quantidade)
		{
			return Task.FromResult(jogos.Values.Skip((pagina - 1) * quantidade).Take(quantidade).ToList());
		}

		public Task<Jogo> GetById(Guid id)
		{
			if (!jogos.ContainsKey(id))
			{
				return null;
			}
			return Task.FromResult(jogos[id]);
		}

		public Task<List<Jogo>> GetByName(string nome, string produtora)
		{
			return Task.FromResult(jogos.Values.Where(jogo => jogo.Nome.Equals(nome) && jogo.Produtora.Equals(produtora)).ToList());
		}

		public Task Insert(Jogo jogo)
		{
			jogos.Add(jogo.Id, jogo);
			return Task.CompletedTask;
		}

		public Task Update(Jogo jogo)
		{
			jogos[jogo.Id] = jogo;
			return Task.CompletedTask;
		}
		public Task Delete(Guid id)
		{
			jogos.Remove(id);
			return Task.CompletedTask;
		}

		public void Dispose()
		{
			// TODO
		}
	}
}
