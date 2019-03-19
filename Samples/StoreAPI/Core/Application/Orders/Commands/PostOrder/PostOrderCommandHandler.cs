using MediatR;
using StoreAPI.Core.Application.Interfaces.Contexts;
using StoreAPI.Core.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace StoreAPI.Core.Application.Orders.Commands.PostOrder
{
    public class PostOrderCommandHandler : IRequestHandler<PostOrderCommand, PostOrderCommandResponse>
    {
        private IStoreContext Context { get; set; }
        public PostOrderCommandHandler(IStoreContext context)
        {
            Context = context;
        }
        public async Task<PostOrderCommandResponse> Handle(PostOrderCommand request, CancellationToken cancellationToken)
        {
            var data = request.Post();

            data.RegistrationDate = DateTime.UtcNow;

            await Context.Orders.AddAsync(data);

            await Context.SaveChangesAsync();

            return new PostOrderCommandResponse
            {
                Message = "Successful operation!",
                Request = request.AsDictionary(ModelWrapper.EnumProperties.AllWithoutKeys),
                Data = new PostOrderCommandResposeDTO
                {
                    OrderID = data.OrderID,
                    CustomerID = data.CustomerID,
                    RegistrationDate = data.RegistrationDate,
                    ConfirmationDate = data.ConfirmationDate,
                    CancellationDate = data.CancellationDate
                }
            };
        }
    }
}
