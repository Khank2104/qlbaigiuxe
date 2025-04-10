using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qlbaigiuxe
{
    public class ParkingLot
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Capacity must be non-negative")]
        public int Capacity { get; set; }

    }
}
