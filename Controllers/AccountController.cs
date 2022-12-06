using Blog.Data;
using Blog.Extensions;
using Blog.Models;
using Blog.Services;
using Blog.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SecureIdentity.Password;

namespace Blog.Controllers;


[ApiController]
public class AccountController :ControllerBase
{
    [HttpPost("v1/accounts/")]
    public async Task<IActionResult> Post([FromBody]RegisterViewModel model,[FromServices]BlogDataContext context)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ResultViewModel<string>(ModelState.GetErrors()));
        var user = new User
        {
            Name = model.Name,
            Email = model.Email,
            Slug = model.Email.Replace("@", "-").Replace(".", "-")

        };

        var password = PasswordGenerator.Generate(25);
        user.PasswordHash = PasswordHasher.Hash(password);

        try
        {
            await context.Users.AddAsync(user);
            await context.SaveChangesAsync();

            return Ok(new ResultViewModel<dynamic>(new
            {
                user = user.Email, password
            }));
        }
        catch (DbUpdateException)
        {
            return StatusCode(400, new ResultViewModel<string>("0x599 - Email já cadastrado"));
        }
        catch
        {
            return StatusCode(500, new ResultViewModel<dynamic>("0x020 - Falha interna do servidor."));
        }






    }
    
    


    
    
    [HttpPost("v1/accounts/login")]
    public IActionResult Login([FromServices]TokenService _tokenService)
    {
         var token = _tokenService.GenerateToken(null);
        return Ok(token);
    }

   
}