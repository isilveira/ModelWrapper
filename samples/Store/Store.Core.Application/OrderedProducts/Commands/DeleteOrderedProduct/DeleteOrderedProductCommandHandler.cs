﻿using MediatR;
using Microsoft.EntityFrameworkCore;
using Store.Core.Application.Interfaces.Infrastructures.Data;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Store.Core.Application.OrderedProducts.Commands.DeleteOrderedProduct
{
    public class DeleteOrderedProductCommandHandler : IRequestHandler<DeleteOrderedProductCommand, DeleteOrderedProductCommandResponse>
    {
        private IStoreContext Context { get; set; }
        public DeleteOrderedProductCommandHandler(IStoreContext context)
        {
            Context = context;
        }
        public async Task<DeleteOrderedProductCommandResponse> Handle(DeleteOrderedProductCommand request, CancellationToken cancellationToken)
        {
            var id = request.Project(x => x.OrderedProductID);

            var data = await Context.OrderedProducts.SingleOrDefaultAsync(x => x.OrderedProductID == id);

            if (data == null)
                throw new Exception("OrderedProduct not found!");

            Context.OrderedProducts.Remove(data);

            await Context.SaveChangesAsync();

            return new DeleteOrderedProductCommandResponse(request, data, "Successful operation!", 1);
        }
    }
}