using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApp.GCommon
{
    public static class ApplicationConstants
    {
        public const string AppDateFormat = "yyyy-MM-dd";
        public const string NoImageUrl = "no-image.jpg";
        public const string IsDeletedPropertyName = "IsDeleted";
        public const string PriceSqlType = "decimal(18,6)";
        public const string AccessDeniedPath = "/Home/AccessDenied";
        public const string ManagerAuthCookie = "ManagerAuth";
    }
}
