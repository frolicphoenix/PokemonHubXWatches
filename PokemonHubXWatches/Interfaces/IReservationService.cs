using PokemonHubXWatches.Models;

namespace PokemonHubXWatches.Interfaces
{
    public interface IReservationService
    {
        Task<IEnumerable<Reservation>> GetAllReservationsAsync();
        Task<Reservation?> GetReservationByIdAsync(int id);
        Task<Reservation> CreateReservationAsync(Reservation reservation);
        Task<Reservation?> UpdateReservationAsync(int id, Reservation reservation);
        Task<bool> DeleteReservationAsync(int id);
        Task<IEnumerable<Reservation>> GetReservationsByUserIdAsync(int userId);
        Task<IEnumerable<Reservation>> GetReservationsByWatchIdAsync(int watchId);
        Task<bool> ValidateReservationExistsAsync(int id);
        Task<bool> IsWatchAvailableForPeriodAsync(int watchId, DateTime startDate, DateTime endDate, int? excludeReservationId = null);
    }
}