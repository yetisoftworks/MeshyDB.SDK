using System;
using System.Collections.Generic;
using System.Text;

namespace MeshyDB.SDK.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]

    public class MeshNameAttribute : Attribute
    {
        internal string Name { get; set; }
        public MeshNameAttribute(string name)
        {
            Name = name;
        }
    }
}
