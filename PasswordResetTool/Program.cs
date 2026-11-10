using Microsoft.AspNetCore.Identity;

Console.Write("Enter the new password for lunga@raceday.co.za: ");
string password = ReadPassword();

var hasher = new PasswordHasher<object>();

string hash = hasher.HashPassword(new object(), password);

Console.WriteLine();
Console.WriteLine("Generated password hash:");
Console.WriteLine(hash);

Console.WriteLine();
Console.WriteLine("Copy the complete hash and keep it private.");

static string ReadPassword()
{
    string password = "";
    ConsoleKeyInfo key;

    do
    {
        key = Console.ReadKey(intercept: true);

        if (key.Key == ConsoleKey.Backspace && password.Length > 0)
        {
            password = password[..^1];
        }
        else if (!char.IsControl(key.KeyChar))
        {
            password += key.KeyChar;
        }
    }
    while (key.Key != ConsoleKey.Enter);

    return password;
}