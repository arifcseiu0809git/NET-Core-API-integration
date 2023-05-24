using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.Common.Enums
{
    public enum SpTypeEnum
    {
        OldType = 1,
        NewType = 2
    }
    public enum executionOutEnum
    {
        withOutAnyParameter = 0,
        WithParameter = 1,
        WithOutParameter = 2,
        ForDataTableRefCursor = 3,
        ForOutputParameter = 4
    }
}
