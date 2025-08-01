﻿using System.Text.Json.Serialization;

namespace Terminal.Dtos.Common
{
    public class ResponseDto<T>
    {
        [JsonIgnore]
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public bool Status { get; set; }
        public T Data { get; set; }
    }
}
