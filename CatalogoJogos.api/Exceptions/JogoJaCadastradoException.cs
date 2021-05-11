using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogoJogos.api.Exceptions
{
	public class JogoJaCadastradoException : Exception
	{
		public JogoJaCadastradoException() : base("O jogo já está cadastrado")
		{}
	}
}
