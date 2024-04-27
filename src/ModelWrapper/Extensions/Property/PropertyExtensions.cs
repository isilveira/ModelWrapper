using System.Collections.Generic;

namespace ModelWrapper.Extensions.Property
{
	public static class PropertyExtensions
	{
		public static void ClearProperties<TModel>(this WrapRequest<TModel> source)
			where TModel : class
		{
			source.AllProperties.Clear();
		}
		public static List<WrapRequestProperty> GetAllRequestProperties<TModel>(this WrapRequest<TModel> source)
			where TModel : class
		{
			return source.AllProperties;
		}

		public static void SetAllRequestProperties<TModel>(this WrapRequest<TModel> source, List<WrapRequestProperty> wrapRequestProperties)
			where TModel : class
		{
			wrapRequestProperties.ForEach(wrapRequestProperty => source.AllProperties.Add(wrapRequestProperty));
		}
	}
}
