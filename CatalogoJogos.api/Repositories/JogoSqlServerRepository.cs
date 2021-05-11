using CatalogoJogos.api.Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogoJogos.api.Repositories
{
	public class JogoSqlServerRepository : IJogoRepository
	{
		private readonly SqlConnection _sqlConnection;

		public JogoSqlServerRepository(IConfiguration configuration)
		{
			_sqlConnection = new SqlConnection(configuration.GetConnectionString("Default"));
		}

		public async Task<List<Jogo>> Get(int pagina, int quantidade)
		{
			var jogos = new List<Jogo>();
			var comando = $"select * from Jogos order by id offset {((pagina -1) * quantidade)} rows fetch next {quantidade} rows only";

			await _sqlConnection.OpenAsync();

			SqlCommand sqlCommand = new SqlCommand(comando, _sqlConnection);
			SqlDataReader reader = await sqlCommand.ExecuteReaderAsync();

			while (reader.Read())
			{
				jogos.Add(new Jogo { 
					Id = (Guid)reader["Id"],
					Nome = (string)reader["Nome"],
					Produtora = (string)reader["Produtora"],
					Preco = (double)reader["Preco"]
				});
			}

			await _sqlConnection.CloseAsync();

			return jogos;
		}

		public async Task<Jogo> GetById(Guid id)
		{
			Jogo jogo = null;

			var comando = $"select * from Jogos where Id = '{id}'";

			await _sqlConnection.OpenAsync();
			SqlCommand sqlCommand = new SqlCommand(comando, _sqlConnection);
			SqlDataReader reader = await sqlCommand.ExecuteReaderAsync();

			while (reader.Read())
			{
				jogo = new Jogo()
				{
					Id = (Guid)reader["Id"],
					Nome = (string)reader["Nome"],
					Produtora = (string)reader["Produtora"],
					Preco = (double)reader["Preco"]
				};
			}

			await _sqlConnection.CloseAsync();

			return jogo;
		}

		public async Task<List<Jogo>> GetByName(string nome, string produtora)
		{
			var jogos = new List<Jogo>();

			var comando = $"select * from Jogos where Nome = {nome} and Produtora = {produtora}";

			await _sqlConnection.OpenAsync();
			SqlCommand sqlCommand = new SqlCommand(comando, _sqlConnection);
			SqlDataReader reader = await sqlCommand.ExecuteReaderAsync();

			while (reader.Read())
			{
				jogos.Add(new Jogo()
				{
					Id = (Guid)reader["Id"],
					Nome = (string)reader["Nome"],
					Produtora = (string)reader["Produtora"],
					Preco = (double)reader["Preco"]
				});
			}

			await _sqlConnection.CloseAsync();

			return jogos;
		}

		public async Task Insert(Jogo jogo)
		{
			var comando = $"insert into Jogos (Id, Nome, Produtora, Preco) values ('{jogo.Id}', '{jogo.Nome}', '{jogo.Produtora}', '{jogo.Preco.ToString().Replace(",", ".")}')";

			await _sqlConnection.OpenAsync();
			SqlCommand sqlCommand = new SqlCommand(comando, _sqlConnection);
			sqlCommand.ExecuteNonQuery();

			await _sqlConnection.CloseAsync();
		}

		public async Task Update(Jogo jogo)
		{
			var comando = $"update Jogos set Nome = '{jogo.Nome}', Produtora = '{jogo.Produtora}', Preco = '{jogo.Preco.ToString().Replace(",", ".")}'";

			await _sqlConnection.OpenAsync();
			SqlCommand sqlCommand = new SqlCommand(comando, _sqlConnection);
			await sqlCommand.ExecuteNonQueryAsync();

			await _sqlConnection.CloseAsync();
		}
		public async Task Delete(Guid id)
		{
			var comando = $"delete from Jogos where Id = '{id}'";

			await _sqlConnection.OpenAsync();
			SqlCommand sqlCommand = new SqlCommand(comando, _sqlConnection);
			await sqlCommand.ExecuteNonQueryAsync();

			await _sqlConnection.CloseAsync();
		}

		public void Dispose()
		{
			_sqlConnection?.Close();
			_sqlConnection?.Dispose();
		}
	}
}
