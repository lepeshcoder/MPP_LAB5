using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class StringFormatter : IStringFormatter
    {
        public static readonly StringFormatter Shared = new StringFormatter();

        private ConcurrentDictionary<string, Func<object, string>> _cache = new();

        public string Format(string template, object target)
        {
            if (!IsTemplateValid(template)) throw new InvalidTemplateException(template);
            int i = 0;
            int BeginPropertyName = 0, EndPropertyName = 0;
            bool Isproperty = false;
            StringBuilder result = new StringBuilder();
            while(i < template.Length)
            {
                var c = template[i];
                switch (c)
                {
                    case '{':
                        {
                            if (template[i + 1] == '{')
                            {
                                result.Append(c);
                                i+=2;
                            }
                            else
                            {
                                BeginPropertyName = i + 1;
                                Isproperty = true;
                                i++;
                            }
                            break;
                        }
                    case '}':
                        {
                            if(Isproperty)
                            {
                                EndPropertyName = i - 1;
                                string propertyName = template.Substring(BeginPropertyName, EndPropertyName - BeginPropertyName + 1);
                                string? property = GetProperty(propertyName, target);
                                if(property != null)
                                {
                                    result.Append(property);
                                    Isproperty = false;
                                }
                                else
                                {
                                    throw new InvalidPropertyNameException(propertyName);
                                }
                                i++;
                            }
                            else
                            {
                                result.Append(c);
                                i+=2;
                            }
                            break;
                        }
                    default:
                        {
                            if (!Isproperty) result.Append(c);
                            i++;
                            break;
                        }
                }
            }
            return result.ToString();
        }

        private bool IsTemplateValid(string template)
        {
            Stack<char> brackets = new Stack<char>();
            foreach (var c in template)
            {
                if (c == '{') brackets.Push(c);
                else if(c == '}')
                {
                    bool Bsucces = brackets.TryPop(out char result);
                    if (!Bsucces) return false;
                }
            }
            return brackets.Count == 0;
        }

        private string? GetProperty(string propertyName, object target)
        {
            string key = $"{target.GetType()}.{propertyName}";
            if (_cache.ContainsKey(key))  return _cache[key](target);
            else
            {
                ParameterExpression param = Expression.Parameter(typeof(object));
                MemberExpression curObjParam;
                try
                {
                    curObjParam = Expression.PropertyOrField(Expression.TypeAs(param, target.GetType()), propertyName);
                }
                catch (Exception ex)
                {
                    return null;
                }
                MethodCallExpression methodCallExpression = Expression.Call(curObjParam, "ToString", null, null);
                Expression<Func<object,string>> lambdaExpression = Expression.Lambda<Func<object, string>>(methodCallExpression, new[] { param });
                Func<object,string> lambda = lambdaExpression.Compile();
                _cache.TryAdd(key, lambda);
                return lambda(target);

            }
        }

    }
}
