namespace Library.Application.Common
{
    public readonly record struct Result<T>(bool IsSuccess, T? Value, string? Error)
    {
        public static Result<T> Success(T Value) => new( true, Value, null ); 
        public static Result<T> Failure(string error) => new( false, default, error);
    }
}
