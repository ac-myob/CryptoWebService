using AutoMapper;
using CryptoWebService.API.Dtos;
using CryptoWebService.Domain.Models;

namespace CryptoWebService.API.Mappers;

public class TransactionMapperProfiles : Profile
{
    public TransactionMapperProfiles()
    {
        CreateMap<TransactionPostPutDto, Transaction>();
        CreateMap<Transaction, TransactionGetDto>();
    }
}