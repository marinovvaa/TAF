using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoFramework
{
    /// <summary>
    /// Base class for all test application classes.
    /// </summary>
    public abstract class AppBase
    {
        public IBrowserSession Session { get; set; }    

        public AppBase (IBrowserSession session)
        {
            Session = session;
        }
    }
}
