using Cruxlab;

const string filePath = "text.txt";
var tfr = new TextFileReader();
var content = tfr.ReadTextFile(filePath);
var pm = new PasswordManager();
var validCount = pm.GetValidPasswordsCount(content);
Console.WriteLine($"There are {validCount} valid passwords.");
