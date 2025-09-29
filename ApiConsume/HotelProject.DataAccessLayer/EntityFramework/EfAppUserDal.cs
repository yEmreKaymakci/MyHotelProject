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
    public class EfAppUserDal : GenericRepository<AppUser>, IAppUserDal
    {
        private readonly Context _context;

        public EfAppUserDal(Context context) : base(context)
        {
            _context = context; // Constructor'dan gelen context'i sakla
        }

        public List<AppUser> UserListWithWorkLocation()
        {
            return _context.Set<AppUser>()
                .Include(x => x.WorkLocation)
                .ToList();
        }

        public List<AppUser> UserListWithWorkLocations()
        {
            return _context.Users.Include(x => x.WorkLocation).ToList();
        }
    }
}
