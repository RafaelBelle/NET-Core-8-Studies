﻿using AutoMapper;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;
using CashFlow.Domain.Entities;

namespace CashFlow.Application.AutoMapper;
public class AutoMapping : Profile
{
    public AutoMapping()
    {
        RequestToEntity();
        EntityToResponse();
    }

    private void RequestToEntity()
    {
        CreateMap<RequestExpenseJson, Expense>()
            .ForMember(dest => dest.Tags, config => config.MapFrom(source => source.Tags.Distinct()));

        CreateMap<RequestRegisterUserJson, User>()
            .ForMember(dest => dest.Password, config => config.Ignore());

        CreateMap<Communication.Enums.Tag, Tag>()
            .ForMember(dest => dest.TagName, config => config.MapFrom(source => source));
    }

    private void EntityToResponse()
    {
        CreateMap<Expense, ResponseRegisteredExpenseJson>();
        CreateMap<Expense, ResponseShortExpenseJson>();

        CreateMap<Expense, ResponseExpenseJson>()
            .ForMember(dest => dest.Tags, config => config.MapFrom(source => source.Tags.Select(tag => tag.TagName)));

        CreateMap<User, ResponseUserProfileJson>();
    }
}
