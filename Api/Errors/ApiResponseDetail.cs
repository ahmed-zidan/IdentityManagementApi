namespace Api.Errors
{
    public class ApiResponseDetail:ApiResponse
    {
        public string MessageDetails { get; set; }

        public ApiResponseDetail(int statusCode, string mssage,string messageDetails): base(statusCode, mssage)
        {
            this.MessageDetails = messageDetails;
        }
    }
}
