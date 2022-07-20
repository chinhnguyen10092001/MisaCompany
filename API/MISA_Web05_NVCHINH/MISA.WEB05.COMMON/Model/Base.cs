using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.WEB05.COMMON.Model
{
    public class Base
    {
        public DateTime? CreatedDate { get; set; }//Ngày tạo
        public string? CreatedBy { get; set; }// Người tạo
        public DateTime? ModifiedDate { get; set; }//NGày sửa
        public string? ModifiedBy { get; set; }//Người sửa
    }
}
