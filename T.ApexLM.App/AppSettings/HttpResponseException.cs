namespace T.ApexLM.App.AppSettings
{
    [Serializable]
    public class HttpResponseException : Exception
    {
        public HttpResponseException()
        {
        }

        public HttpResponseException(string message) : base(message)
        {
        }

        public HttpResponseException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
