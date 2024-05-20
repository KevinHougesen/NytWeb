using Microsoft.AspNetCore.Components.Forms;

namespace FileManager
{
    public class FileModel
    {
        public string Content { get; set; }
        public IBrowserFile File { get; set; }
        public Stream FileStream { get; set; }
    }
}
