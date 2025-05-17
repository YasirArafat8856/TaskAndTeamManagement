namespace TaskAndTeamManagement.Infrascture.Utility
{
    public class ApiResponse
    {
        public ApiResponse(int statusCode, string message = null)
        {
            StatusCode = statusCode;
            Message = message ?? GetDefaultMessageForStatusCode(statusCode); // coalescing operator if it gets null
        }
        public int StatusCode { get; set; }
        public string Message { get; set; }

        private string GetDefaultMessageForStatusCode(int statusCode)
        {
            return statusCode switch
            {
                200 => "Data created successfully!",
                400 => "Invalid input. Please provide a valid data.", //Bad Request
                401 => "You aren't authorized to access this resource.", // Unauthorized
                404 => "The requested resource couldn't be found.", // Not Found
                500 => "An internal server error occurred. Please try again later.",
                _ => null
            };
        }
    }
}
