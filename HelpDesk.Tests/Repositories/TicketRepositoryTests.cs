using HelpDesk.Api.Data;
using HelpDesk.Api.Models;
using HelpDesk.Api.Repositories;
using Microsoft.EntityFrameworkCore;

namespace HelpDesk.Tests.Repositories
{
    public class TicketRepositoryTests
    {
        private HelpDeskDbContext CreateContext()
        {
            var options = new DbContextOptionsBuilder<HelpDeskDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            return new HelpDeskDbContext(options);
        }

        [Fact]
        public async Task CreateTicketAsync_ShouldCreateTicket()
        {
            using var context = CreateContext();

            var repository = new TicketRepository(context);

            var ticket = new Ticket
            {
                Title = "Test Ticket",
                Description = "Test Description",
                Priority = "High",
                Status = "Open",
                RaisedBy = "Test User",
                CreatedDate = DateTime.Now
            };

            var result = await repository.CreateTicketAsync(ticket);

            Assert.NotNull(result);
            Assert.Equal("Test Ticket", result.Title);
            Assert.Equal(1, await context.Tickets.CountAsync());
        }

        [Fact]
        public async Task GetTicketByIdAsync_ShouldReturnTicket()
        {
            using var context = CreateContext();

            var ticket = new Ticket
            {
                Title = "Existing Ticket",
                Description = "Description",
                Priority = "Medium",
                Status = "Open",
                RaisedBy = "Test User",
                CreatedDate = DateTime.Now
            };

            context.Tickets.Add(ticket);
            await context.SaveChangesAsync();

            var repository = new TicketRepository(context);

            var result = await repository.GetTicketByIdAsync(ticket.Id);

            Assert.NotNull(result);
            Assert.Equal("Existing Ticket", result.Title);
        }

        [Fact]
        public async Task GetAllTicketsAsync_ShouldReturnAllTickets()
        {
            using var context = CreateContext();

            context.Tickets.AddRange(
                new Ticket
                {
                    Title = "Ticket 1",
                    Description = "Description 1",
                    Priority = "Low",
                    Status = "Open",
                    RaisedBy = "User 1",
                    CreatedDate = DateTime.Now
                },
                new Ticket
                {
                    Title = "Ticket 2",
                    Description = "Description 2",
                    Priority = "High",
                    Status = "Closed",
                    RaisedBy = "User 2",
                    CreatedDate = DateTime.Now
                }
            );

            await context.SaveChangesAsync();

            var repository = new TicketRepository(context);

            var result = await repository.GetAllTicketsAsync();

            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task DeleteTicketAsync_ShouldDeleteTicket()
        {
            using var context = CreateContext();

            var ticket = new Ticket
            {
                Title = "Ticket To Delete",
                Description = "Description",
                Priority = "Low",
                Status = "Open",
                RaisedBy = "Test User",
                CreatedDate = DateTime.Now
            };

            context.Tickets.Add(ticket);
            await context.SaveChangesAsync();

            var repository = new TicketRepository(context);

            var result = await repository.DeleteTicketAsync(ticket.Id);

            Assert.True(result);
            Assert.Empty(context.Tickets);
        }

        [Fact]
        public async Task GetTicketsByStatusAsync_ShouldReturnMatchingTickets()
        {
            using var context = CreateContext();

            context.Tickets.AddRange(
                new Ticket
                {
                    Title = "Open Ticket",
                    Description = "Description",
                    Priority = "High",
                    Status = "Open",
                    RaisedBy = "User 1",
                    CreatedDate = DateTime.Now
                },
                new Ticket
                {
                    Title = "Closed Ticket",
                    Description = "Description",
                    Priority = "Low",
                    Status = "Closed",
                    RaisedBy = "User 2",
                    CreatedDate = DateTime.Now
                }
            );

            await context.SaveChangesAsync();

            var repository = new TicketRepository(context);

            var result = await repository.GetTicketsByStatusAsync("Open");

            Assert.Single(result);
            Assert.Equal("Open", result.First().Status);
        }
    }
}