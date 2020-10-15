using SFA.DAS.Location.Domain.ImportTypes;
using System;
using System.Collections.Generic;
using System.Text;

namespace SFA.DAS.Location.Domain.Models
{
    public class AdminDistrictData
    {
        public string Outcode { get; set; }
        public double Long { get; set; }
        public double Lat { get; set; }
        public string[] AdminDistrict { get; set; }

        public static implicit operator AdminDistrictData(PostcodesLocationApiItem source)
        {
            return new AdminDistrictData
            {
                Outcode = source.Outcode,
                Lat = source.Lat,
                Long = source.Long,
                AdminDistrict = source.AdminDistrict
            };
        }
    }
}
