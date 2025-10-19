using Microsoft.AspNetCore.Components;

namespace T.ApexLM.App.AppSettings
{
    public abstract class DisposableHelper : ComponentBase, IDisposable, IAsyncDisposable
    {
        private bool _disposed;

        /// <summary>  
        /// Synchronous dispose pattern
        /// </summary>  
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>  
        /// Override this method to dispose managed resources
        /// </summary>  
        protected virtual void DisposeManagedResources() { }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                return;
            }

            if (disposing)
            {
                DisposeManagedResources();
            }

            // Free unmanged resource, if any

            _disposed = true;
        }

        /// <summary>
        /// Asynchronous dispose pattern
        /// </summary>
        public async ValueTask DisposeAsync()
        {
            Dispose(disposing: true);
            await DisposeAsyncCore();
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Override this method to dispose asynchronous resources
        /// </summary>
        protected virtual ValueTask DisposeAsyncCore() => ValueTask.CompletedTask;
    }
}
