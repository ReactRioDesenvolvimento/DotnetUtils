namespace ReactRio.Utils;

public static class Base64
{
    public static string ToUrlSafeFromBytes(byte[] bytes)
    {
        var base64 = Convert.ToBase64String(bytes);

        return base64.Replace("/", "-");
    }

    public static byte[] FromUrlSafeToBytes(string urlSafe)
    {
        var base64 = urlSafe.Replace('-', '/');

        return Convert.FromBase64String(base64);
    }
}
