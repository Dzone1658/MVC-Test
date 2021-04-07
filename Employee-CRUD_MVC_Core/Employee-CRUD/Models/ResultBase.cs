using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Employee_CRUD.Models
{
    public class ResultBase<T>
    {
        public ResultBase()
        {
        }

        public ResultBase(bool isSuccess, T result)
        {
            IsSuccess = isSuccess;
            Result = result;
        }

        public ResultBase(bool isSuccess, T result, string message)
        {
            IsSuccess = isSuccess;
            Result = result;
            Message = message;
        }

        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public T Result { get; set; }
    }
}
