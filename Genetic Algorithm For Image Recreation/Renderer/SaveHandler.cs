using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Genetic_Algorithm_For_Image_Recreation.Renderer
{
    internal class SaveHandler
    {

        public static void SaveImageToPng(BitmapSource bitmapToSave, string filePath)
        {
            BitmapEncoder encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(bitmapToSave));

            using(var fileStream = new FileStream(filePath, FileMode.Create))
            {
                encoder.Save(fileStream);
            }
        }

    }
}
