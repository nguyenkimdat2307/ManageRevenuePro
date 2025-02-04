using Microsoft.AspNetCore.Http;

namespace ManageRevenue.Domain.Common
{
    public class Response<T>
    {
        public Response()
        {
            Code = StatusCodes.Status200OK;
        }

        public int Code { get; set; }
        public string Message { get; set; }

        public T Data { get; set; }

        public List<T> DataList { get; set; }
    }
}
