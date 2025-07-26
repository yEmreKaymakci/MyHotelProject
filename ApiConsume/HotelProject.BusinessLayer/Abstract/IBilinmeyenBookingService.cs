using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelProject.EntityLayer.Concrete;

namespace HotelProject.BusinessLayer.Abstract
{
    public interface IBilinmeyenBookingService
    {
        void TDelete(Booking t);
        Booking TGetByID(int id);
        List<Booking> TGetList();
        void TInsert(Booking t);
        void TUpdate(Booking t);
    }
}
