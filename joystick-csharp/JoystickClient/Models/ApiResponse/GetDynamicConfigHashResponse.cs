using System;
using System.Collections.Generic;
using System.Text;

namespace JoystickClient.Models.ApiResponse
{
    public class GetDynamicConfigHashResponse
    {
        public string Hash { get; set; }
        public int Version { get; set; }
        public string ContentId { get; set; }
        public long EnvironmentId { get; set; }
    }
}