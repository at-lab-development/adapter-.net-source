using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMNAdapter.Tracking
{
    /// <summary>
    /// Tracker is responsible for access to the attribute, 
    /// added to method, when it is invoked
    /// </summary>
    public class AnnotationTracker
    {
        /// <summary>
        /// Looks for attribute by it's type in the call stack
        /// </summary>
        /// <typeparam name="TAttribute">Type of target attribute</typeparam>
        /// <returns>Returns attribute instance or null if not found</returns>
        public static TAttribute GetAttributeInCallStack<TAttribute>()
        {
            var stackTrace = new StackTrace();
            TAttribute attribute = default(TAttribute);

            for (var i = 1; i < stackTrace.FrameCount; i++)
            {
                var frame = new StackFrame(i);
                object attributeObject = frame.GetMethod().GetCustomAttributes(true).FirstOrDefault(x => x.GetType() == typeof(TAttribute));

                if (attributeObject != null)
                {
                    attribute = (TAttribute) attributeObject;
                    break;
                }
            }            

            return attribute;
        }
    }
}
