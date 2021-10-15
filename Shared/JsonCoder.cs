using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doomain.Shared
{
    /// <summary>
    ///   <br />
    /// </summary>
    public class ByteCoder : ICoder, IDisposable
    {
        private MemoryStream ms;
        private bool disposedValue;


        /// <inheritdoc/>
        public ICoder Encode(object obj)
        {
            if (ms == null) Start();

            switch (obj)
            {
                case string text:
                    ms.Write(Encoding.UTF8.GetBytes(text));
                    break;
                case Guid id:
                    ms.Write(id.ToByteArray());
                    break;
                default:
                    throw new NotImplementedException();
            }
            return this;
        }

        /// <inheritdoc/>
        public byte[] Finilize()
        {
            var result = ms.ToArray();
            ms.Close();
            ms.Dispose();
            ms = null;
            return result;
        }

        private void Start()
        {
            ms = new MemoryStream();
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    if (ms != null)
                        ms.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
