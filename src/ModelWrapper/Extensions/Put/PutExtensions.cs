namespace ModelWrapper.Extensions.Put
{

	/// <summary>
	/// Class that extends put functionality into ModelWrapper
	/// </summary>
	public static class PutExtensions
    {
        /// <summary>
        /// Mathod that extends IWrapRequest<T> allowing use put
        /// </summary>
        /// <typeparam name="TModel">Generic type of the entity</typeparam>
        /// <param name="request">Self IWrapRequest<T> instance</param>
        /// <param name="model">Entity were data from request will be putted</param>
        /// <returns>Entity updated</returns>
        public static TModel Put<TModel>(
            this WrapRequest<TModel> request,
            TModel model
        ) where TModel : class
		{
			return request.CreateOrUpdateModelAndSetOnRequest(model, false);
		}
    }
}
