using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretApi.Application.Features.Queries.GetAllProduct
{
    public class GetAllProducQueryResponse
    {
        public int TotalCount { get; set; }
        public object Products { get; set; }
    }
}
