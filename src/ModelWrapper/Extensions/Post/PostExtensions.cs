namespace ModelWrapper.Extensions.Post
{
	/// <summary>
	/// Class that extends post functionality into ModelWrapper
	/// </summary>
	public static class PostExtensions
    {
        /// <summary>
        /// Mathod that extends IWrapRequest<T> allowing use post
        /// </summary>
        /// <typeparam name="TModel">Generic type of the entity</typeparam>
        /// <param name="request">Self IWrapRequest<T> instance</param>
        /// <returns>New entity</returns>
        public static TModel Post<TModel>(
            this WrapRequest<TModel> request
        ) where TModel : class
        {
            return request.CreateOrUpdateModelAndSetOnRequest(null, false);
        }
    }
}
