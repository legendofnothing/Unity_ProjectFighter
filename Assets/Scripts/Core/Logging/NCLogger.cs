// #define DEBUG_ENABLED

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;

namespace Core.Logging {
    public enum LogLevel {
        INFO,
        WARNING,
        ERROR
    }
    
    public static class NCLogger {

        private static Dictionary<LogLevel, string> _colors = new Dictionary<LogLevel, string>() {
            {LogLevel.INFO, "white"},
            {LogLevel.WARNING, "orange"},
            {LogLevel.ERROR, "red"},
        };

        public static void Log(object message, LogLevel level = LogLevel.INFO) {
#if DEBUG_ENABLED
            string content = $"<color={_colors[level]}>[{GetClassName()}]</color> {message}";
            UnityEngine.Debug.Log(content);
#endif
        }

        private static string GetClassName() {
            string name;
            Type type;
            int frames = 2;
            do {
                MethodBase method = new StackFrame(frames, false).GetMethod();
                type = method.DeclaringType;
                if (type == null) return method.Name;
                frames++;
                name = type.FullName;
            } while (type.Module.Name.Equals("mscorlib.dll", StringComparison.OrdinalIgnoreCase));
            return name;
        }
    }
}