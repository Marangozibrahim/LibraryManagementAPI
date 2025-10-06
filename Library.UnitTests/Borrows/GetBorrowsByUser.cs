using AutoMapper;
using Library.Application.Abstractions.Auth;
using Library.Application.Abstractions.Persistence;
using Library.Application.Dtos.Borrow;
using Library.Application.Features.Borrows.Queries.GetBorrowsByUser;
using Library.Application.Features.Borrows.Queries.GetBorrowsByUser.Specs;
using Library.Domain.Entities;
using Moq;

namespace Library.UnitTests.Borrows
{
    public class GetBorrowsByUser
    {
        private readonly Mock<IUnitOfWork> _uowMock;
        private readonly Mock<ICurrentUserService> _currentUserServiceMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly GetBorrowsByUserQueryHandler _handler;

        public GetBorrowsByUser()
        {
            _uowMock = new Mock<IUnitOfWork>();
            _currentUserServiceMock = new Mock<ICurrentUserService>();
            _mapperMock = new Mock<IMapper>();

            _handler = new GetBorrowsByUserQueryHandler(
                _uowMock.Object,
                _currentUserServiceMock.Object,
                _mapperMock.Object
            );
        }

        [Fact]
        public async Task Handle_ShouldReturnAllBorrows_WhenUserIsAdmin()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var bookId = Guid.NewGuid();
            var borrows = new List<Borrow>
            {
                new Borrow(bookId, userId)
            };
            var borrowDtos = new List<BorrowDto> { new BorrowDto() };

            _currentUserServiceMock.Setup(s => s.IsInRole("Admin")).Returns(true);
            _uowMock.Setup(u => u.Repository<Borrow>().ListAsync(It.IsAny<CancellationToken>()))
                    .ReturnsAsync(borrows);
            _mapperMock.Setup(m => m.Map<IReadOnlyList<BorrowDto>>(borrows))
                       .Returns(borrowDtos);

            var query = new GetBorrowsByUserQuery();

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Single(result);
            _uowMock.Verify(u => u.Repository<Borrow>().ListAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldReturnBorrowsByUser_WhenUserIsNotAdmin()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var bookId = Guid.NewGuid();
            var borrows = new List<Borrow>
            {
                new Borrow(bookId, userId)
            };
            var borrowDtos = new List<BorrowDto> { new BorrowDto() };

            _currentUserServiceMock.Setup(s => s.IsInRole("Admin")).Returns(false);
            _currentUserServiceMock.Setup(s => s.UserId).Returns(userId);
            _uowMock.Setup(u => u.Repository<Borrow>().ListAsync(It.IsAny<GetBorrowsByUserSpec>(), It.IsAny<CancellationToken>()))
                    .ReturnsAsync(borrows);
            _mapperMock.Setup(m => m.Map<IReadOnlyList<BorrowDto>>(borrows))
                       .Returns(borrowDtos);

            var query = new GetBorrowsByUserQuery();

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Single(result);
            _uowMock.Verify(u => u.Repository<Borrow>().ListAsync(It.IsAny<GetBorrowsByUserSpec>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldThrowException_WhenUserIdIsNullForNonAdmin()
        {
            // Arrange
            _currentUserServiceMock.Setup(s => s.IsInRole("Admin")).Returns(false);
            _currentUserServiceMock.Setup(s => s.UserId).Returns((Guid?)null);

            var query = new GetBorrowsByUserQuery();

            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => _handler.Handle(query, CancellationToken.None));
        }
    }
}
