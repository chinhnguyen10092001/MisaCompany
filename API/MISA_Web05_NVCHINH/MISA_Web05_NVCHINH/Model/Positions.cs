using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA_Web05_NVCHINH.Model
{
    public class Positions:Base
    {
        public Positions()
        {
            this.PositionId = Guid.NewGuid();
        }
        public Guid PositionId { get; set; }// khoa chinh
        public string? PositionName { get; set; }// ten chuc vu
       

    }
}
