using AutoMapper;
using CryptoWebService.API.Dtos;
using CryptoWebService.Domain.Models;

namespace CryptoWebService.API.Mappers;

public class CoinMappingProfiles : Profile
{
    public CoinMappingProfiles()
    {
        CreateMap<CoinPostPutDto, Coin>();
        CreateMap<Coin, CoinGetDto>();
    }
}