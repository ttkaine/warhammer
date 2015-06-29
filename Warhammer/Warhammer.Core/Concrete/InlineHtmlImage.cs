using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text.RegularExpressions;

namespace Warhammer.Core.Concrete
{
    /// <summary>
    /// ConvertHtml - main class to encode (embed) and decode (extract) images to/from Html.
    /// 
    /// Author: Dennis Lang - 2014
    /// http://home.comcast.net/~lang.dennis/
    ///
    /// This file is part of InlineHtmlImages.
    ///
    /// InlineHtmlImages is free software: you can redistribute it and/or modify
    /// it under the terms of the GNU General Public License as published by
    /// the Free Software Foundation, either version 3 of the License, or
    /// (at your option) any later version.
    ///
    /// InlineHtmlImages is distributed in the hope that it will be useful,
    /// but WITHOUT ANY WARRANTY; without even the implied warranty of
    /// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    /// GNU General Public License for more details.
    ///
    /// The GNU General Public License: <http://www.gnu.org/licenses/>.

    /// </summary>
    class ConvertHtml
    {
        Regex imagePat = new Regex("(<img [^>]+|image: *url[^)]+)", RegexOptions.IgnoreCase | RegexOptions.Compiled);
        Regex imageSrcPat = new Regex("src=(\"[^\"]+\"|'[^']+'| *[^ ]+)", RegexOptions.IgnoreCase | RegexOptions.Compiled);
        Regex imageUrlPat = new Regex(@"url\(([^)]+)", RegexOptions.IgnoreCase | RegexOptions.Compiled);

        public ConvertHtml()
        {
            okayToOverwrite = false;
        }

        public bool okayToOverwrite
        {
            get;
            set;
        }

  



        /// <summary>
        /// Convert base 64 data to an image.
        /// </summary>
        /// <param name="base64String"></param>
        /// <returns></returns>
        private static Image Base64ToImage(string base64String)
        {
            // Convert Base64 String to byte[]
            byte[] imageBytes = Convert.FromBase64String(base64String);
            MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);

            // Convert byte[] to Image
            ms.Write(imageBytes, 0, imageBytes.Length);
            Image image = Image.FromStream(ms, true);
            return image;
        }

        /// <summary>
        /// Return image type (extension) from ImageFormat.
        /// </summary>
        const string sUnknownImageType = "unk";
        private static string GetImageType(ImageFormat imageFormat)
        {
            if (ImageFormat.Bmp.Equals(imageFormat))
                return "bmp";
            else if (ImageFormat.Jpeg.Equals(imageFormat))
                return "jpg";
            else if (ImageFormat.Png.Equals(imageFormat))
                return "png";
            else if (ImageFormat.Gif.Equals(imageFormat))
                return "gif";
            else if (ImageFormat.Tiff.Equals(imageFormat))
                return "tiff";
            else if (ImageFormat.Wmf.Equals(imageFormat))
                return "wmf";

            return sUnknownImageType;
        }

        public Image ExtractImagesInHtml(string text)
        {
            Image myImage = null;
            try
            {
                Match mImg;
                const char separator1 = ',';
                char separator2 = char.MinValue;

             //   Dictionary<ImageInfo, List<ImageUse>> imageCntDic = new Dictionary<ImageInfo, List<ImageUse>>();

                foreach (Match match in imagePat.Matches(text))
                {
                    int pos = match.Index;
                    string imgStr = text.Substring(pos, match.Length);
                    switch (char.ToLower(imgStr[0]))
                    {
                        case 'i':   // background-image: url(foo.png);
                            mImg = imageUrlPat.Match(imgStr);
                            separator2 = char.MinValue;
                            break;
                        case '<':
                            mImg = imageSrcPat.Match(imgStr);
                            separator2 = '\n';
                            break;
                        default:
                            mImg = null;
                            break;
                    }

                    if (mImg != null && mImg.Groups.Count == 2)
                    {
                        string imageData = mImg.Groups[1].Value.Trim().Replace("'", "").Replace("\"", "");

                        if (imageData.StartsWith("data"))
                        {
                            int sepIdx = imageData.IndexOf(separator1);
                            if (separator2 != char.MinValue && imageData[sepIdx + 1] == separator2)
                                sepIdx++;

                            string imageBase64 = imageData.Substring(sepIdx + 1);
                            try
                            {
                                Image image = Base64ToImage(imageBase64);
                                if(image != null)
                                {
                                    return image;
                                }
                            }
                            catch (Exception ex1)
                            {
                            }
                        }
                        else
                        {
                            try
                            {
                                Image bmp = Image.FromFile(imageData);
                                return bmp;

                            }
                            catch (Exception ex1)
                            {
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }

            return myImage;
        }
    }
}
