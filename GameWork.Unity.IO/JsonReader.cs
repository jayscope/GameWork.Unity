using System.IO;
using GameWork.Core.IO.Readers.PlatformAdaptors;
using GameWork.Core.Models.Interfaces;
using Newtonsoft.Json;

namespace GameWork.Unity.IO
{
	public class JsonReader : IJsonReader, IModelReader
	{
		public TModel ConstructModel<TModel>(Stream stream) where TModel : IModel
		{
			var reader = new StreamReader(stream);
			var str = reader.ReadToEnd();

			return JsonConvert.DeserializeObject<TModel>(str);
		}
	}
}