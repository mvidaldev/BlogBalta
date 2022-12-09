using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Blog.Extensions;
using Blog.Models;
using Microsoft.IdentityModel.Tokens;

namespace Blog.Services;

public class TokenService
{
    public string GenerateToken(User user)
    {
        //criar instancia do token
        var tokenHandler = new JwtSecurityTokenHandler();
        //Pega a chave JWT e converte para byte
        var key =Encoding.ASCII.GetBytes(Configuration.JwtKey);
        //cria um security descriptor
        var claims = user.GetClaims();
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddHours(8),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        //gera o token passando o tokendescriptor
        var token = tokenHandler.CreateToken(tokenDescriptor);
        //retorna o token pronto.
        return tokenHandler.WriteToken(token);
    }
}