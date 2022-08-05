using AutoMapper;
using CryptoWebService.API.Dtos;
using CryptoWebService.Application.Abstractions;
using CryptoWebService.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace CryptoWebService.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : Controller
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public UserController(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult> GetAllUsers()
    {
       return Ok(await _userRepository.GetAllUsersAsync());
    }    
    
    [HttpGet("{id:int}")]
    public async Task<ActionResult<User?>> GetUserById(int id)
    {
        var user = await _userRepository.GetUserByIdAsync(id);

        if (user == null)
            return NotFound();

        var userGetDto = _mapper.Map<UserGetDto>(user);

        return Ok(userGetDto);
    }
}
