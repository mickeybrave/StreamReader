namespace StreamReader.Core
{
    public struct Error
    {
        public string Message { get; set; }

    }
    public struct ReaderResult<T>
    {
        public T Result { get; set; }
        public Error Error { get; set; }
    }
}
