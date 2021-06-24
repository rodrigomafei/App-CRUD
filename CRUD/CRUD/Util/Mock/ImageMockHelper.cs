using System;
using System.Collections.Generic;
using System.Text;

namespace CRUD.Util.Mock
{
    public static class ImageMockHelper
    {
        public static List<string> ImagePaths
        {
            get
            {
                return new List<string>
                {
                    "https://cdn.pixabay.com/photo/2017/12/09/08/18/pizza-3007395_960_720.jpg",
                    "https://cdn.pixabay.com/photo/2017/09/30/15/10/plate-2802332_960_720.jpg",
                    "https://cdn.pixabay.com/photo/2012/07/19/02/31/pizza-maker-52557_960_720.jpg",
                    "https://cdn.pixabay.com/photo/2018/07/09/09/34/pizza-3525673_960_720.jpg",
                    "https://cdn.pixabay.com/photo/2017/12/10/14/47/pizza-3010062_960_720.jpg"
                };
            }
        }

        public static string GetRandomImage()
        {
            var random = new Random();

            return ImagePaths[random.Next(0, ImagePaths.Count - 1)];
        }
    }
}
