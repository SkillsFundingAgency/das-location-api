using SFA.DAS.Location.Domain.ImportTypes;
using System;
using System.Collections.Generic;
using System.Text;

namespace SFA.DAS.Location.Domain.Models
{
    public class PostcodeData
    {
        public string Postcode { get; set; }
        public double Lat { get; set; }
        public double Long { get; set; }
        public string AdminDistrict { get ; set ; }

        public static implicit operator PostcodeData(PostcodesLocationApiItem source)
        {
            if (source == null)
            {
                return null;
            }
            
            return new PostcodeData
            {
                Postcode = source.Postcode,
                Lat = source.Lat,
                Long = source.Long,
                AdminDistrict = source.AdminDistrict
            };
        }
    }
}
