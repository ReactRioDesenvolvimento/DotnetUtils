namespace ReactRio.DotnetUtils;

public class HashedPassword
{
    private const int DefaultNumberOfIterations = 100_000;

    private HashedPassword(byte[] salt, int iterations, byte[] password)
    {
        Salt = salt;
        Iterations = iterations;
        Password = password;
    }

    private byte[] Salt { get; }
    private int Iterations { get; }
    private byte[] Password { get; }

    public string ToEncodedString()
    {
        var saltBase64 = Convert.ToBase64String(Salt);
        var passwordBase64 = Convert.ToBase64String(Password);
        var formatedString = $"{saltBase64};{Iterations};{passwordBase64}";
        var formatedStringBytes = Encoding.UTF8.GetBytes(formatedString);

        return Convert.ToBase64String(formatedStringBytes);
    }

    public bool Compare(string plainPassword)
    {
        var hashedPassword = GetHashedPassword(plainPassword, Salt, Iterations);

        return hashedPassword.Password.SequenceEqual(Password);
    }

    public static HashedPassword FromEncodedString(string encodedPassword)
    {
        var formatedStringBytes = Convert.FromBase64String(encodedPassword);
        var formatedString = Encoding.UTF8.GetString(formatedStringBytes);

        var parts = formatedString.Split(";");
        var salt = Convert.FromBase64String(parts[0]);
        var iterations = int.Parse(parts[1]);
        var password = Convert.FromBase64String(parts[2]);

        return new HashedPassword(salt, iterations, password);
    }

    public static HashedPassword FromPlainPassword(string plainPassword)
    {
        if (string.IsNullOrEmpty(plainPassword))
            throw new ArgumentException("Password cannot be null or empty", nameof(plainPassword));

        return GetHashedPassword(plainPassword);
    }

    private static HashedPassword GetHashedPassword
    (
        string plainTextPassword,
        byte[]? salt = null,
        int? iterations = null
    )
    {
        var numberOfIterations = iterations ?? DefaultNumberOfIterations;
        var saltBytes = salt ?? RandomNumberGenerator.GetBytes(32);

        var pbkdf2 = new Rfc2898DeriveBytes(plainTextPassword, saltBytes, numberOfIterations);
        var hashedPasswordBytes = pbkdf2.GetBytes(32);

        return new HashedPassword(saltBytes, numberOfIterations, hashedPasswordBytes);
    }
}
