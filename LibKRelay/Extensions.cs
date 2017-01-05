using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static LibKRelay.ClientListener;

namespace LibKRelay
{
    public static class Extensions
    {
        /// <summary>
        /// Safely invokes a delegate and logs errors of applicable.
        /// </summary>
        /// <param name="method">Delegate to be invoked</param>
        /// <param name="arg1">Passed to the delegate</param>
        /// <param name="arg2">Passed to the delegate</param>
        public static void TryDynamicInvoke(this Delegate method, object arg1, object arg2)
        {
            try
            {
                method.DynamicInvoke(arg1, arg2);
            }
            catch (Exception ex)
            {
                LogException(ex);
            }
        }

        /// <summary>
        /// Safely invokes a delegate and logs errors of applicable.
        /// </summary>
        /// <param name="method">Delegate to be invoked</param>
        /// <param name="arg">Passed to the delegate</param>
        public static void TryDynamicInvoke(this Delegate method, object arg)
        {
            try
            {
                method.DynamicInvoke(arg);
            }
            catch (Exception ex)
            {
                LogException(ex);
            }
        }

        private static void LogException(Exception ex)
        {
            MethodBase site = ex.TargetSite;
            string methodName = site == null ? "<null method reference>" : site.Name;
            string className = site == null ? "" : site.ReflectedType.Name;
            ConsoleEx.Error("An exception was thrown at " + className + "." + methodName + ":\n\t" + ex.Message);
        }
    }
}
