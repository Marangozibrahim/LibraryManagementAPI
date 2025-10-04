using AutoMapper;
using Library.Application.Abstractions.Persistence;
using Library.Domain.Entities;
using MediatR;

namespace Library.Application.Features.Authors.Commands.AddAuthor;

public sealed class AddAuthorCommandHandler(
    IUnitOfWork _uow,
    IMapper _mapper)
    : IRequestHandler<AddAuthorCommand, Guid>
{
    public async Task<Guid> Handle(AddAuthorCommand request, CancellationToken cancellationToken)
    {
        var author = _mapper.Map<Author>(request);

        await _uow.Repository<Author>().AddAsync(author, cancellationToken);
        await _uow.SaveChangesAsync(cancellationToken);

        return author.Id;
    }
}
