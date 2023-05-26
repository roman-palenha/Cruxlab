namespace Cruxlab
{
    public class PasswordManager
    {
        private List<PasswordProperties> GetPasswordProperties(string content)
        {
            var result = new List<PasswordProperties>();
            if(content == null)
                throw new ArgumentNullException(nameof(content));

            var lines = content.Split('\n');
            foreach (var line in lines)
            {
                var props = line.Split(' ');
                var required = props[0][0];
                var minMax = props[1].Replace(":", string.Empty).Split('-');
                var min = int.Parse(minMax[0]);
                var max = int.Parse(minMax[1]);
                var password = props[2];

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
