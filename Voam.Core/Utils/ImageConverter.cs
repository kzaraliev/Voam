namespace Voam.Core.Utils
{
    public class ImageConverter
    {
        public static byte[] GetImageDataFromFile(string filePath)
        {
            byte[] imageData;
            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                using (BinaryReader br = new BinaryReader(fs))
                {
                    imageData = br.ReadBytes((int)fs.Length);
                }
            }
            return imageData;
        }
    }
}
