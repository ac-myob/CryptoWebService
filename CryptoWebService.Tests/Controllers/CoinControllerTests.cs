using AutoMapper;
using CryptoWebService.API.Controllers;
using CryptoWebService.API.Dtos;
using CryptoWebService.API.Mappers;
using CryptoWebService.Application.Abstractions;
using CryptoWebService.Domain.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace CryptoWebService.Tests.Controllers;

public class CoinControllerTests
{
    private readonly CoinController _coinController;
    private readonly Mock<IRepository<Coin, int>> _mockCoinRepository;
    private readonly IMapper _mapper;
    
    public CoinControllerTests()
    {
        _mockCoinRepository = new Mock<IRepository<Coin, int>>();
        _mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile<CoinMappingProfiles>()));
        _coinController = new CoinController(_mockCoinRepository.Object, _mapper);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(3)]
    public async Task GetAllCoins_ReturnsAllCoinsInRepositoryWithStatusCode200(int numberOfCoins)
    {
        var expectedCoins = SampleCoins.GetMany(numberOfCoins);
        _mockCoinRepository.Setup(f => f.GetAllAsync()).ReturnsAsync(expectedCoins);
        var expectedCoinDtos = _mapper.Map<IEnumerable<CoinGetDto>>(expectedCoins);
        
        var result = await _coinController.GetAllCoins();
        var actionResult = Assert.IsType<ActionResult<IEnumerable<CoinGetDto>>>(result);
        var okObjectResult = Assert.IsType<OkObjectResult>(actionResult.Result);
        var actualCoinGetDtos = Assert.IsAssignableFrom<IEnumerable<CoinGetDto>>(okObjectResult.Value);

        actualCoinGetDtos.Should().BeEquivalentTo(expectedCoinDtos);
    }

    [Fact]
    public async Task GetCoinById_ReturnsCoinWithMatchingIdWithStatusCode200_GivenCoinId()
    {
        var expectedCoin = SampleCoins.GetOne();
        _mockCoinRepository.Setup(f => f.GetByIdAsync(expectedCoin.Id))
            .ReturnsAsync(expectedCoin);
        var expectedCoinGetDto = _mapper.Map<CoinGetDto>(expectedCoin);

        var result = await _coinController.GetCoinById(1);
        var actionResult = Assert.IsType<ActionResult<CoinGetDto>>(result);
        var okObjectResult = Assert.IsType<OkObjectResult>(actionResult.Result);
        var actualCoinGetDto = Assert.IsAssignableFrom<CoinGetDto>(okObjectResult.Value);

        actualCoinGetDto.Should().BeEquivalentTo(expectedCoinGetDto);
    }
    
    [Fact]
    public async Task GetCoinById_ReturnsStatusCode404_GivenNonExistentCoinId()
    {
        _mockCoinRepository.Setup(f => f.GetByIdAsync(1)).ReturnsAsync((Coin?) null);
        var result = await _coinController.GetCoinById(1);
        var actionResult = Assert.IsType<ActionResult<CoinGetDto>>(result);
        var notFoundObjectResult = Assert.IsType<NotFoundObjectResult>(actionResult.Result);
        
        Assert.Equal("Cannot find coin with id 1.", notFoundObjectResult.Value);
    }
}
