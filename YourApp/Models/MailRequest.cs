﻿using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace _101SendEmailNotificationDoNetCoreWebAPI.Model
{
    public class MailRequest
    {
        public string ToEmail { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public List<IFormFile> Attachments { get; set; }
    }
}
