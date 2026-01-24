using CodeEditor.Domain.Services.Interfaces;

namespace CodeEditor.Domain.Services
{
    public class DocumentService : IDocumentService
    {
        private readonly IStoreDocumentService _documentStore;

        public DocumentService(IStoreDocumentService documentStore)
        {
            _documentStore = documentStore;
        }

        public async Task<string?> LoadDocument(string documentId)
        {
            return await _documentStore.GetDocumentAsync(documentId);
        }

        public async Task SaveDocument(string documentId, string content)
        {
            if (string.IsNullOrWhiteSpace(content))
            {
                throw new ArgumentException("Content cannot be empty");
            }

            await _documentStore.SaveDocumentAsync(documentId, content);
        }
    }
}
