using System;
using System.Linq.Expressions;
using System.Reflection;

namespace EPPlusForMVC
{
    public class ExcelColumnDefinition
    {
        public MemberInfo MemberInfo { get; set; }
        public string Format { get; set; }
        public string Header { get; set; }

        public static ExcelColumnDefinition Create<T>(Expression<Func<T, object>> member, string format = null, string header = null)
        {
            return new ExcelColumnDefinition { MemberInfo = GetMemberInfo(member), Format = format, Header = header };
        }

        private static MemberInfo GetMemberInfo<T>(Expression<Func<T, object>> expression)
        {
            if (expression.Body is MemberExpression)
                return ((MemberExpression)expression.Body).Member;

            return ((MemberExpression) ((UnaryExpression) expression.Body).Operand).Member;
        }
    }
}