using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Practices.Unity;

namespace NREticaret.DI
{
    public interface IContainerAccessor
    {
        IUnityContainer Container { get; }
    }
}