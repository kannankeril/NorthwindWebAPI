using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NorthwindWebAPI.Models
{
    public class ResultObject
    {
        public int Status { get; set; }
        public string Message { get; set; }
        public object Value { get; set; }

        public ResultObject(int status, string message)
        {
            Status = status;
            Message = message;
        }

        public ResultObject(object value)
        {
            Status = 1;
            Message = String.Empty;
            Value = value;
        }

        public bool IsSuccessful
        {
            get { return Status == 1 ? true : false; }
        }
    }
}