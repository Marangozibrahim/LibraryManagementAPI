using MediatR;

namespace Library.Application.Features.Categories.Commands.UpdateCategory;
public sealed record UpdateCategoryCommand(Guid Id, string Name) : IRequest<Unit>
{
}
