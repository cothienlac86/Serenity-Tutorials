
namespace MovieTutorial.NewsManagement.Entities
{
    using Newtonsoft.Json;
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;
    using Serenity.Data.Mapping;
    using System;
    using System.ComponentModel;
    using System.IO;

    [ConnectionKey("InfoWebAppDb_v2.1"), DisplayName("tbl_PrivateNews"), InstanceName("tbl_PrivateNews"), TwoLevelCached]
    [ReadPermission("Administration")]
    [ModifyPermission("Administration")]
    public sealed class TblPrivateNewsRow : Row, IIdRow, INameRow
    {
        [DisplayName("Id"), Identity]
        public Int32? Id
        {
            get { return Fields.Id[this]; }
            set { Fields.Id[this] = value; }
        }

        [DisplayName("Title"), Size(1000), QuickSearch]
        public String Title
        {
            get { return Fields.Title[this]; }
            set { Fields.Title[this] = value; }
        }

        [DisplayName("News Content")]
        public String NewsContent
        {
            get { return Fields.NewsContent[this]; }
            set { Fields.NewsContent[this] = value; }
        }

        [DisplayName("Address"), Size(1000)]
        public String Address
        {
            get { return Fields.Address[this]; }
            set { Fields.Address[this] = value; }
        }

        [DisplayName("Dientich"), Size(50)]
        public String Dientich
        {
            get { return Fields.Dientich[this]; }
            set { Fields.Dientich[this] = value; }
        }

        [DisplayName("Price"), Size(50)]
        public String Price
        {
            get { return Fields.Price[this]; }
            set { Fields.Price[this] = value; }
        }

        [DisplayName("Phone Number"), Size(50)]
        public String PhoneNumber
        {
            get { return Fields.PhoneNumber[this]; }
            set { Fields.PhoneNumber[this] = value; }
        }

        [DisplayName("Menu Id")]
        public Int32? MenuId
        {
            get { return Fields.MenuId[this]; }
            set { Fields.MenuId[this] = value; }
        }

        [DisplayName("Tinh Thanh Id")]
        public Int32? TinhThanhId
        {
            get { return Fields.TinhThanhId[this]; }
            set { Fields.TinhThanhId[this] = value; }
        }

        [DisplayName("Quan Huyen Id")]
        public Int32? QuanHuyenId
        {
            get { return Fields.QuanHuyenId[this]; }
            set { Fields.QuanHuyenId[this] = value; }
        }

        [DisplayName("User Id")]
        public Int32? UserId
        {
            get { return Fields.UserId[this]; }
            set { Fields.UserId[this] = value; }
        }

        [DisplayName("Org Price"), Column("Org_Price"), Size(10), Scale(3)]
        public Decimal? OrgPrice
        {
            get { return Fields.OrgPrice[this]; }
            set { Fields.OrgPrice[this] = value; }
        }

        [DisplayName("Status")]
        public Int32? Status
        {
            get { return Fields.Status[this]; }
            set { Fields.Status[this] = value; }
        }

        [DisplayName("Create Date")]
        public DateTime? CreateDate
        {
            get { return Fields.CreateDate[this]; }
            set { Fields.CreateDate[this] = value; }
        }

        [DisplayName("Active"), NotNull]
        public Int16? Active
        {
            get { return Fields.Active[this]; }
            set { Fields.Active[this] = value; }
        }

        [DisplayName("Reup"), NotNull]
        public Boolean? Reup
        {
            get { return Fields.Reup[this]; }
            set { Fields.Reup[this] = value; }
        }

        IIdField IIdRow.IdField
        {
            get { return Fields.Id; }
        }

        StringField INameRow.NameField
        {
            get { return Fields.Title; }
        }

        public static readonly RowFields Fields = new RowFields().Init();

        public TblPrivateNewsRow()
            : base(Fields)
        {
        }

        public class RowFields : RowFieldsBase
        {
            public Int32Field Id;
            public StringField Title;
            public StringField NewsContent;
            public StringField Address;
            public StringField Dientich;
            public StringField Price;
            public StringField PhoneNumber;
            public Int32Field MenuId;
            public Int32Field TinhThanhId;
            public Int32Field QuanHuyenId;
            public Int32Field UserId;
            public DecimalField OrgPrice;
            public Int32Field Status;
            public DateTimeField CreateDate;
            public Int16Field Active;
            public BooleanField Reup;

            public RowFields()
                : base("[dbo].[tbl_PrivateNews]")
            {
                LocalTextPrefix = "NewsManagement.TblPrivateNews";
            }
        }
    }
}