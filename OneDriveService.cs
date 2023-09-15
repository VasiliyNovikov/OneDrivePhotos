using Microsoft.Graph.Models;

namespace OneDrivePhotos
{
    internal class OneDriveService
    {
        private readonly Drive _drive;

        public OneDriveService(Drive drive) => _drive = drive;
    }
}