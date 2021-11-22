using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WkApi.Domain.Common
{
    /// <summary>
    /// Gender enum
    /// </summary>
    [Flags]
    public enum Gender
    {
        NotToSay = 0,
        Male = 1,
        Female = 2
    }
}
