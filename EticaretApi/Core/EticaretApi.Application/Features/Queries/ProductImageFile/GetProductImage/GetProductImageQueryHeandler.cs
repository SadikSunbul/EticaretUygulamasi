using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretApi.Application.Features.Queries.ProductImageFile.GetProductImage
{
    public class GetProductImageQueryHeandler : IRequestHandler<GetProductImageQueryRequest, GetProductImageQueryResponse>
    {
        public Task<GetProductImageQueryResponse> Handle(GetProductImageQueryRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
