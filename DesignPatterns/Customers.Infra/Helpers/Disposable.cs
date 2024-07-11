using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Customers.Infra.Helpers
{
    public abstract class Disposable : IDisposable
    {
        private volatile int counter;

        ~Disposable()
        {
            SafeDispose(false);
        }

        public void Dispose()
        {
            SafeDispose(true);

            GC.SuppressFinalize(this);
        }

        private void SafeDispose(bool disposing)
        {
            if(Interlocked.Increment(ref counter) == 1)
            {
                CleanUnManagedResources(disposing);
            }
        }

        protected abstract void CleanUnManagedResources(bool disposing);
    }
}
