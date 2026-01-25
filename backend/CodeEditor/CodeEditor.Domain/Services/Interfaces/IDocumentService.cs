using System;
using System.Collections.Generic;
using System.Text;

namespace CodeEditor.Domain.Services.Interfaces
{
    public interface IDocumentService
    {
        Task<string?> LoadDocument(string documentId);

        Task SaveDocument(string documentId, string content);
    }
}
