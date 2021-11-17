using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doomain.Shared
{
    /// <summary>
    /// Utility to split data into header and content
    /// </summary>
    public static class DataSplitterUtility
    {

        /// <summary>
        /// Splits message into header and content
        /// </summary>
        /// <param name="msg">raw data</param>
        /// <param name="header">output header</param>
        /// <param name="content">output content</param>
        /// <returns>status if splitting was succesful</returns>
        public static bool RecieveMessage(byte[] msg, out byte[] header, out byte[] content)
        {
            try
            {
                byte[] buf = new byte[msg.Length];

                using var ms = new MemoryStream(msg);

                ms.Read(buf, 0, 4);
                var headerSize = BitConverter.ToInt32(buf.AsSpan()[0..4]);

                ms.Read(buf, 0, 4);
                var contentSize = BitConverter.ToInt32(buf.AsSpan()[0..4]);

                header = new byte[headerSize];
                content = new byte[contentSize];

                ms.Read(header, 0, headerSize);
                ms.Read(content, 0, contentSize);

                return true;
            }
            catch (Exception)
            {
                header = new byte[] { 0 };
                content = new byte[] { 0 };

                return false;
            }
        }
    }
}
