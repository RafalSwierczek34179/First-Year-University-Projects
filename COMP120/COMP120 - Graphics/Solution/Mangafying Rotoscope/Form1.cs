using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mangafying_Rotoscope
{
    public partial class ApplicationWindow : Form
    {

        public ApplicationWindow()
        {
            InitializeComponent();
        }


        // Remember to tru using for color in color known colours for posterization----------------------------------------------------------
        //-----------------------------------------------------------------------------------------------------------------------------------
        //-----------------------------------------------------------------------------------------------------------------------------------


        private void ApplicationWindow_Load(object sender, EventArgs e)
        {
            Loading_Text.Text = "Effects may take a few secs to apply";
        }


        private double AverageLuminance(Bitmap bitmap)
        {
            int widthQ1 = (int)(bitmap.Width * 0.33); int widthQ2 = (int)(bitmap.Width * 0.66);
            int heightQ1 = (int)(bitmap.Height * 0.33); int heightQ2 = (int)(bitmap.Height * 0.66);

            double totalLuminance = 0;
            int area = 0;

            // Luminance might vary across the image so I only find the luminance around the center of the image where the face is probably at
            for (int x = widthQ1; x < widthQ2; x++)
            {
                for (int y = heightQ1; y < heightQ2; y++)
                {
                    area += 1;
                    totalLuminance += Luminance(bitmap.GetPixel(x, y));
                }
            }

            return totalLuminance / area;

        }

        // Draws the outline onto the shading instead of vice versa because most of the time there will be less black pixels in the outline than on the shading
        private void BitmapMerge(Bitmap shadingBitmap, Bitmap outlineBitmap)
        {
            for (int x = 0; x < shadingBitmap.Width; x++)
            {
                for (int y = 0; y < shadingBitmap.Height; y++)
                {
                    if (outlineBitmap.GetPixel(x, y) == Color.FromArgb(255, 0, 0, 0))
                    {
                        shadingBitmap.SetPixel(x, y, Color.FromKnownColor(KnownColor.Black));
                    }
                }
            }
        }

        // Uses the given colour distance algorithm
        private double ColourDistance(Color colourOne, Color colourTwo)
        {
            return Math.Abs(Math.Sqrt(Math.Pow(colourTwo.R - colourOne.R, 2) + Math.Pow(colourTwo.G - colourOne.G, 2) + Math.Pow(colourTwo.B - colourOne.B, 2)));
        }

        // Uses the given Luminance algorithm
        private double Luminance(Color colour)
        {
            return (colour.R + colour.G + colour.B) / 3;
        }


        // Uses a modification of the edge detection algorithm given, the given algorithm uses luminance whereas this uses colour distance to determine edges
        private void Outline(Bitmap bitmap, int tolerance)
        {
            int width = bitmap.Width;
            int height = bitmap.Height;

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    if (x + 1 >= width || y + 1 >= height)
                    {
                        // Stops out of bounds error, means that no edges will be detected on the last row/column of pixels but thats okay as it's not really noticeable
                        bitmap.SetPixel(x, y, Color.FromKnownColor(KnownColor.White));
                        break;
                    }

                    Color currentPixelColour = bitmap.GetPixel(x, y);
                    Color rightPixelColour = bitmap.GetPixel(x + 1, y);
                    Color bottomPixelColour = bitmap.GetPixel(x, y + 1);


                    double downDist = ColourDistance(currentPixelColour, bottomPixelColour);
                    double rightDist = ColourDistance(currentPixelColour, rightPixelColour);

                    if (Math.Abs(downDist) >= tolerance && Math.Abs(rightDist) >= tolerance)
                    {
                        bitmap.SetPixel(x, y, Color.FromKnownColor(KnownColor.Black));
                    }
                    else
                    {
                        bitmap.SetPixel(x, y, Color.FromKnownColor(KnownColor.White));
                    }
                }
            }
        }

        private void MangaShading(Bitmap bitmap, int averageLuminance, int twoThirdsOfAveLum)
        {
            for (int x = 0; x < bitmap.Width; x++)
            {
                for (int y = 0; y < bitmap.Height; y++)
                {
                    Color colour = bitmap.GetPixel(x, y);


                    if (Luminance(colour) > averageLuminance)
                    {
                        bitmap.SetPixel(x, y, Color.FromKnownColor(KnownColor.White));
                    }
                    else if (Luminance(colour) > twoThirdsOfAveLum)
                    {
                        bitmap.SetPixel(x, y, Color.FromKnownColor(KnownColor.Gray));
                    }
                    else
                    {
                        bitmap.SetPixel(x, y, Color.FromKnownColor(KnownColor.Black));
                    }
                }
            }
        }

        // Uses my own version of the given posterization algorithm
        private void AnimePosterization(Bitmap imageBitmap)
        { 
            for (int x = 0; x < imageBitmap.Width; x++)
            {
                for (int y = 0; y < imageBitmap.Height; y++)
                {

                    Color colour = imageBitmap.GetPixel(x, y);

                    Color replacementColour = imageBitmap.GetPixel(x, y);
                    double colourDist = 9999;

                    // YellowGreen has the highest index because its the last value
                    for (KnownColor index = 0; index <= KnownColor.YellowGreen; index++)
                    {
                        double tempColourDist = ColourDistance(colour, Color.FromKnownColor(index));
                        if (tempColourDist  < colourDist)
                        {
                            colourDist = tempColourDist;
                            replacementColour = Color.FromKnownColor(index);
                        }
                    }

                    imageBitmap.SetPixel(x, y, replacementColour);
                }
            }
        }

        private void Load_Button_MouseClick(object sender, MouseEventArgs e)
        {
            // I got the 3 lines below from https://docs.microsoft.com/en-us/dotnet/desktop/winforms/controls/how-to-open-files-using-the-openfiledialog-component?view=netframeworkdesktop-4.8
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "JPEGs (*.jpg)|*.jpg";
            fileDialog.ShowDialog();

            Image.imageBitmap = new Bitmap(fileDialog.FileName);
            Image.outlineBitmap = new Bitmap(fileDialog.FileName);
            Image.originalBitmap = new Bitmap(fileDialog.FileName);

            ImageBox.Image = Image.imageBitmap;
            Original_Image.Image = null;
        }

        private void Save_Button_Click(object sender, EventArgs e)
        {
            Bitmap outputBitmap = Image.imageBitmap;

            if (Image.imageBitmap == null)
            {
                return;
            }

            FolderBrowserDialog folderDialog = new FolderBrowserDialog();
            folderDialog.ShowDialog();
            outputBitmap.Save(folderDialog.SelectedPath + "\\output image.jpg");
            

        }

        private void Manga_Effect_Button_Click(object sender, EventArgs e)
        {
            if (Image.imageBitmap == null)
            {
                // Shading/Outline Bitmaps won't be set if the user presses the manga button before loading an image, this stops the program from crashing in such event
                return;
            }

            Bitmap shadingBitmap = Image.imageBitmap;
            Bitmap outlineBitmap = Image.outlineBitmap;

            

            // Displays a little version of the original image to allow the user to compare with the output image
            Original_Image.Image = Image.originalBitmap;

            // After tinkering around with these values, i found these equations produce the most consistent desired graphic effects across most images
            int averageLuminance = (int)AverageLuminance(shadingBitmap);
            int twoThirdsOfAveLum = (int)(averageLuminance * 0.66);
            int outlineTolerance = (int)(AverageLuminance(outlineBitmap) * 0.2);

            MangaShading(shadingBitmap, averageLuminance, twoThirdsOfAveLum);
            Outline(outlineBitmap, outlineTolerance);

            BitmapMerge(shadingBitmap, outlineBitmap);

            ImageBox.Image = shadingBitmap;           
        }

        private void Anime_Button_Click(object sender, EventArgs e)
        {
            if (Image.imageBitmap == null)
            {
                // Posterization/Outline Bitmaps won't be set if the user presses the anime button before loading an image, this stops the program from crashing in such event
                return;
            }

            Bitmap imageBitmap = Image.imageBitmap;
            Bitmap outlineBitmap = Image.outlineBitmap;
            int outlineTolerance = (int)(AverageLuminance(outlineBitmap) * 0.2);
            AnimePosterization(imageBitmap);
            Outline(outlineBitmap, outlineTolerance);
            BitmapMerge(imageBitmap, outlineBitmap);

            ImageBox.Image = imageBitmap;
            Original_Image.Image = Image.originalBitmap;

        }
    }

    public class Image : Form
    {       

        public static Bitmap imageBitmap;
        public static Bitmap outlineBitmap;
        public static Bitmap originalBitmap;
    
    }
}

