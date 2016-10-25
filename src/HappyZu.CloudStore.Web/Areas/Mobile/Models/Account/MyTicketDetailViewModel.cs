using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Web;
using HappyZu.CloudStore.Trip.Dto;

namespace HappyZu.CloudStore.Web.Areas.Mobile.Models.Account
{
    public class MyTicketDetailViewModel
    {
        public TicketOrderDto TicektOrder { get; set; }

        public IList<TicketOrderItemDto> OrderItems { get; set; }

        public string GetEnumName<T>(Enum value)
        {
            Type enumType = typeof(T);
            var enumValue = Enum.GetName(enumType, value);
            MemberInfo member = enumType.GetMember(enumValue)[0];

            var attrs = member.GetCustomAttributes(typeof(DisplayAttribute), false);
            var outString = ((DisplayAttribute)attrs[0]).Name;

            if (((DisplayAttribute)attrs[0]).ResourceType != null)
            {
                outString = ((DisplayAttribute)attrs[0]).GetName();
            }

            return outString;
        }
    }
}