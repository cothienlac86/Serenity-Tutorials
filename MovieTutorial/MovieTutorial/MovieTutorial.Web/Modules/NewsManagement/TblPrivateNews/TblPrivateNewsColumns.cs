﻿
namespace MovieTutorial.NewsManagement.Columns
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [ColumnsScript("NewsManagement.TblPrivateNews")]
    [BasedOnRow(typeof(Entities.TblPrivateNewsRow))]
    public class TblPrivateNewsColumns
    {
        [EditLink, DisplayName("Db.Shared.RecordId"), AlignRight]
        public Int32 Id { get; set; }
        [EditLink]
        public String Title { get; set; }
        public String NewsContent { get; set; }
        public String Address { get; set; }
        public String Dientich { get; set; }
        public String Price { get; set; }
        public String PhoneNumber { get; set; }
        public Int32 MenuId { get; set; }
        public Int32 TinhThanhId { get; set; }
        public Int32 QuanHuyenId { get; set; }
        public Int32 UserId { get; set; }
        public Decimal OrgPrice { get; set; }
        public Int32 Status { get; set; }
        public DateTime CreateDate { get; set; }
        public Int16 Active { get; set; }
        public Boolean Reup { get; set; }
    }
}