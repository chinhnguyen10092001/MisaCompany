using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA_Web05_NVCHINH.Model
{
    public class Base
    {
        public DateTime? CreatedDate { get; set; }//Ngay tao
        public string? CreatedBy { get; set; }// Nguoi tao
        public DateTime? ModifiedDate { get; set; }//Ngay sua
        public string? ModifiedBy { get; set; }//Nguoi sua
    }
}
