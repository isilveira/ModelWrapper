﻿using MediatR;
using ModelWrapper.Extensions.Post;
using ModelWrapper.Extensions.Select;
using Store.Core.Application.Interfaces.Infrastructures.Data;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Store.Core.Application.Products.Commands.PostProduct
{
    public class PostProductCommandHandler : IRequestHandler<PostProductCommand, PostProductCommandResponse>
    {
        private IStoreContext Context { get; set; }
        public PostProductCommandHandler(IStoreContext context)
        {
            Context = context;
        }
        public async Task<PostProductCommandResponse> Handle(PostProductCommand request, CancellationToken cancellationToken)
        {
            var data = request.Post();

            await Context.Products.AddAsync(data);

            data.RegistrationDate = DateTime.UtcNow;

            await Context.SaveChangesAsync();

            return new PostProductCommandResponse(request, data.Select(request), "Successful operation!", 1);
        }
    }
}