using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieTutorial.Models
{
    //public class PrivateNewsPageViewModel
    //{
    //    public string Message { get; set; }
    //    public List<PrivateNewsModel> PrivateNewsList { get; set; }
    //}

    public class PrivateNewsModel
    {
        public PrivateNewsModel()
        {
            Id = 0;
            Status = 0;
            Title = String.Empty;
            Address = String.Empty;
            Dientich = String.Empty;
            Price = String.Empty;
            PhoneNumer = String.Empty;
            NewsContent = String.Empty;
            TinhThanhId = 0;
            QuanHuyenId = 0;
            MenuId = 0;
            StartDate = DateTime.Now.AddMonths(-2).ToLocalTime();
            EndDate = DateTime.Now.AddDays(1).ToLocalTime();
            Datetime = DateTime.Now.ToLocalTime();
            Number = 0;
            IsSelected = false;
            IsReUp = false;
            listPrivateNews = new List<PrivateNewsModel>();
            listAreaChild = new List<AreaModel>();
        }

        #region Propeties

        public bool IsReUp { get; set; }

        public bool IsSelected { get; set; }

        /// <summary>
        /// Number
        /// </summary>
        public int Number { get; set; }

        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// TinhThanhId
        /// </summary>
        public int MenuId { get; set; }

        /// <summary>
        /// TinhThanhId
        /// </summary>
        public int TinhThanhId { get; set; }

        /// <summary>
        /// TinhThanhId
        /// </summary>
        public int QuanHuyenId { get; set; }

        /// <summary>
        /// Status
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// Title
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Address
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Dientich
        /// </summary>
        public string Dientich { get; set; }

        /// <summary>
        /// Price
        /// </summary>
        public string Price { get; set; }

        /// <summary>
        /// Phone Number
        /// </summary>
        public string PhoneNumer { get; set; }

        /// <summary>
        /// News Content
        /// </summary>
        public string NewsContent { get; set; }

        /// <summary>
        /// Datetime
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Datetime
        /// </summary
        public DateTime EndDate { get; set; }

        /// <summary>
        ///
        /// </summary>
        public DateTime Datetime { get; set; }

        /// <summary>
        /// Get news private
        /// </summary>
        public List<PrivateNewsModel> listPrivateNews { get; set; }

        public List<AreaModel> listAreaChild { get; set; }
        #endregion
    }

}