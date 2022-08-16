using AutoMapper;
using CryptoWebService.API.Dtos;
using CryptoWebService.Application.Abstractions;
using CryptoWebService.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace CryptoWebService.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TransactionController : Controller
{
    private readonly ITransactionService _transactionService;
    private readonly IMapper _mapper;

    public TransactionController(ITransactionService transactionService, IMapper mapper)
    {
        _transactionService = transactionService;
        _mapper = mapper;
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TransactionGetDto>>> GetAllTransactions()
    {
        var transactions = await _transactionService.GetAllAsync();
        var transactionGetDtos = _mapper.Map<IEnumerable<TransactionGetDto>>(transactions);

        return Ok(transactionGetDtos);
    }    
    
    [HttpGet("{transactionId:int}")]
    public async Task<ActionResult<TransactionGetDto?>> GetTransactionById(int transactionId)
    {
        var transaction = await _transactionService.GetByIdAsync(transactionId);
        if (transaction == null) 
            return NotFound();
        var transactionGetDto = _mapper.Map<TransactionGetDto>(transaction);

        return Ok(transactionGetDto);
    }

    [HttpPost]
    public async Task<ActionResult<TransactionGetDto>> CreateTransaction(TransactionPostPutDto transactionPostDto)
    {
        var transactionDomain = _mapper.Map<Transaction>(transactionPostDto);
        var createIsSuccessful = await _transactionService.CreateAsync(transactionDomain);
        var transactionGetDto = _mapper.Map<TransactionGetDto>(transactionDomain);

        if (!createIsSuccessful)
            return NotFound();
        
        return CreatedAtAction(
            nameof(GetTransactionById), 
            new {transactionId = transactionGetDto.Id}, 
            transactionGetDto);
    }

    [HttpPut("{transactionId:int}")]
    public async Task<ActionResult<TransactionGetDto>> UpdateTransaction(
        int transactionId, TransactionPostPutDto transactionPutDto)
    {
        var transactionDomain = _mapper.Map<Transaction>(transactionPutDto);
        var updateIsSuccessful = await _transactionService.UpdateAsync(transactionId, transactionDomain);
        var transactionGetDto = _mapper.Map<TransactionGetDto>(transactionDomain);

        return updateIsSuccessful ? Ok(transactionGetDto) : BadRequest();
    }

    [HttpDelete]
    public async Task<ActionResult> DeleteTransaction(int transactionId)
    {
        var deleteIsSuccessful = await _transactionService.DeleteAsync(transactionId);

        return deleteIsSuccessful ? NoContent() : NotFound($"Cannot find transaction with id {transactionId}.");
    }
}