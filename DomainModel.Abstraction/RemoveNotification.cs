using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace DomainModel.Abstraction
{
    /// <summary>
    /// Notify about removal
    /// </summary>
    public class RemoveNotification : INotification
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        public RemoveNotification(Guid id)
        {

        }
    }


}
