﻿using ModelWrapper;
using StoreAPI.Core.Domain.Entities;

namespace StoreAPI.Core.Application.Categories.Commands.PostCategory
{
    public class PostCategoryCommandResponse : WrapResponse<Category>
    {
        public PostCategoryCommandResponse(WrapRequest<Category> request, object data, string message = null, long? resultCount = null)
            : base(request, data, message, resultCount)
        {
        }
    }
}