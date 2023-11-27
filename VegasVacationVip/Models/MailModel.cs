using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VegasVacationVip.Models
{
    public class LogicModel
    {
        public string Smtp { get; set; } = "mx.serverpipe.com";
        public int Port { get; set; } = 25;
        public bool IsEnableSsl { get; set; } = true;
        public bool IsUseDefaultCredentials { get; set; }
        public string Login { get; set; } = "relay";
        public string Password { get; set; } = "R2020r$";
        public string FromName { get; set; } = "relay@serverpipe.com";
        public string FromEmail { get; set; } = "relay@serverpipe.com";
        public string ToEmail { get; set; } = "";
        public string Subject { get; set; } = "";
        public string Body { get; set; } = "";
    }
    public class ContactUsModel
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Request { get; set; }
    }

    public class BuyNowModel
    {
        public string? fname { get; set; }
        public string? lname { get; set; }
        public string? sfname { get; set; }
        public string? slname { get; set; }
        public string? address { get; set; }
        public string? city { get; set; }
        public string? state { get; set; }
        public string? zipcode { get; set; }
        public string? country { get; set; }
        public string? phone { get; set; }
        public string? email { get; set; }
        public string? chname { get; set; }
        public string? ctype { get; set; }
        public string? cnumber { get; set; }
        public string? xmonth { get; set; }
        public string? xyear { get; set; }
        public string? ccvv { get; set; }
    }
}
