namespace Api.Errors
{
    public class ApiResponse
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
      
        public ApiResponse(int statusCode, string mssage = null)
        {
            this.StatusCode = statusCode;
            this.Message = mssage ?? getDefaultMessageForStatusCode(statusCode);
        }
        private string getDefaultMessageForStatusCode(int statusCode)
        {
            return statusCode switch
            {
                400 => "A bad request, you have made",
                401 => "Authorize, you are not",
                404 => "Resourse is not found",
                500 => "Internal server error",
                _ => "UnExpected Error"

            };
        }
    }
}
