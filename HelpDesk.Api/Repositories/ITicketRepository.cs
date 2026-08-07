using HelpDesk.Api.Models;

namespace HelpDesk.Api.Repositories
{
    public interface ITicketRepository
    {
        Task<IEnumerable<Ticket>> GetAllTicketsAsync();

        Task<Ticket?> GetTicketByIdAsync(int id);

        Task<Ticket> CreateTicketAsync(Ticket ticket);

        Task<bool> UpdateTicketAsync(Ticket ticket);

        Task<bool> DeleteTicketAsync(int id);

        Task<IEnumerable<Ticket>> GetTicketsByStatusAsync(string status);
    }
}