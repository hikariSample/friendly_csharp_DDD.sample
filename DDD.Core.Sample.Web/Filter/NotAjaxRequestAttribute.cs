using System;

namespace DDD.Core.Sample.Web.Filter
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class NotAjaxRequestAttribute : Attribute
    {

    }
}