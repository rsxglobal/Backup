using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentLab.Models.Response
{
    public class BaseResponse
    {
        public bool IsSuccessful { get; set; }
        public string TransactionId { get; set; }
        public BaseResponse()
        {
            TransactionId = Guid.NewGuid().ToString();
        }
    }
}