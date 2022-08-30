using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integral.Domain.Services
{
    public interface IDataParserService<TSource, TResult> where TSource : class where TResult : class
    {
        TResult? Parse(TSource item);

        TSource? ParseBack(TResult item);
    }
}
