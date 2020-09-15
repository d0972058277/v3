using System;

namespace V3Lib.NewtonsoftJsonExtensions
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, AllowMultiple = false, Inherited = false)]
    public class AddJsonTypeNameAttribute : System.Attribute { }
}