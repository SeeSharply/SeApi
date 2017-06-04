﻿using SeApi.Core.Attribute;

namespace SeApi.Core.Base
{
    public class ApiRequest
    {
        [Required]
        public string AppKey { get; set; }
        public string UserId { get; set; }
    }
}
