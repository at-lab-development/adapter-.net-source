using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace TMNAdapter.Core.Tracking.Attributes
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
                    attribute = (TAttribute)attributeObject;
                    break;
                }
            }

            return attribute;
        }

        /// <summary>
        /// Looks for attribute by it's type, supplied with class type and method name
        /// </summary>
        /// <param name="testClassType">Type of class, where target method is contained</param>
        /// <param name="methodName">Name of target method</param>
        /// <typeparam name="TAttribute">Type of target attribute</typeparam>
        /// <returns>Returns attribute instance or null if not found</returns>
        public static TAttribute GetAttributeByMethodName<TAttribute>(Type testClassType, string methodName)
            where TAttribute : Attribute 
        {
            MethodInfo methodInfo = testClassType.GetMethod(methodName);

            if (methodInfo == null)
            {
                return null;
            }

            return (TAttribute) methodInfo.GetCustomAttribute(typeof(TAttribute), true);
        }
    }
}
