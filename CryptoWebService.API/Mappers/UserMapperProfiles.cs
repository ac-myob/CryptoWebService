using AutoMapper;
using CryptoWebService.API.Dtos;
using CryptoWebService.Domain.Models;

namespace CryptoWebService.API.Mappers;

public class UserMapperProfiles : Profile
{
    public UserMapperProfiles()
    {
        CreateMap<User, UserGetDto>();
    }
}