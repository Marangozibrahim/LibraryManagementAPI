using AutoMapper;
using Library.Application.Dtos.Category;
using Library.Application.Features.Categories.Commands.AddCategory;
using Library.Domain.Entities;

namespace Library.Application.Mapping;
public class CategoryProfile : Profile
{
    public CategoryProfile()
    {
        CreateMap<AddCategoryCommand, Category>();
        CreateMap<Category, CategoryDto>();
    }
}
