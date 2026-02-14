using System;
using System.Collections.Generic;
using System.Text;

namespace CodeEditor.Domain.Entities
{
    public class GitRepo
    {
        public long Id { get; set; }
        
        public string Name { get; set; }

        public string Url { get; set; }
    }
}
