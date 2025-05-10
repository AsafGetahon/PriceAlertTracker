using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightManagementTask.Model.Models
{
    public class ApiResponse<T>
    {
        public bool IsSuccess { get; set; }
        public T? Data { get; set; }
        public List<string> ErrorMessages { get; set; } = new();

        public static ApiResponse<T> Success(T data) => new() { IsSuccess = true, Data = data };
        public static ApiResponse<T> Fail(params string[] errors) =>
            new() { IsSuccess = false, ErrorMessages = errors.ToList() };
    }
}
