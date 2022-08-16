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
    private readonly IRepository<User, int> _userRepository;
    private readonly IMapper _mapper;

    public UserController(IRepository<User, int> userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserGetDto>>> GetAllUsers()
    {
        var users = await _userRepository.GetAllAsync();
        var userGetDtos = _mapper.Map<IEnumerable<UserGetDto>>(users);

        return Ok(userGetDtos);
    }    
    
    [HttpGet("{userId:int}")]
    public async Task<ActionResult<UserGetDto?>> GetUserById(int userId)
    {
        var user = await _userRepository.GetByIdAsync(userId);
        if (user == null) 
            return NotFound();
        var userGetDto = _mapper.Map<UserGetDto>(user);

        return Ok(userGetDto);
    }

    [HttpPost]
    public async Task<ActionResult<UserGetDto>> CreateUser(UserPostPutDto userPostDto)
    {
        var userDomain = _mapper.Map<User>(userPostDto);
        await _userRepository.CreateAsync(userDomain);
        var userGetDto = _mapper.Map<UserGetDto>(userDomain);

        return CreatedAtAction(nameof(GetUserById), new {userId = userGetDto.Id}, userGetDto);
    }

    [HttpPut("{userId:int}")]
    public async Task<ActionResult<UserGetDto>> UpdateUser(int userId, UserPostPutDto userPutDto)
    {
        var userDomain = _mapper.Map<User>(userPutDto);
        var updateIsSuccessful = await _userRepository.UpdateAsync(userId, userDomain);
        var userGetDto = _mapper.Map<UserGetDto>(userDomain);

        return updateIsSuccessful ? Ok(userGetDto) : BadRequest();
    }

    [HttpDelete]
    public async Task<ActionResult> DeleteUser(int userId)
    {
        var deleteIsSuccessful = await _userRepository.DeleteAsync(userId);

        return deleteIsSuccessful ? NoContent() : NotFound();
    }
}
