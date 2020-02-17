using System;
using System.Collections.Generic;
using System.Text;

namespace Fourplaces.Model
{
    public sealed class LoginInfo
    {
        private LoginInfo()
        {
        }

        public static LoginInfo Instance { get { return Nested.instance; } }

        private class Nested
        {
            // Explicit static constructor to tell C# compiler
            // not to mark type as beforefieldinit
            static Nested()
            {
            }

            internal static readonly LoginInfo instance = new LoginInfo();
        }
    }
}
