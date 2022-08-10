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
    public async Task<ActionResult<IEnumerable<UserGetDto>>> GetAllUsers()
    {
        var users = await _userRepository.GetAllUsersAsync();
        var userGetDtos = _mapper.Map<IEnumerable<UserGetDto>>(users);

        return Ok(userGetDtos);
    }    
    
    [HttpGet("{userId:int}")]
    public async Task<ActionResult<UserGetDto?>> GetUserById(int userId)
    {
        var user = await _userRepository.GetUserByIdAsync(userId);
        if (user == null) 
            return NotFound();
        var userGetDto = _mapper.Map<UserGetDto>(user);

        return Ok(userGetDto);
    }

    [HttpPost]
    public async Task<ActionResult<UserGetDto>> CreateUser(UserPostPutDto userPostDto)
    {
        var userDomain = _mapper.Map<User>(userPostDto);
        await _userRepository.CreateUserAsync(userDomain);
        var userGetDto = _mapper.Map<UserGetDto>(userDomain);

        return CreatedAtAction(nameof(GetUserById), new {userId = userGetDto.Id}, userGetDto);
    }

    [HttpPut("{userId:int}")]
    public async Task<ActionResult<UserGetDto>> UpdateUser(int userId, UserPostPutDto userPutDto)
    {
        var userDomain = _mapper.Map<User>(userPutDto);
        userDomain.Id = userId;
        var updateIsSuccessful = await _userRepository.UpdateUserAsync(userDomain);
        var userGetDto = _mapper.Map<UserGetDto>(userDomain);

        return updateIsSuccessful ? Ok(userGetDto) : BadRequest();
    }

    [HttpDelete]
    public async Task<ActionResult> DeleteUser(int userId)
    {
        var deleteIsSuccessful = await _userRepository.DeleteUserAsync(userId);

        return deleteIsSuccessful ? NoContent() : NotFound();
    }

    [HttpGet("{userId:int}/transactions")]
    public async Task<ActionResult<IEnumerable<TransactionGetDto>>> GetUserTransactions(int userId)
    {
        var transactionDomains = await _userRepository.GetUserTransactionsAsync(userId);
        var transactionGetDtos = _mapper.Map<IEnumerable<TransactionGetDto>>(transactionDomains);
        
        return Ok(transactionGetDtos);
    }
    
    [HttpGet("{userId:int}/transactions/{transactionId:int}")]
    public async Task<ActionResult<IEnumerable<TransactionGetDto>>> GetUserTransactions(int userId, int transactionId)
    {
        var transactionDomain = await _userRepository.GetUserTransactionAsync(userId, transactionId);
        var transactionGetDto = _mapper.Map<TransactionGetDto>(transactionDomain);
        
        return Ok(transactionGetDto);
    }
    
    [HttpPost("{userId:int}/transactions")]
    public async Task<ActionResult<TransactionGetDto>> CreateUserTransaction(
        int userId, [FromBody] TransactionPostPutDto transactionPostDto)
    {
        var transactionDomain = _mapper.Map<Transaction>(transactionPostDto);
        var createIsSuccessful = await _userRepository.CreateUserTransactionAsync(userId, transactionDomain);
        if (!createIsSuccessful)
            return NotFound();
        var transactionGetDtos = _mapper.Map<TransactionGetDto>(transactionDomain);
        
        return Ok(transactionGetDtos);
    }
}
