namespace Cruxlab
{
    public class PasswordManager
    {
        private const int REQUIRED_IDX = 0;
        private const int MINMAX_IDX = 1;
        private const int MIN_IDX = 0;
        private const int MAX_IDX = 1;
        private const int PASS_IDX = 2;

        private List<PasswordProperties> GetPasswordProperties(string content)
        {
            var result = new List<PasswordProperties>();
            if(content == null)
                throw new ArgumentNullException(nameof(content));

            var lines = content.Split('\n');
            foreach (var line in lines)
            {
                var props = line.Split(' ');
                var required = props[REQUIRED_IDX][0];
                var minMax = props[MINMAX_IDX].Replace(":", string.Empty).Split('-');
                var min = int.Parse(minMax[MIN_IDX]);
                var max = int.Parse(minMax[MAX_IDX]);
                var password = props[PASS_IDX];

                result.Add(new PasswordProperties()
                {
                    RequiredSymbol = required,
                    Min = min,
                    Max = max,
                    Password = password
                });
            }

            return result;
        }

        private bool CheckValidPassword(PasswordProperties pass)
        {
            var symbolsCount = pass.Password.Count(x => x.Equals(pass.RequiredSymbol));
            return symbolsCount >= pass.Min && symbolsCount <= pass.Max;
        }

        public int GetValidPasswordsCount(string content)
        {
            try
            {
                var passwords = GetPasswordProperties(content);
                return passwords.Count(CheckValidPassword);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return 0;
        }
    }
}
