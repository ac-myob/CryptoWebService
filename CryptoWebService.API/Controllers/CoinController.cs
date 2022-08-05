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
    
    [HttpGet("{id:int}")]
    public async Task<ActionResult<CoinGetDto>> GetCoinById(int id)
    {
        var coin = await _coinRepository.GetCoinByIdAsync(id);

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

        return CreatedAtAction(nameof(GetCoinById), new {id = coinGetDto.Id}, coinGetDto);
    }

    [HttpDelete]
    [Route("{id:int}")]
    public async Task<ActionResult> DeleteCoin(int id)
    {
        var deleteIsSuccessful = await _coinRepository.DeleteCoinAsync(id);

        return deleteIsSuccessful ? NoContent() : NotFound();
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<CoinGetDto>> UpdateCoin(int id, [FromBody] CoinPostPutDto coinPutDto)
    {
        var coinDomain = _mapper.Map<Coin>(coinPutDto);
        coinDomain.Id = id;

        var updateIsSuccessful = await _coinRepository.UpdateCoinAsync(coinDomain);

        var coinGetDto = _mapper.Map<CoinGetDto>(coinDomain);

        return updateIsSuccessful ? Ok(coinGetDto) : NotFound();
    }
}
