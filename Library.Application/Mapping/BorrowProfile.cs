using AutoMapper;
using Library.Application.Dtos.Borrow;
using Library.Domain.Entities;

namespace Library.Application.Mapping;

public class BorrowProfile : Profile
{
    public BorrowProfile()
    {
        CreateMap<Borrow, BorrowDto>()
            .ForMember(dest => dest.BookName,
                opt => opt.MapFrom(src => src.Book.Title))
            .ForMember(dest => dest.BorrowStatus,
                opt => opt.MapFrom(src => src.Status));
    }
}
