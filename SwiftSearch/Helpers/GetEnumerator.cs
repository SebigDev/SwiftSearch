using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace SwiftSearch.Helpers
{
    public static class Helper
    {
        public static string GetDescription(this Enum value)
        {
            Type type = value.GetType();

            MemberInfo[] memInfo = type.GetMember(value.ToString());

            if (memInfo != null && memInfo.Length > 0)
            {
                object[] attrs = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);

                if (attrs != null && attrs.Length > 0)
                {
                    return ((DescriptionAttribute)attrs[0]).Description;
                }
            }

            return value.ToString();
        }
        public static MvcHtmlString EnumDropDownListFor<TModel, TEnum>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TEnum>> enumAccessor)
        {
            var propertyInfo = enumAccessor.ToPropertyInfo();
            var enumType = propertyInfo.PropertyType;
            var enumValues = Enum.GetValues(enumType).Cast<Enum>();
            var selectItems = enumValues.Select(s => new SelectListItem
            {
                Text = s.GetDescription(),
                Value = s.ToString()
            });

            return htmlHelper.DropDownListFor(enumAccessor, selectItems);
        }

        public static PropertyInfo ToPropertyInfo(this LambdaExpression expression)
        {
            var memberExpression = expression.Body as MemberExpression;
            return (memberExpression == null) ? null : memberExpression.Member as PropertyInfo;
        }
    }

}