using HelpDesk.Api.Controllers;
using HelpDesk.Api.Models;
using HelpDesk.Api.Repositories;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace HelpDesk.Tests.Controllers
{
    public class TicketControllerTests
    {
        private readonly Mock<ITicketRepository> _repositoryMock;
        private readonly TicketController _controller;

        public TicketControllerTests()
        {
            _repositoryMock = new Mock<ITicketRepository>();
            _controller = new TicketController(_repositoryMock.Object);
        }

        [Fact]
        public async Task GetAll_ShouldReturnOkResult()
        {
            // Arrange
            var tickets = new List<Ticket>
            {
                new Ticket
                {
                    Id = 1,
                    Title = "Test Ticket",
                    Description = "Test Description",
                    Priority = "High",
                    Status = "Open",
                    RaisedBy = "Test User",
                    CreatedDate = DateTime.Now
                }
            };

            _repositoryMock
                .Setup(r => r.GetAllTicketsAsync())
                .ReturnsAsync(tickets);

            // Act
            var result = await _controller.GetAll();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);

            var returnedTickets = Assert.IsAssignableFrom<IEnumerable<Ticket>>(
                okResult.Value);

            Assert.Single(returnedTickets);
        }

        [Fact]
        public async Task GetById_ShouldReturnOk_WhenTicketExists()
        {
            // Arrange
            var ticket = new Ticket
            {
                Id = 1,
                Title = "Test Ticket",
                Description = "Test Description",
                Priority = "High",
                Status = "Open",
                RaisedBy = "Test User",
                CreatedDate = DateTime.Now
            };

            _repositoryMock
                .Setup(r => r.GetTicketByIdAsync(1))
                .ReturnsAsync(ticket);

            // Act
            var result = await _controller.GetById(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);

            var returnedTicket = Assert.IsType<Ticket>(okResult.Value);

            Assert.Equal(1, returnedTicket.Id);
            Assert.Equal("Test Ticket", returnedTicket.Title);
        }

        [Fact]
        public async Task GetById_ShouldReturnNotFound_WhenTicketDoesNotExist()
        {
            // Arrange
            _repositoryMock
                .Setup(r => r.GetTicketByIdAsync(999))
                .ReturnsAsync((Ticket?)null);

            // Act
            var result = await _controller.GetById(999);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Create_ShouldReturnCreatedAtAction()
        {
            // Arrange
            var ticket = new Ticket
            {
                Id = 1,
                Title = "New Ticket",
                Description = "New Description",
                Priority = "Medium",
                Status = "Open",
                RaisedBy = "Test User",
                CreatedDate = DateTime.Now
            };

            _repositoryMock
                .Setup(r => r.CreateTicketAsync(ticket))
                .ReturnsAsync(ticket);

            // Act
            var result = await _controller.Create(ticket);

            // Assert
            var createdResult =
                Assert.IsType<CreatedAtActionResult>(result);

            Assert.Equal("GetById", createdResult.ActionName);

            var returnedTicket =
                Assert.IsType<Ticket>(createdResult.Value);

            Assert.Equal(1, returnedTicket.Id);
        }

        [Fact]
        public async Task Delete_ShouldReturnNoContent_WhenSuccessful()
        {
            // Arrange
            _repositoryMock
                .Setup(r => r.DeleteTicketAsync(1))
                .ReturnsAsync(true);

            // Act
            var result = await _controller.Delete(1);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task Delete_ShouldReturnNotFound_WhenTicketDoesNotExist()
        {
            // Arrange
            _repositoryMock
                .Setup(r => r.DeleteTicketAsync(999))
                .ReturnsAsync(false);

            // Act
            var result = await _controller.Delete(999);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}