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
    private readonly IRepository<Coin, int> _coinRepository;
    private readonly IMapper _mapper;

    public CoinController(IRepository<Coin, int> coinRepository, IMapper mapper)
    {
        _coinRepository = coinRepository;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CoinGetDto>>> GetAllCoins()
    {
        var coins = await _coinRepository.GetAllAsync();
        var coinsGetDto = _mapper.Map<IEnumerable<CoinGetDto>>(coins);

        return Ok(coinsGetDto);
    }
    
    [HttpGet("{coinId:int}")]
    public async Task<ActionResult<CoinGetDto>> GetCoinById(int coinId)
    {
        var coin = await _coinRepository.GetByIdAsync(coinId);

        if (coin == null)
            return NotFound();

        var coinGetDto = _mapper.Map<CoinGetDto>(coin);
        return Ok(coinGetDto);
    }

    [HttpPost]
    public async Task<ActionResult<CoinGetDto>> CreateCoin(CoinPostPutDto coinPostDto)
    {
        var coinDomain = _mapper.Map<Coin>(coinPostDto); 
        await _coinRepository.CreateAsync(coinDomain);
        var coinGetDto = _mapper.Map<CoinGetDto>(coinDomain);

        return CreatedAtAction(nameof(GetCoinById), new {coinId = coinGetDto.Id}, coinGetDto);
    }

    [HttpDelete]
    [Route("{coinId:int}")]
    public async Task<ActionResult> DeleteCoin(int coinId)
    {
        var deleteIsSuccessful = await _coinRepository.DeleteAsync(coinId);

        return deleteIsSuccessful ? NoContent() : NotFound();
    }

    [HttpPut("{coinId:int}")]
    public async Task<ActionResult<CoinGetDto>> UpdateCoin(int coinId, [FromBody] CoinPostPutDto coinPutDto)
    {
        var coinDomain = _mapper.Map<Coin>(coinPutDto);
        var updateIsSuccessful = await _coinRepository.UpdateAsync(coinId, coinDomain);
        var coinGetDto = _mapper.Map<CoinGetDto>(coinDomain);

        return updateIsSuccessful ? Ok(coinGetDto) : BadRequest();
    }
}
