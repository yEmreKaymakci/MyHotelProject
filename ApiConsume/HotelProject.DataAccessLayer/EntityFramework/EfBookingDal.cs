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

        public void BookingStatusChangeApproved3(int id)
        {
            var values = _context.Bookings.Find(id);
            values.Status = "Onaylandı";
            _context.SaveChanges();
        }

        public void BookingStatusChangeCancel(int id)
        {
            var values = _context.Bookings.Find(id);
            values.Status = "İptal Edildi";
            _context.SaveChanges();
        }

        public void BookingStatusChangeWait(int id)
        {
            var values = _context.Bookings.Find(id);
            values.Status = "Müşteri Aranacak";
            _context.SaveChanges();
        }

        public int GetBookingCount()
        {
            return _context.Bookings.Count();
        }

        public List<Booking> Last6Bookings()
        {
            return _context.Bookings.OrderByDescending(x => x.BookingID).Take(6).ToList();
        }

        // Insert metodunu kaldırın, GenericRepository'deki kullanılsın
    }
}