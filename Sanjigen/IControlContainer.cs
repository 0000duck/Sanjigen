using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Caltron
{
    public interface IControlContainer
    {
        Control.ControlCollection Controls { get; }
        void Refresh();
    }
}
