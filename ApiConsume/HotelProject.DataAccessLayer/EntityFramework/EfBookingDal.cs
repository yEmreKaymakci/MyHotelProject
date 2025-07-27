using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelProject.DataAccessLayer.Abstract;
using HotelProject.DataAccessLayer.Concrete;
using HotelProject.DataAccessLayer.Repositories;
using HotelProject.EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;

namespace HotelProject.DataAccessLayer.EntityFramework
{
    public class EfBookingDal : GenericRepository<Booking>, IBookingDal
    {
        private readonly Context _context;
        public EfBookingDal(Context context) : base(context)
        {
            _context = context;
        }

        public void BookingStatusChangeApproved(Booking booking)
        {
            var values = _context.Bookings.FirstOrDefault(x => x.BookingID == booking.BookingID);
            if (values != null)
            {
                values.Status = "Onaylandı";
                _context.SaveChanges();
            }
        }

        public void BookingStatusChangeApproved2(int id)
        {
            var booking = _context.Bookings.Find(id);
            if (booking != null)
            {
                booking.Status = "Onaylandı";
                _context.SaveChanges();
            }
        }

        // Insert metodunu kaldırın, GenericRepository'deki kullanılsın
    }
}