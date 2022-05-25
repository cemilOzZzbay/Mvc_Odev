namespace AppCore.Business.Models.Bases
{
    public interface IResultData<out TResultType>
    {
        TResultType Data { get; }
    }
}
