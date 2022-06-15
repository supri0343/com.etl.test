using System;
using System.Collections.Generic;
using System.Data;

namespace Com.Danliris.ETL.Service.Models
{
    public class ResponseSuccess
    {
        public ResponseSuccess(string message, object data = null, object info = null)
        {
            this.message = message;
            this.data = data;
            this.info = info;
        }
        public string message {get; set;}
        public object? data {get; set;}
        public object? info {get; set;}
    }
}