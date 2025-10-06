using AutoMapper;
using Library.Application.Abstractions.Persistence;
using Library.Application.Dtos.Borrow;
using Library.Application.Features.Borrows.Queries.GetBorrowById.Spec;
using Library.Domain.Entities;
using MediatR;

namespace Library.Application.Features.Borrows.Queries.GetBorrowById;
public sealed class GetBorrowByIdQueryHandler(
    IRepository<Borrow> _borrowRepo,
    IMapper _mapper)
    : IRequestHandler<GetBorrowByIdQuery, BorrowDto>
{
    public async Task<BorrowDto> Handle(GetBorrowByIdQuery request, CancellationToken cancellationToken)
    {
        var borrow = await _borrowRepo.FirstOrDefaultAsync(new GetBorrowByIdWithDetailsSpec(request.Id));
        if (borrow == null)
        {
            throw new KeyNotFoundException("Borrow not found with the given Id");
        }

        return _mapper.Map<BorrowDto>(borrow);
    }
}