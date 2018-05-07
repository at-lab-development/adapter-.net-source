using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMNAdapter.Tracking
{
    public class AnnotationTracker
    {
        public static T GetAttributeInCallStack<T>()
        {
            var stackTrace = new StackTrace();
            T attribute = default(T);

            for (var i = 1; i < stackTrace.FrameCount; i++)
            {
                var frame = new StackFrame(i);
                object attributeObject = frame.GetMethod().GetCustomAttributes(true).FirstOrDefault(x => x.GetType() == typeof(T));

                if (attributeObject != null)
                {
                    attribute = (T) attributeObject;
                    break;
                }
            }            

            return attribute;
        }
    }
}
