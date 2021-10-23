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
                    // for variable lenght type there is in front size encoded
                    ms.Write(BitConverter.GetBytes(text.Length));
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
        public ICoder Decode(out string obj)
        {
            var buffer = new byte[1024];
            ms.Read(buffer, 0, sizeof(int));
            var length = BitConverter.ToInt32(buffer);
            ms.Read(buffer, 0, length);
            obj = Encoding.UTF8.GetString(buffer, 0, length);
            return this;
        }

        /// <inheritdoc/>
        public ICoder Decode(out Guid obj)
        {
            var buffer = new byte[1024];
            ms.Read(buffer, 0, 16);
            obj = new Guid(buffer[0..16]);

            return this;
        }

        /// <inheritdoc/>
        public ICoder Init(byte[] content)
        {
            ms = new MemoryStream(content);
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

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
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

        private void Start()
        {
            ms = new MemoryStream();
        }
    }
}
