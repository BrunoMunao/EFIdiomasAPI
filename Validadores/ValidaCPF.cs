namespace Validadores
{
	public class ValidaCPF
	{
		public static bool ValidarCPF(string cpf)
		{
			cpf = cpf.Replace(".", "").Replace("-", "");

			if (cpf.Length != 11)
				return false;

			int[] multiplicadores1 = { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
			int[] multiplicadores2 = { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

			string tempCpf = cpf.Substring(0, 9);
			int soma = 0;

			for (int i = 0; i < 9; i++)
				soma += int.Parse(tempCpf[i].ToString()) * multiplicadores1[i];

			int resto = soma % 11;

			if (resto < 2)
				resto = 0;
			else
				resto = 11 - resto;

			string digito = resto.ToString();
			tempCpf += digito;

			soma = 0;

			for (int i = 0; i < 10; i++)
				soma += int.Parse(tempCpf[i].ToString()) * multiplicadores2[i];

			resto = soma % 11;

			if (resto < 2)
				resto = 0;
			else
				resto = 11 - resto;

			digito += resto.ToString();

			return cpf.EndsWith(digito);
		}
	}
}