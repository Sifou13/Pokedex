using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pokedex.Api.Framework
{
    public interface IDateTimeProvider
    {
        DateTime GetDateTimeUtcNow();
    }

    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTime GetDateTimeUtcNow()
        {
            return DateTime.UtcNow;
        }
    }
}
