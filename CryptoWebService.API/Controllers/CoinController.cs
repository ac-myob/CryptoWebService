using AutoMapper;
using CryptoWebService.API.Dtos;
using CryptoWebService.Application.Abstractions;
using CryptoWebService.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace CryptoWebService.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CoinController : Controller
{
    private readonly ICoinRepository _coinRepository;
    private readonly IMapper _mapper;

    public CoinController(ICoinRepository coinRepository, IMapper mapper)
    {
        _coinRepository = coinRepository;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CoinGetDto>>> GetAllCoins()
    {
        var coins = await _coinRepository.GetAllCoinsAsync();
        var coinsGetDto = _mapper.Map<IEnumerable<CoinGetDto>>(coins);

        return Ok(coinsGetDto);
    }
    
    [HttpGet("{coinId:int}")]
    public async Task<ActionResult<CoinGetDto>> GetCoinById(int coinId)
    {
        var coin = await _coinRepository.GetCoinByIdAsync(coinId);

        if (coin == null)
            return NotFound();

        var coinGetDto = _mapper.Map<CoinGetDto>(coin);
        return Ok(coinGetDto);
    }

    [HttpPost]
    public async Task<ActionResult<CoinGetDto>> CreateCoin(CoinPostPutDto coinPostDto)
    {
        var coinDomain = _mapper.Map<Coin>(coinPostDto); 
        await _coinRepository.CreateCoinAsync(coinDomain);
        var coinGetDto = _mapper.Map<CoinGetDto>(coinDomain);

        return CreatedAtAction(nameof(GetCoinById), new {coinId = coinGetDto.Id}, coinGetDto);
    }

    [HttpDelete]
    [Route("{coinId:int}")]
    public async Task<ActionResult> DeleteCoin(int coinId)
    {
        var deleteIsSuccessful = await _coinRepository.DeleteCoinAsync(coinId);

        return deleteIsSuccessful ? NoContent() : NotFound();
    }

    [HttpPut("{coinId:int}")]
    public async Task<ActionResult<CoinGetDto>> UpdateCoin(int coinId, [FromBody] CoinPostPutDto coinPutDto)
    {
        var coinDomain = _mapper.Map<Coin>(coinPutDto);
        coinDomain.Id = coinId;
        var updateIsSuccessful = await _coinRepository.UpdateCoinAsync(coinDomain);
        var coinGetDto = _mapper.Map<CoinGetDto>(coinDomain);

        return updateIsSuccessful ? Ok(coinGetDto) : BadRequest();
    }
}
