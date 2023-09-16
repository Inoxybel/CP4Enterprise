namespace CP4Enterprise.CrossCutting.Helpers
{
    public class Result<T>
    {
        public bool Success { get; set; }
        public T Data { get; set; }
        public string ErrorMessage { get; set; }
    }
}
