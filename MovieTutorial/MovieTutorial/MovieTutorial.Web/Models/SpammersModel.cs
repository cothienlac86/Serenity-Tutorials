using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieTutorial.Models
{
    public class SpammersModel
    {
        public int Id { get; set; }
        public string PhoneNo { get; set; }
        public string Name { get; set; }
        public int Status { get; set; }

        public SpammersModel()
        {
            Id = 0;
            PhoneNo = string.Empty;
            Name = string.Empty;
            Status = 0;
        }

        public List<SpammersModel> ListSpammers { set; get; }
    }
}