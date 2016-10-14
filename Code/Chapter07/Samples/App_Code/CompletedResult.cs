using System;
using System.Threading;

//
// CompletedResult is an IAsyncResult that indicates the action was completely synchronously.
// Used when a conditional async operation is not executed in a method that requires
// an IAsyncResult to be returned.
//
namespace Samples
{
    public class CompletedResult : IAsyncResult, IDisposable
    {
        private bool _isDisposed;
        private ManualResetEvent _waitHandle;

        private CompletedResult(object state)
        {
            this.AsyncState = state;
            this._waitHandle = new ManualResetEvent(true);
        }

        public void Complete(AsyncCallback callback)
        {
            if (callback != null)
                callback(this);
        }

        public WaitHandle AsyncWaitHandle
        {
            get { return this._waitHandle; }
        }
        public bool CompletedSynchronously { get { return true; } }
        public bool IsCompleted { get { return true; } }
        public object AsyncState { get; set; }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this._isDisposed)
            {
                if (disposing)
                {
                    if (this._waitHandle != null)
                        this._waitHandle.Close();
                }
                this._waitHandle = null;
                this._isDisposed = true;
            }
        }

        ~CompletedResult()
        {
            Dispose(false);
        }

        public static CompletedResult Create(object state, AsyncCallback callback)
        {
            CompletedResult cr = new CompletedResult(state);
            cr.Complete(callback);
            return cr;
        }
    }
}