using HotelProject.EntityLayer.Concrete;

namespace HotelProject.BusinessLayer.Concrete
{
    public interface IBookingManager
    {
        void TDelete(Booking t);
        Booking TGetByID(int id);
        List<Booking> TGetList();
        void TInsert(Booking t);
        void TUpdate(Booking t);
    }
}