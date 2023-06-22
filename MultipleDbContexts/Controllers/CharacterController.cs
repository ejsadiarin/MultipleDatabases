using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MultipleDbContexts.Data;
using MultipleDbContexts.Models;

namespace MultipleDbContexts.Controllers;

[Route("api/[controller]")]
[ApiController]
// [Authorize]
public class CharacterController : ControllerBase
{
    private readonly UserDbContext _userDbContext;
    private readonly CharacterDbContext _characterDbContext;

    public CharacterController(UserDbContext userDbContext, CharacterDbContext characterDbContext)
    {
        _userDbContext = userDbContext;
        _characterDbContext = characterDbContext;
    }
    
    // API methods
    // NOTE: assume there is a [Authorize] on top, meaning we have an authenticated user
    [HttpGet("{userId}")]
    public async Task<ActionResult<UserResponseDto>> GetCharacterById(int userId)
    {
        var user = await _userDbContext.Users.FirstAsync(u => u.Id == userId);
        var characters = await _characterDbContext.Characters.Where(c => c.UserId == user.Id).ToListAsync();

        var result = new UserResponseDto(user.Id, user.Name, characters);
        
        return Ok(result);
    }
}