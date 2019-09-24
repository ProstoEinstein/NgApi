using System.Linq;
using System.Collections.Generic;
namespace NgApi
{
    public class PaginatedResponse<T>
    {
        public int Total { get; set; }
        public IEnumerable<T> Data { get; set; }
        public PaginatedResponse(IEnumerable<T> data, int i, int len)
        {
            //[1] page, 10 results
            Data = data.Skip((i - 1) * len).Take(len).ToList();
            Total = data.Count();
        }


    }
}