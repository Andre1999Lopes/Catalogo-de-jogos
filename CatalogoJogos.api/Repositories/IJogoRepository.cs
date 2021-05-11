using CatalogoJogos.api.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogoJogos.api.Repositories
{
	public interface IJogoRepository : IDisposable
	{
		Task<List<Jogo>> Get(int pagina, int quantidade);
		Task<Jogo> GetById(Guid id);
		Task<List<Jogo>> GetByName(string nome, string produtora);
		Task Insert(Jogo jogo);
		Task Update(Jogo jogo);
		Task Delete(Guid id);
	}
}
