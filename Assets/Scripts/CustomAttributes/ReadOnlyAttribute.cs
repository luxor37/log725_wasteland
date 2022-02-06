using System;
using UnityEngine;

namespace CustomAttributes
{
    /// <summary>
    /// Read Only attribute.
    /// Attribute is use only to mark ReadOnly properties.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public class ReadOnlyAttribute : PropertyAttribute { }

}