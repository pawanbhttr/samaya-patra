using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SamayaPatra.Common
{
    public class CheckPoint
    {
        public int CheckPointID { get; set; }
        [Required(ErrorMessage = "Check Point Name is required")]
        public string CheckPointName { get; set; }
        public bool IsActive { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
