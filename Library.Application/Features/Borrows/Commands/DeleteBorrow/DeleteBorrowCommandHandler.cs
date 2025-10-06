using Library.Application.Abstractions.Persistence;
using Library.Domain.Entities;
using MediatR;

namespace Library.Application.Features.Borrows.Commands.DeleteBorrow;
public sealed class DeleteBorrowCommandHandler(
    IUnitOfWork _uow
    ) : IRequestHandler<DeleteBorrowCommand, Unit>
{
    public async Task<Unit> Handle(DeleteBorrowCommand request, CancellationToken cancellationToken)
    {
        var borrowRepo = _uow.Repository<Borrow>();

        var borrow = await borrowRepo.GetByIdAsync(request.Id);
        if (borrow == null) 
            throw new KeyNotFoundException("Borrow record not found");

        borrowRepo.Delete(borrow);
        await _uow.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
