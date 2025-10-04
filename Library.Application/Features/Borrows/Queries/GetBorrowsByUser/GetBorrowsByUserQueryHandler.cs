using AutoMapper;
using Library.Application.Abstractions.Auth;
using Library.Application.Abstractions.Persistence;
using Library.Application.Dtos.Borrow;
using Library.Application.Features.Borrows.Queries.GetBorrowsByUser.Specs;
using Library.Domain.Entities;
using MediatR;

namespace Library.Application.Features.Borrows.Queries.GetBorrowsByUser;

public sealed class GetBorrowsByUserQueryHandler(
    IUnitOfWork _uow,
    ICurrentUserService _currentUserService,
    IMapper _mapper
    ) 
    : IRequestHandler<GetBorrowsByUserQuery, IReadOnlyList<BorrowDto>>
{
    public async Task<IReadOnlyList<BorrowDto>> Handle(GetBorrowsByUserQuery request, CancellationToken cancellationToken)
    {
        var userId = _currentUserService.UserId!;
        var userName = _currentUserService.UserName!;

        var spec = new GetBorrowsByUserSpec(userId.Value);
        var borrows = await _uow.Repository<Borrow>().ListAsync(spec, cancellationToken);

        return _mapper.Map<IReadOnlyList<BorrowDto>>(borrows);
    }
}
