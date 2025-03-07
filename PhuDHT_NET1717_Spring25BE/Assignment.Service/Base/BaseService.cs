using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Service.Base
{
    public interface IBaseService
    {
        int Status { get; set; }
        string? Message { get; set; }
        object? Data { get; set; }

    }

    public class BaseService : IBaseService
    {
        public int Status { get; set; }
        public string? Message { get; set; }
        public object? Data { get; set; }

        public BaseService()
        {
            Status = -1;
            Message = "Action fail";
        }

        public BaseService(int status, string message)
        {
            Status = status;
            Message = message;
        }

        public BaseService(int status, string message, object data)
        {
            Status = status;
            Message = message;
            Data = data;
        }
    }
}
