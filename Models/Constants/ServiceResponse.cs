﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Constants
{
    public class ServiceResponse
    {
        public string? Message { get; set; }
        public bool IsSuccess { get; set; } = false;
        public string? Error { get; set; } = "some thing went wrong";
    }
}
