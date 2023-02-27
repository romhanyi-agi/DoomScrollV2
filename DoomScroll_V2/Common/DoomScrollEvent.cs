using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoomScroll_V2.Common
{
    public class DoomScrollEvent
    {
        public event Action MyAction;
        public void InvokeAction()
        {
            MyAction?.Invoke();
        }
    }
}
