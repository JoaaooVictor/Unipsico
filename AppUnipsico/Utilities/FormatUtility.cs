namespace AppUnipsico.Utilities
{
    public static class FormatUtility
    {
        public static string InserePontoTracoCpf(string cpf)
        {
            if (string.IsNullOrEmpty(cpf) || cpf.Length != 11)
            {
                return cpf;
            }

            return $"{cpf.Substring(0, 3)}.{cpf.Substring(3, 3)}.{cpf.Substring(6, 3)}-{cpf.Substring(9)}";
        }

        public static string RemovePontoTracoCpf(string cpf)
        {
            if (string.IsNullOrEmpty(cpf))
                return cpf;

            return new string(cpf.Where(char.IsDigit).ToArray());
        }
    }
}
