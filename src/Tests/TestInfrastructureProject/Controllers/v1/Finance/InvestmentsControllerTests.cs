using BlazorHero.CleanArchitecture.Application.Features.Finance.Investments.Commands.AddEdit;
using BlazorHero.CleanArchitecture.Server.Controllers.v1.Finance;
using FakeItEasy;
using System;
using System.Threading.Tasks;
using Xunit;

namespace TestInfrastructureProject.Controllers.v1.Finance
{
    public class InvestmentsControllerTests
    {


        public InvestmentsControllerTests()
        {

        }

        private InvestmentsController CreateInvestmentsController()
        {
            return new InvestmentsController();
        }

        [Fact]
        public async Task GetAll_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var investmentsController = this.CreateInvestmentsController();
            int pageNumber = 0;
            int pageSize = 0;
            string searchString = null;
            string orderBy = null;

            // Act
            var result = await investmentsController.GetAll(
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
            var investmentsController = this.CreateInvestmentsController();
            long id = 0;

            // Act
            var result = await investmentsController.GetById(
                id);

            // Assert
            Assert.True(false);
        }

        [Fact]
        public async Task Post_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var investmentsController = this.CreateInvestmentsController();
            AddEditInvestmentCommand command = null;

            // Act
            var result = await investmentsController.Post(
                command);

            // Assert
            Assert.True(false);
        }

        [Fact]
        public async Task Delete_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var investmentsController = this.CreateInvestmentsController();
            long id = 0;

            // Act
            var result = await investmentsController.Delete(
                id);

            // Assert
            Assert.True(false);
        }
    }
}
