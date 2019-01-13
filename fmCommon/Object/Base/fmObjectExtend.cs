using System.Collections.Generic;
using System.Linq;

namespace fmCommon
{
    /// <summary>
    /// fmObject Extend
    /// </summary>
    public static class fmObjectExtend
    {
        public static List<T> Clone<T>(this List<T> list) where T : IfmObject
        {
            return list.Select(item => (T)item.Clone()).ToList();
        }
    }
}
