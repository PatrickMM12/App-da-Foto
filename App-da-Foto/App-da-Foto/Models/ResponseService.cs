﻿using System;
using System.Collections.Generic;
using System.Text;

namespace App_da_Foto.Models
{
    public class ResponseService<T>
    {
        public bool IsSuccess { get; set; }  
        public int StatusCode { get; set; }
        public T Data { get; set; }
        public Dictionary<string, List<string>> Errors { get; set; }
    }   
}
