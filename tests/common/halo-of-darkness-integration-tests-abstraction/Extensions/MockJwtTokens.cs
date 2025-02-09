using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;

namespace HaloOfDarkness.IntegrationTests.Abstraction.Extensions;

public static class MockJwtTokens
{
    private static readonly JwtSecurityTokenHandler _tokenHandler = new();
    private static readonly RandomNumberGenerator Rng = RandomNumberGenerator.Create();
    private static readonly byte[] Key = new byte[32];

    static MockJwtTokens()
    {
        Rng.GetBytes(Key);
        SecurityKey = new SymmetricSecurityKey(Key) { KeyId = Guid.NewGuid().ToString() };
        SigningCredentials = new SigningCredentials(SecurityKey, SecurityAlgorithms.HmacSha256);
    }

    public static string Issuer { get; } = Guid.NewGuid().ToString();
    public static SecurityKey SecurityKey { get; }
    public static SigningCredentials SigningCredentials { get; }

    public static string GenerateJwtToken(IEnumerable<Claim> claims)
    {
        return _tokenHandler.WriteToken(
            new JwtSecurityToken(
                Issuer,
                audience: null,
                claims,
                notBefore: null,
                DateTime.UtcNow.AddMinutes(20),
                SigningCredentials));
    }
}
