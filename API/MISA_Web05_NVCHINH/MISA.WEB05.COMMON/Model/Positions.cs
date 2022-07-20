using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.WEB05.COMMON.Model
{
    public class Positions:Base
    {
        public Guid PositionId { get; set; }// khoa chinh
        public string? PositionName { get; set; }// ten chuc vu

    }
}
