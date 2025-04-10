using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace qlbaigiuxe
{
    public class ParkingLotContext : DbContext
    {
        public ParkingLotContext() : base("name=ParkingLotDb") { }
        public DbSet<ParkingLot> ParkingLots { get; set; }
    }
}
