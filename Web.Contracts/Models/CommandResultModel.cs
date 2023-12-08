namespace Web.Contracts.Models
{
    public class CommandResultModel<T> : ResponseModel
    {
        public T? Data { get; set; }
    }
}