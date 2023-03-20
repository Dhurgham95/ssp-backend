using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend
{
    public class UserMangageResponse
    {
     
            public string Message { get; set; }
            public bool IsSuccess { get; set; }
            public IEnumerable<String> Errors { get; set; }

            public DateTime? ExpireDate { get; set; }

            public DateTime? CreatedAt { get; set; }
        
    }
}
