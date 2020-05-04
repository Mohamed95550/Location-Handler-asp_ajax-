using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SelectCountry.Models
{
    public class MultiSelectVM
    {
        public int countryId { get; set; }
        public string countryName { get; set; }
        public int departementId { get; set; }
        public string departementName { get; set; }
        public int cityId { get; set; }
        public string cityName { get; set; }
    }
}
