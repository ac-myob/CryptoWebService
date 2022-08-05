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
    
    [HttpGet("{userId:int}")]
    public async Task<ActionResult<User?>> GetUserById(int userId)
    {
        var user = await _userRepository.GetUserByIdAsync(userId);
        if (user == null) 
            return NotFound();
        var userGetDto = _mapper.Map<UserGetDto>(user);

        return Ok(userGetDto);
    }

    [HttpPost]
    public async Task<ActionResult<User>> CreateUser(UserPostPutDto userPostDto)
    {
        var userDomain = _mapper.Map<User>(userPostDto);
        await _userRepository.CreateUserAsync(userDomain);
        var userGetDto = _mapper.Map<UserGetDto>(userDomain);

        return CreatedAtAction(nameof(GetUserById), new {id = userGetDto.Id}, userGetDto);
    }

    [HttpPut("{userId:int}")]
    public async Task<ActionResult<User>> UpdateUser(int userId, UserPostPutDto userPutDto)
    {
        var userDomain = _mapper.Map<User>(userPutDto);
        userDomain.Id = userId;
        var updateIsSuccessful = await _userRepository.UpdateUserAsync(userDomain);
        var userGetDto = _mapper.Map<UserGetDto>(userDomain);

        return updateIsSuccessful ? Ok(userGetDto) : BadRequest();
    }
}
