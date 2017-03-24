using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentLab.Models.Response
{
    public class SuccessResponse : BaseResponse//inherits
    {
        public SuccessResponse()
        {
            IsSuccessful = true;
        }
    }
}