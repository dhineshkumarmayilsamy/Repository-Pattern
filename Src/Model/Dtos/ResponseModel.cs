using System;

namespace Model.Dtos
{
    public class ResponseModel
    {
        public string Status { get; set; }
        public Object Result { get; set; }
        public string ErrorMessage { get; set; }
        public string ErrorCode { get; set; }

    }
}
