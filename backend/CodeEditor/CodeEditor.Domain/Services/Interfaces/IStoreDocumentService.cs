using System;
using System.Collections.Generic;
using System.Text;

namespace CodeEditor.Domain.Services.Interfaces
{
    public interface IStoreDocumentService
    {
        Task<string?> GetDocumentAsync(string documentId); 
        Task<bool> SaveDocumentAsync(string documentId, string content); 
        Task<bool> DeleteDocumentAsync(string documentId); 
    }
}
