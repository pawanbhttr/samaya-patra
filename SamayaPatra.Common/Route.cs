using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SamayaPatra.Common
{
    public class Route
    {
        public Route()
        {
            CheckPoints = new List<RouteCheckPoint>();
        }
        public int RouteID { get; set; }
        [Required(ErrorMessage = "Route Name is required")]
        public string RouteName { get; set; }
        [Required(ErrorMessage = "Total KM is required")]
        public decimal TotalKM { get; set; }
        public bool IsActive { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public List<RouteCheckPoint> CheckPoints { get; set; }
    }
}
