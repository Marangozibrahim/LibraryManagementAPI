using MediatR;

namespace Library.Application.Features.Categories.Commands.AddCategory;
public sealed record AddCategoryCommand(string Name) : IRequest<Guid>
{
}
