using BlazorHero.CleanArchitecture.Application.Features.Finance.Transactions.Commands.AddEdit;
using BlazorHero.CleanArchitecture.Server.Controllers.v1.Finance;
using FakeItEasy;
using MediatR;
using System;
using System.Threading.Tasks;
using Xunit;

namespace TestInfrastructureProject.Controllers.v1.Finance
{
    public class TransactionsControllerTests
    {

        private readonly IMediator _mediator;

        public TransactionsControllerTests()
        {

        }

        private TransactionsController CreateTransactionsController()
        {
            return new TransactionsController();
        }

        [Fact]
        public async Task GetAll_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var transactionsController = this.CreateTransactionsController();
            int pageNumber = 0;
            int pageSize = 0;
            string searchString = null;
            string orderBy = null;

            // Act
            var result = await transactionsController.GetAll(
                pageNumber,
                pageSize,
                searchString,
                orderBy);

            // Assert
            Assert.True(false);
        }

        [Fact]
        public async Task GetById_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var transactionsController = this.CreateTransactionsController();
            long id = 0;

            // Act
            var result = await transactionsController.GetById(
                id);

            // Assert
            Assert.True(false);
        }

        [Fact]
        public async Task Post_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var transactionsController = this.CreateTransactionsController();
            AddEditTransactionCommand command = null;

            // Act
            var result = await transactionsController.Post(
                command);

            // Assert
            Assert.True(false);
        }

        [Fact]
        public async Task Delete_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var transactionsController = this.CreateTransactionsController();
            long id = 0;

            // Act
            var result = await transactionsController.Delete(
                id);

            // Assert
            Assert.True(false);
        }
    }
}
