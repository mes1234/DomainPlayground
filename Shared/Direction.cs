using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doomain.Shared
{
    /// <summary>
    /// Defines communication direction
    /// </summary>
    public class Direction
    {
        /// <summary>
        /// The inbound
        /// </summary>
        public static Direction Inbound = new("in");

        /// <summary>
        /// The outbound
        /// </summary>
        public static Direction Outbound = new("out");

        private readonly string _direction;

        /// <summary>
        /// Initializes a new instance of the <see cref="Direction"/> class.
        /// </summary>
        /// <param name="direction">The direction.</param>
        public Direction(string direction)
        {
            _direction = direction;
        }
    }
}
