using SB.Entities;
using System;
using static SB.Entities.Enums;

namespace SB.Resources
{
    public class GenericUtils
    {
        public static bool ExistEnumTipo(Type enumType, int value) => Enum.IsDefined(typeof(DocumentType).GetNestedType(enumType.Name), value);

        public static DateTime GetDateZone(DateTime incoming, string zoneId = "SA Pacific Standard Time")
        {
            incoming = TimeZoneInfo.ConvertTime(incoming, TimeZoneInfo.FindSystemTimeZoneById(zoneId));
            incoming = incoming.AddTicks(-(incoming.Ticks % TimeSpan.TicksPerSecond));
            return incoming;
        }

        public static Pagination ValidatePagination(Pagination param)
        {
            if (param.PageSize < 0 || param.PageSize > 100) param.PageSize = 1;
            if (param.Page <= 0) param.Page = 1;
            return param;
        }
    }
}
