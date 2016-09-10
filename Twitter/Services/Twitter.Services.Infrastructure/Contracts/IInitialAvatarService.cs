namespace Twitter.Services.Infrastructure.Contracts
{
    using System.Drawing;

    public interface IInitialAvatarService
    {
        Image GetDefaultAvatarImage(string path);

        byte[] GetByteArrayFromImage(Image image);
    }
}
