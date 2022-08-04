using AutoMapper;
using CryptoWebService.API.Dtos;
using CryptoWebService.Application.Abstractions;
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
        var coinsGetDto = _mapper.Map<List<CoinGetDto>>(coins);

        return Ok(coinsGetDto);
    }
    
    [HttpGet("{id:int}")]
    public async Task<ActionResult<CoinGetDto>> GetCoinById(int id)
    {
        var coin = await _coinRepository.GetCoinByIdAsync(id);

        if (coin == null)
            return NotFound();

        var coinGetDto = _mapper.Map<CoinGetDto>(coin);
        return coinGetDto;
    }
}
