namespace KooliProjekt.WpfApp.Api
{
    public class Result
    {
        public string Error { get; set; }

        public bool HasError
        {
            get
            {
                return !string.IsNullOrEmpty(Error);
            }
        }
    }

    public class Result<T> : Result
    {
        public T Value { get; set; }
    }
}
