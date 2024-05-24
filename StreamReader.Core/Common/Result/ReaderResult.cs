namespace StreamReader.Core
{
    public struct ReaderResult<T>
    {
        public T Result { get; set; }
        public Error Error { get; set; }
    }
}
