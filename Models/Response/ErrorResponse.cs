using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentLab.Models.Response
{
    public class ErrorResponse : BaseResponse//inherits
    {
        public List<string> ErrorMsg { get; set; }
        public ErrorResponse(string errMsg)
        {
            ErrorMsg = new List<string>();
            ErrorMsg.Add(errMsg);
            IsSuccessful = false;
        }
        public ErrorResponse(IEnumerable<string> errMsgs)// IEnummerable allow you use a list since we defined a list to handle more than one error message.

        {
            ErrorMsg = new List<string>();
            ErrorMsg.AddRange(errMsgs);
            IsSuccessful = false;
        }
    }
}