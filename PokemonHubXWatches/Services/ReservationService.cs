using Microsoft.EntityFrameworkCore;
using PokemonHubXWatches.Data;
using PokemonHubXWatches.Interfaces;
using PokemonHubXWatches.Models;

namespace PokemonHubXWatches.Services
{
    public class ReservationService : IReservationService
    {
        private readonly ApplicationDbContext _context;

        public ReservationService(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<Reservation>> GetAllReservationsAsync()
        {
            return await _context.Reservations
                .Include(r => r.User)
                .Include(r => r.Watch)
                .ToListAsync();
        }

        public async Task<Reservation?> GetReservationByIdAsync(int id)
        {
            return await _context.Reservations
                .Include(r => r.User)
                .Include(r => r.Watch)
                .FirstOrDefaultAsync(r => r.ReservationID == id);
        }

        public async Task<Reservation> CreateReservationAsync(Reservation reservation)
        {
            if (reservation == null)
                throw new ArgumentNullException(nameof(reservation));

            if (reservation.EndDate <= reservation.ReservationDate)
                throw new InvalidOperationException("End date must be after reservation date.");

            var isAvailable = await IsWatchAvailableForPeriodAsync(
                reservation.WatchID,
                reservation.ReservationDate,
                reservation.EndDate);

            if (!isAvailable)
                throw new InvalidOperationException("Watch is not available for the selected period.");

            reservation.Status = ReservationStatus.Pending;
            reservation.CreatedAt = DateTime.UtcNow;

            _context.Reservations.Add(reservation);
            await _context.SaveChangesAsync();
            return reservation;
        }

        public async Task<Reservation?> UpdateReservationAsync(int id, Reservation reservation)
        {
            if (reservation == null)
                throw new ArgumentNullException(nameof(reservation));

            var existingReservation = await GetReservationByIdAsync(id);
            if (existingReservation == null) return null;

            if (reservation.EndDate <= reservation.ReservationDate)
                throw new InvalidOperationException("End date must be after reservation date.");

            if (reservation.ReservationDate != existingReservation.ReservationDate ||
                reservation.EndDate != existingReservation.EndDate)
            {
                var isAvailable = await IsWatchAvailableForPeriodAsync(
                    reservation.WatchID,
                    reservation.ReservationDate,
                    reservation.EndDate,
                    id);

                if (!isAvailable)
                    throw new InvalidOperationException("Watch is not available for the selected period.");
            }

            existingReservation.ReservationDate = reservation.ReservationDate;
            existingReservation.EndDate = reservation.EndDate;
            existingReservation.Status = reservation.Status;
            existingReservation.TotalPrice = reservation.TotalPrice;
            existingReservation.Notes = reservation.Notes;
            existingReservation.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return existingReservation;
        }

        public async Task<bool> DeleteReservationAsync(int id)
        {
            var reservation = await GetReservationByIdAsync(id);
            if (reservation == null) return false;

            _context.Reservations.Remove(reservation);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Reservation>> GetReservationsByUserIdAsync(int userId)
        {
            return await _context.Reservations
                .Include(r => r.Watch)
                .Where(r => r.UserId == userId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Reservation>> GetReservationsByWatchIdAsync(int watchId)
        {
            return await _context.Reservations
                .Include(r => r.User)
                .Where(r => r.WatchID == watchId)
                .ToListAsync();
        }

        public async Task<bool> ValidateReservationExistsAsync(int id)
        {
            return await _context.Reservations.AnyAsync(r => r.ReservationID == id);
        }

        public async Task<bool> IsWatchAvailableForPeriodAsync(
            int watchId,
            DateTime startDate,
            DateTime endDate,
            int? excludeReservationId = null)
        {
            var watch = await _context.Watches
                .FirstOrDefaultAsync(w => w.WatchID == watchId);

            if (watch == null || !watch.IsAvailable || watch.StockQuantity <= 0)
                return false;

            var query = _context.Reservations
                .Where(r => r.WatchID == watchId &&
                           r.Status != ReservationStatus.Cancelled &&
                           (r.ReservationDate <= endDate && r.EndDate >= startDate));

            if (excludeReservationId.HasValue)
            {
                query = query.Where(r => r.ReservationID != excludeReservationId.Value);
            }

            var conflictingReservation = await query.FirstOrDefaultAsync();
            return conflictingReservation == null;
        }
    }
}