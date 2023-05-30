using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretApi.Application.Features.Commends.CreateProduct
{
    public class CreateProductCommendHandler : IRequestHandler<CreateProductCommendRequest, CreateProductCommendResponse>
    {
        public Task<CreateProductCommendResponse> Handle(CreateProductCommendRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
