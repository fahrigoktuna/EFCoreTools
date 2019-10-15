using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace EFCoreTools.UnitTesting.Helpers
{
    internal static class BenchmarkHelper
    {
        static Stopwatch _timer = new Stopwatch();
        internal static void StartBenchMark()
        {
            _timer.Start();
        }

        internal static void EndBenchMark(string type, [System.Runtime.CompilerServices.CallerMemberName] string testClassMethodName = "",
            [System.Runtime.CompilerServices.CallerFilePath] string sourceFilePath = "")
        {

            _timer.Stop();
            var elapsed = _timer.ElapsedMilliseconds;
            _timer.Reset();
            Console.WriteLine("{0} ELAPSED TIME : {1}", type, elapsed);

            var testClassName = Path.GetFileNameWithoutExtension(sourceFilePath);

            var assembly = Assembly.GetExecutingAssembly();

            var classType = assembly.GetTypes()
                .First(t => t.Name == testClassName);

            var repositoryBaseName = classType.Namespace.Split('.')[classType.Namespace.Split('.').Length - 1];

            //Do your own save results to anywhere.

        }
    }
}
