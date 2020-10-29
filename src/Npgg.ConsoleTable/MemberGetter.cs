//source : https://www.codeproject.com/Articles/993798/FieldInfo-PropertyInfo-GetValue-SetValue-Alternati
//lisence : The Code Project Open License(CPOL)

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;


namespace Npgg
{

    public class MemberGetter
    {

        public static Dictionary<string, MemberGetter> GetAssigners(Type type)
        {
            var result = new Dictionary<string, MemberGetter>();

            var types = new List<MemberTypes>(new[] { MemberTypes.Field, MemberTypes.Property });

            foreach (var member in type.GetMembers().Where(b=>types.Contains(b.MemberType)))
            {
                result.Add(member.Name, new MemberGetter(member));
            }

            return result;
        }
        public static Dictionary<string, MemberGetter> GetAssigners<T>()
            => GetAssigners(typeof(T));

        private readonly static MethodInfo sm_valueAssignerMethod
            = typeof(MemberGetter).GetMethod("ValueAssigner", BindingFlags.Static | BindingFlags.NonPublic);

        private static void ValueAssigner<T>(out T dest, T src) => dest = src;

        private readonly Func<object, object> getter;

        private readonly Action<object, object> setter;


        public object GetValue(object targetObject) => getter(targetObject);

        public void SetValue(object targetObject, object memberValue) => setter(targetObject, memberValue);

        public readonly Type DeclaringType;
        public readonly Type ValueType;
        public readonly string MemberName;
        public MemberGetter(MemberInfo memberInfo)
        {
            this.MemberName = memberInfo.Name;
            this.DeclaringType = memberInfo.DeclaringType;
            MemberExpression exMember = null;
            Func<Expression, MemberExpression> getMemberExpression;

            if (memberInfo is FieldInfo fi)
            {
                this.ValueType = fi.FieldType;
                var assignmentMethod = sm_valueAssignerMethod.MakeGenericMethod(fi.FieldType);

                getMemberExpression = _ex => exMember = Expression.Field(_ex, fi);
            }
            else if (memberInfo is PropertyInfo pi)
            {
                this.ValueType = pi.PropertyType;
                var assignmentMethod = pi.GetSetMethod(true);

                getMemberExpression = _ex => exMember = Expression.Property(_ex, pi);
            }
            else
            {
                throw new ArgumentException
                ("The member must be either a Field or a Property, not " + memberInfo.MemberType);
            }

            Init(getMemberExpression
                    , out this.getter
                );

        }

        private void Init(
            Func<Expression, MemberExpression> getMember,
            out Func<object, object> getter)
        {
            var exObjParam = Expression.Parameter(typeof(object), "theObject");
            var exValParam = Expression.Parameter(typeof(object), "theProperty");

            var exObjConverted = Expression.Convert(exObjParam, this.DeclaringType);

            Expression exMember = getMember(exObjConverted);

            Expression getterMember = ValueType.IsValueType ? Expression.Convert(exMember, typeof(object)) : exMember;
            getter = Expression.Lambda<Func<object, object>>(getterMember, exObjParam).Compile();
        }
    }
}