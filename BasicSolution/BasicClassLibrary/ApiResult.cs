using System;
using System.Collections.Generic;
using System.Text;

namespace BasicClassLibrary
{
    public class ApiResult
    {
        public ApiResult(int code, string msg, string data)
        {
            Code = code;
            Msg = msg;
            Data = data;
        }
        public ApiResult() { }
        public int Code { get; set; }
        public string Msg { get; set; }
        public string Data { get; set; }
    }



}
