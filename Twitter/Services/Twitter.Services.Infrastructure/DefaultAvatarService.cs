namespace Twitter.Services.Infrastructure
{
    using System.Drawing;
    using System.IO;
    using Contracts;

    public class DefaultAvatarService : IInitialAvatarService
    {
        public Image GetDefaultAvatarImage(string path)
        {
            Image image = Image.FromFile(path);
            return image;
        }

        public byte[] GetByteArrayFromImage(Image image)
        {
            using (var ms = new MemoryStream())
            {
                image.Save(ms, image.RawFormat);
                return ms.ToArray();
            }
        }
    }
}
