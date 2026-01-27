using System;
using System.Collections.Generic;
using System.Text;

namespace CodeEditor.Domain.Requests.AuthRequests
{
    public class RefreshTokensRequest
    {
        public long UserId { get; set; }
        public string RefreshToken { get; set; }
    }
}
