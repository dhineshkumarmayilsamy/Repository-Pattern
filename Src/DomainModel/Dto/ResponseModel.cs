using System;

namespace Model.Dto
{
    public class ResponseModel
    {
        public string Status { get; set; }
        public Object Data { get; set; }
        public string ErrorMessage { get; set; }
        public string ErrorCode { get; set; }

    }
}
