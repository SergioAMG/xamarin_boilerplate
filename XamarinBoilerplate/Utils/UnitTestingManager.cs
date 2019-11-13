using System;
using System.Linq;

namespace XamarinBoilerplate.Utils
{
    public static class UnitTestingManager
    {
        public static readonly bool IsRunningFromNUnit = AppDomain.CurrentDomain.GetAssemblies().Any(a => a.FullName.ToLowerInvariant().StartsWith("nunit.framework"));
    }
}
