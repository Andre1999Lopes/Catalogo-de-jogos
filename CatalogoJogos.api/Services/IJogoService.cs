using CatalogoJogos.api.InputModel;
using CatalogoJogos.api.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogoJogos.api.Services
{
    public interface IJogoService : IDisposable
    {
        Task<List<JogoViewModel>> Get(int pagina, int quantidade);
        Task<JogoViewModel> GetGame(Guid idJogo);
        Task<JogoViewModel> InsertGame(JogoInputModel jogo);
        Task UpdateGame(Guid idJogo, JogoInputModel jogo);
        Task UpdateGame(Guid idJogo, double preco);
        Task DeleteGame(Guid idJogo);

    }
}
