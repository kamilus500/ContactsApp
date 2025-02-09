﻿using ContactsApp.Application.CurrentUser;
using ContactsApp.Domain.Dtos;
using ContactsApp.Domain.Entities;
using Mapster;

namespace ContactsApp.Application.Mapper
{
    public static class MapsterConfig
    {
        public static void Configure()
        {
            TypeAdapterConfig<Domain.Entities.Contact, ContactDto>.NewConfig()
                .Map(dest => dest.Id, src => src.Id)
                .Map(dest => dest.FirstName, src => src.FirstName)
                .Map(dest => dest.LastName, src => src.LastName)
                .Map(dest => dest.NumberPhone, src => src.NumberPhone)
                .Map(dest => dest.Email, src => src.Email);

            TypeAdapterConfig<CreateUpdateContactDto, Domain.Entities.Contact>.NewConfig()
                .Map(dest => dest.FirstName, src => src.FirstName)
                .Map(dest => dest.LastName, src => src.LastName)
                .Map(dest => dest.NumberPhone, src => src.NumberPhone)
                .Map(dest => dest.Email, src => src.Email)
                .Ignore(dest => dest.Image, src => src.Image);

            TypeAdapterConfig<Domain.Entities.User, UserDto>.NewConfig()
                .Map(dest => dest.FirstName, src => src.FirstName)
                .Map(dest => dest.LastName, src => src.LastName)
                .Map(dest => dest.Email, src => src.Email);

            TypeAdapterConfig<UserDto, User>.NewConfig()
                .Map(dest => dest.FirstName, src => src.FirstName)
                .Map(dest => dest.LastName, src => src.LastName)
                .Map(dest => dest.Email, src => src.Email)
                .Ignore(dest => dest.Image, src => src.Image);
        }
    }
}
