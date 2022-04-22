﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FieldAgent.Core
{
    public class Response
    {
        private List<string> messages = new List<string>();
        public bool Success => messages.Count == 0;
        public List<string> Messages => new List<string>(messages);
        public void AddMessage(string message)
        {
            messages.Add(message);
        }
    }
    public class Response<T> : Response
    {
        public T Data { get; set; }
    }
}
