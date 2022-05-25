using AppCore.Business.Models.Bases;

namespace AppCore.Business.Models
{
    public class Result
    {
        public bool IsSuccessful { get; }
        public string Message { get; set; }

        public Result(bool isSuccessful, string message)
        {
            IsSuccessful = isSuccessful;
            Message = message;
        }
    }
    public class Result<TResultType> : Result, IResultData<TResultType>
    {
        public TResultType Data { get; }

        public Result(bool isSuccessful, string message, TResultType data) : base(isSuccessful, message)
        {
            Data = data;
        }
    }
}
