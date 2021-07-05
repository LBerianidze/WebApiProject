namespace ApiDataLayer.Models.RequestModels
{
	public class Response
	{
		public int ErrorCode { get; set; }
		public string Status { get; set; }
		public string Description { get; set; }
		public string[] Params { get; set; }
	}
}
