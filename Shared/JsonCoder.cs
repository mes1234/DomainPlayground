using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    /// <summary>
    ///   <br />
    /// </summary>
    public class JsonCoder : ICoder
    {
        /// <inheritdoc/>
        public byte[] GetBytes(object obj)
        {
            if (obj is IEntity entity)
            {
                return entity.Id.ToByteArray();
            }

            throw new NotImplementedException();
        }
    }
}
