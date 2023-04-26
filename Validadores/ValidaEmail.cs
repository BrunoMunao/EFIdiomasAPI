using System.Text.RegularExpressions;

namespace Validadores
{
	public class ValidaEmail
	{
		public static bool ValidarEmail(string email)
		{
			if (string.IsNullOrWhiteSpace(email))
				return false;

			try
			{
				// Define uma expressão regular para verificar se o endereço de e-mail é válido
				var regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
				return regex.IsMatch(email);
			}
			catch
			{
				// Ignora exceções e retorna false se houver algum erro
				return false;
			}
		}
	}

}
