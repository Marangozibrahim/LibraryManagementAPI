using AutoMapper;
using Library.Application.Dtos.Author;
using Library.Application.Features.Authors.Commands.AddAuthor;
using Library.Domain.Entities;

namespace Library.Application.Mapping;

public class AuthorProfile : Profile
{
    public AuthorProfile()
    {
        CreateMap<AddAuthorCommand, Author>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());
        CreateMap<Author, AuthorDto>();
    }
}
