using BlazorHero.CleanArchitecture.Application.Features.Finance.FinanceAccounts.Commands.AddEdit;
using BlazorHero.CleanArchitecture.Server.Controllers.v1.Finance;
using FakeItEasy;
using System;
using System.Threading.Tasks;
using Xunit;

namespace PlaywrightTests.Controllers.v1.Finance
{
    public class FinanceAccountsControllerTests
    {


        public FinanceAccountsControllerTests()
        {

        }

        private FinanceAccountsController CreateFinanceAccountsController()
        {
            return new FinanceAccountsController();
        }

        [Fact]
        public async Task GetAll_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var financeAccountsController = this.CreateFinanceAccountsController();
            int pageNumber = 0;
            int pageSize = 0;
            string searchString = null;
            string orderBy = null;

            // Act
            var result = await financeAccountsController.GetAll(
                pageNumber,
                pageSize,
                searchString,
                orderBy);

            // Assert
            Assert.True(false);
        }

        [Fact]
        public async Task GetNamesAsync_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var financeAccountsController = this.CreateFinanceAccountsController();

            // Act
            var result = await financeAccountsController.GetNamesAsync();

            // Assert
            Assert.True(false);
        }

        [Fact]
        public async Task GetById_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var financeAccountsController = this.CreateFinanceAccountsController();
            int id = 0;

            // Act
            var result = await financeAccountsController.GetById(
                id);

            // Assert
            Assert.True(false);
        }

        [Fact]
        public async Task Post_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var financeAccountsController = this.CreateFinanceAccountsController();
            AddEditFinanceAccountCommand command = null;

            // Act
            var result = await financeAccountsController.Post(
                command);

            // Assert
            Assert.True(false);
        }

        [Fact]
        public async Task Delete_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var financeAccountsController = this.CreateFinanceAccountsController();
            int id = 0;

            // Act
            var result = await financeAccountsController.Delete(
                id);

            // Assert
            Assert.True(false);
        }
    }
}
