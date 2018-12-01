using System;

namespace Example.Models
{
    public class Product
    {

        #region Construction
        public Product()
        {
        }

        #endregion

        #region Column Mappings
        private int _ProductID;
        public virtual int ProductID
        {
            get { return _ProductID; }
            set
            {
                _ProductID = value;
            }
        }

        private int _ClientID;
        public virtual int ClientID
        {
            get { return _ClientID; }
            set
            {
                _ClientID = value;
            }
        }

        private string _ProductNumber;
        public virtual string ProductNumber
        {
            get { return _ProductNumber; }
            set
            {
                _ProductNumber = value;
            }
        }

        private string _ProductName;
        public virtual string ProductName
        {
            get { return _ProductName; }
            set
            {
                _ProductName = value;
            }
        }

        private int? _ProductGroupID;
        public virtual int? ProductGroupID
        {
            get { return _ProductGroupID; }
            set
            {
                _ProductGroupID = value;
            }
        }

        private string _ProductAbbrev;
        public virtual string ProductAbbrev
        {
            get { return _ProductAbbrev; }
            set
            {
                _ProductAbbrev = value;
            }
        }

        private DateTime? _ProductAvailabilityDate;
        public virtual DateTime? ProductAvailabilityDate
        {
            get { return _ProductAvailabilityDate; }
            set
            {
                _ProductAvailabilityDate = value;
            }
        }

        private bool? _SerialNumber;
        public virtual bool? SerialNumber
        {
            get { return _SerialNumber; }
            set
            {
                _SerialNumber = value;
            }
        }

        private int? _Depth;
        public virtual int? Depth
        {
            get { return _Depth; }
            set
            {
                _Depth = value;
            }
        }

        private int? _Width;
        public virtual int? Width
        {
            get { return _Width; }
            set
            {
                _Width = value;
            }
        }

        private int? _Height;
        public virtual int? Height
        {
            get { return _Height; }
            set
            {
                _Height = value;
            }
        }

        private decimal? _Weight;
        public virtual decimal? Weight
        {
            get { return _Weight; }
            set
            {
                _Weight = value;
            }
        }

        private int? _AlertPoint;
        public virtual int? AlertPoint
        {
            get { return _AlertPoint; }
            set
            {
                _AlertPoint = value;
            }
        }

        private int? _Remaining;
        public virtual int? Remaining
        {
            get { return _Remaining; }
            set
            {
                _Remaining = value;
            }
        }

        private decimal? _PurchaseValue;
        public virtual decimal? PurchaseValue
        {
            get { return _PurchaseValue; }
            set
            {
                _PurchaseValue = value;
            }
        }

        private string _Status;
        public virtual string Status
        {
            get { return _Status; }
            set
            {
                _Status = value;
            }
        }

        private DateTime? _StatusUpdated;
        public virtual DateTime? StatusUpdated
        {
            get { return _StatusUpdated; }
            set
            {
                _StatusUpdated = value;
            }
        }

        private string _PackageType;
        public virtual string PackageType
        {
            get { return _PackageType; }
            set
            {
                _PackageType = value;
            }
        }

        private int? _PackageAmount;
        public virtual int? PackageAmount
        {
            get { return _PackageAmount; }
            set
            {
                _PackageAmount = value;
            }
        }

        private string _Description;
        public virtual string Description
        {
            get { return _Description; }
            set
            {
                _Description = value;
            }
        }

        private bool _SalesProduct;
        public virtual bool SalesProduct
        {
            get { return _SalesProduct; }
            set
            {
                _SalesProduct = value;
            }
        }

        private byte? _OutOfStockCode;
        public virtual byte? OutOfStockCode
        {
            get { return _OutOfStockCode; }
            set
            {
                _OutOfStockCode = value;
            }
        }

        private int _CreatedBy;
        public virtual int CreatedBy
        {
            get { return _CreatedBy; }
            set
            {
                _CreatedBy = value;
            }
        }

        private DateTime? _CreatedDateTime;
        public virtual DateTime? CreatedDateTime
        {
            get { return _CreatedDateTime; }
            set
            {
                _CreatedDateTime = value;
            }
        }

        private int? _ApprovedBy;
        public virtual int? ApprovedBy
        {
            get { return _ApprovedBy; }
            set
            {
                _ApprovedBy = value;
            }
        }

        private DateTime? _ApprovedDateTime;
        public virtual DateTime? ApprovedDateTime
        {
            get { return _ApprovedDateTime; }
            set
            {
                _ApprovedDateTime = value;
            }
        }

        private int? _Account;
        public virtual int? Account
        {
            get { return _Account; }
            set
            {
                _Account = value;
            }
        }

        private DateTime? _StartDate;
        public virtual DateTime? StartDate
        {
            get { return _StartDate; }
            set
            {
                _StartDate = value;
            }
        }

        private DateTime? _EndDate;
        public virtual DateTime? EndDate
        {
            get { return _EndDate; }
            set
            {
                _EndDate = value;
            }
        }

        private string _UnitType;
        public virtual string UnitType
        {
            get { return _UnitType; }
            set
            {
                _UnitType = value;
            }
        }

        private string _ISBN;
        public virtual string ISBN
        {
            get { return _ISBN; }
            set
            {
                _ISBN = value;
            }
        }

        #endregion

        public static Product GetXSSUserProduct()
        {
            return new Product
            {
                ProductID = 1,
                ProductNumber = "asdfasdf",
                ProductName = @"<script type=""text/javascript"">
document.getElementById('infodiv').innerHTML += '<p>Note the added image/script from another domain. The url fo this image can be used to send information to another domain...</p>'+
  '<img src=""http://www.google.se/intl/en_com/images/logo_plain.png""/>'</script>",
                Description = @"
<script type=""text/javascript"" src=""http://localhost:8091/Scripts/xss.js""></script>

"
            };
        }


    }
}