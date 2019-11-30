using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SamayaPatra.Common
{
    public class Vehicle
    {
        public int VehicleID { get; set; }
        [Required(ErrorMessage = "Vehicle No is required")]
        public string VehicleNo { get; set; }
        [Required(ErrorMessage = "Model No is required")]
        public string ModelNo { get; set; }
        [Required(ErrorMessage = "Color is required")]
        public string Color { get; set; }
        [Required(ErrorMessage = "Passenger Capacity is required")]
        [Range(0,1000)]
        public int PassengerCapacity { get; set; }
        [Required(ErrorMessage = "Cubic Capacity is required")]
        public decimal CubicCapacity { get; set; }
        [Required(ErrorMessage = "Purchase Date is required")]
        [DataType(DataType.Date, ErrorMessage = "Invalid Date Selected. Please select valid date.")]
        public DateTime PurchasedDate { get; set; }
        public string OwnerName { get; set; }
        public string OwnerContact { get; set; }
        public string OwnerAddress { get; set; }
        [Required(ErrorMessage = "Driver name is required")]
        public string DriverName { get; set; }
        [Required(ErrorMessage = "Driver contact is required")]
        public string DriverContact { get; set; }
        public string DriverAdddress { get; set; }
        public bool IsActive { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
