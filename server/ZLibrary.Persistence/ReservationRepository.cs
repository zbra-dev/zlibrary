using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZLibrary.Model;
using Microsoft.EntityFrameworkCore;

namespace ZLibrary.Persistence
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly ZLibraryContext context;

        public ReservationRepository(ZLibraryContext context)
        {
            this.context = context;
        }
        public async Task<IList<Reservation>> FindAll()
        {
            return await context.Reservations
                 .Include(reservation => reservation.User)
                 .Include(reservation => reservation.Reason)
                 .ToListAsync();
        }

        public async Task<IList<Reservation>> FindByBookId(long BookId)
        {
            return await context.Reservations.Where(r => r.BookId == BookId)
                .Include(reservation => reservation.User)
                .Include(reservation => reservation.Reason)
                .ToListAsync();
        }

        public async Task<Reservation> FindById(long id)
        {
            return await context.Reservations
                .Include(reservation => reservation.User)
                .Include(reservation => reservation.Reason)
                .SingleOrDefaultAsync(r => r.Id == id);
        }

        public async Task<IList<Reservation>> FindByUserId(long userId)
        {
            return await context.Reservations.Where(r => r.User.Id == userId)
                .Include(reservation => reservation.User)
                .Include(reservation => reservation.Reason)
                .ToListAsync();
        }

        public async Task Update(Reservation reservation)
        {
            context.Reservations.Update(reservation);
            await context.SaveChangesAsync();
        }

        public async Task<Reservation> Save(Reservation reservation)
        {
            await context.Reservations.AddAsync(reservation);
            await context.SaveChangesAsync();
            await context.Entry(reservation).ReloadAsync();
            return reservation;
        }

    }
}