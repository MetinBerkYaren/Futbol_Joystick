using System;
using System.Collections.Generic;
using UnityEngine;

namespace CappTools
{
    public static class TextureManager
    {

        public static Sprite CreateSprite(this Texture2D texture2D)
        {
            return Sprite.Create(texture2D, new Rect(0, 0, texture2D.width, texture2D.height),
                new Vector2(.5f, .5f));
        }
        
        public static T[,] ConvertTo2Dimension<T>(T[] array, int width, int height)
        {
            var array2Dim = new T[height, width];
            var column = 0;
            var row = height-1;
            for (var i = 0; i < array.GetLength(0); i++)
            {
                array2Dim[row, column] = array[i];
                column++;
                if (column != width) continue;
                column = 0;
                row--;
            }

            return array2Dim;
        }

        public static T[] ConvertTo1Dimension<T>(T[,] colors)
        {
            var colors1Dim = new T[colors.GetLength(0) * colors.GetLength(1)];

            for (var i = colors.GetLength(0)-1; i >= 0; i--)
            {
                for (var j = 0; j < colors.GetLength(1); j++)
                {
                    colors1Dim[(colors.GetLength(0) - 1 - i) * colors.GetLength(1) + j] = colors[i, j];
                }
            }

            return colors1Dim;
        }

        public static Color32[,] RotateCounterClockWiseAsync(Color32[,] color32)
        {
            var newTextureColors = new Color32[color32.GetLength(1), color32.GetLength(0)];

            for (var i = 0; i < newTextureColors.GetLength(0); i++)
            {
                for (var j = 0; j < newTextureColors.GetLength(1); j++)
                {
                    newTextureColors[i, j] = color32[j, newTextureColors.GetLength(0) - i - 1];
                }
            }

            color32 = newTextureColors;
            
            return newTextureColors;
        }

        public static Color32[,] RotateClockWiseAsync(Color32[,] color32)
        {     
            var newTextureColors = new Color32[color32.GetLength(1), color32.GetLength(0)];

            for (var i = 0; i < newTextureColors.GetLength(0); i++)
            {
                for (var j = 0; j < newTextureColors.GetLength(1); j++)
                {
                    newTextureColors[i, j] = color32[newTextureColors.GetLength(1) -  j - 1, i];
                }
            }
            color32 = newTextureColors;
            
            return newTextureColors;
        }

        public static Color32[,] MirrorAxisYAsync(Color32[,] color32)
        {
            var newTextureColors = new Color32[color32.GetLength(0), color32.GetLength(1)];

            for (var i = 0; i < newTextureColors.GetLength(0); i++)
            {
                for (var j = 0; j < newTextureColors.GetLength(1); j++)
                {
                    newTextureColors[i, j] = color32[i, color32.GetLength(1) - j - 1];
                }
            }

            return newTextureColors;
        }
    }

    public static class TextureManagerExtensions
    {   
        
        public static Dictionary<int, float> GetNeighbourPixels(this Color32[,] colors, Vector2 pixel, int radius)
        {
            
            var neighbours = new Dictionary<int, float>();

            var minX = (int)(pixel.x - radius < 0 ? 0 : pixel.x - radius);
            var maxX = (int)(pixel.x + radius > colors.GetLength(1) - 1 ? colors.GetLength(1) - 1 : pixel.x + radius);

            var minY = (int)(pixel.y - radius < 0 ? 0 : pixel.y - radius);
            var maxY = (int)(pixel.y + radius > colors.GetLength(0) - 1 ? colors.GetLength(0) - 1 : pixel.y + radius);

            for (var i = minX; i <= maxX; i++)
            {
                for (var j = minY; j <= maxY; j++)
                {
                    //var distance = Vector2.Distance(new Vector2(i, j), pixel);
                    //if (distance < radius)
                    //    neighbours.Add(j * colors.GetLength(1) + i, distance);
                    if ((new Vector2(i, j) - pixel).sqrMagnitude < radius * radius)
                        neighbours.Add(j * colors.GetLength(1) + i, radius);
                }
            }
            
            return neighbours;
        }
        
        public static void ConvertToBlackWhite(this Color32[] colors)
        {
            for (var i = 0; i < colors.Length; i++)
            {
                var grayScale = (byte)(colors[i].r*.3f + colors[i].g*.59f + colors[i].b*.11f);
                //grayScale = (byte) (grayScale / 32 * 32);
                colors[i] = new Color32(grayScale, grayScale, grayScale,255);
            }
        }

        public static void ApplyLaplacian(this Color32[,] colors)
        {
            var height = colors.GetLength(0);
            var width = colors.GetLength(1);

            var newColors = colors;
            for (var i = 0; i < height; i++)
            {
                for (var j = 0; j < width; j++)
                {
                    var down = colors[Mathf.Clamp(i - 1, 0, height - 1), j];
                    var up = colors[Mathf.Clamp(i + 1, 0, height - 1), j];
                    var right = colors[i, Mathf.Clamp(j + 1, 0, width - 1)];
                    var left = colors[i, Mathf.Clamp(j - 1, 0, width - 1)];
                    var center = colors[i, j];

                    var r = Mathf.Clamp(center.r * 5 - down.r - up.r - right.r - left.r, 0, 255);
                    var g = Mathf.Clamp(center.g * 5 - down.g - up.g - right.g - left.g, 0, 255);
                    var b = Mathf.Clamp(center.b * 5 - down.b - up.b - right.b - left.b, 0, 255);

                    var newColor = new Color32(
                        (byte)r, (byte)g, (byte)b, 255
                    );

                    newColors[i, j] = newColor;
                }
            }
            colors = newColors;
        }
        
        public static void Unsharp(this Color32[,] colors)
        {
            var height = colors.GetLength(0);
            var width = colors.GetLength(1);

            var newColors = colors;

            //BLURED AND NEGATIVE AND SCALED
            for (var i = 0; i < height; i++)
            {
                for (var j = 0; j < width; j++)
                {
                    Color32[] neighbours = new Color32[9];
                    neighbours[0] = colors[Mathf.Clamp(i - 1, 0, height - 1), j]; //down
                    neighbours[1] = colors[Mathf.Clamp(i + 1, 0, height - 1), j];//up
                    neighbours[2] = colors[i, Mathf.Clamp(j + 1, 0, width - 1)];//right
                    neighbours[3] = colors[i, Mathf.Clamp(j - 1, 0, width - 1)];// down

                    neighbours[4] = colors[Mathf.Clamp(i - 1, 0, height - 1), Mathf.Clamp(j + 1, 0, width - 1)];//dr
                    neighbours[5] = colors[Mathf.Clamp(i - 1, 0, height - 1), Mathf.Clamp(j - 1, 0, width - 1)];//dl
                    neighbours[6] = colors[Mathf.Clamp(i + 1, 0, height - 1), Mathf.Clamp(j + 1, 0, width - 1)];//ur
                    neighbours[7] = colors[Mathf.Clamp(i + 1, 0, height - 1), Mathf.Clamp(j - 1, 0, width - 1)];//ul

                    neighbours[8] = colors[i, j];//center

                    int r = 0, g = 0, b = 0;
                    for (var k = 0; k < 9; k++)
                    {
                        r += neighbours[k].r;
                        g += neighbours[k].g;
                        b += neighbours[k].b;
                    }
                    r /= 9;
                    g /= 9;
                    b /= 9;

                    r = Mathf.Clamp((255 - r) / 8 + colors[i, j].r, 0, 255);
                    g = Mathf.Clamp((255 - g) / 8 + colors[i, j].g, 0, 255);
                    b = Mathf.Clamp((255 - b) / 8 + colors[i, j].b, 0, 255);

                    if (r*.3+g*.59+b*.11 > 160)
                    {
                        r = g = b = 255;
                    }
                    var newColor = new Color32(
                        (byte)r, (byte)g, (byte)b, 255
                    );

                    newColors[i, j] = newColor;
                }
            }

            colors = newColors;

        }

        public static Texture2D RotateImage(this Texture2D originTexture, float angle)
        {
            Texture2D result;
            result = new Texture2D(originTexture.width, originTexture.height);
            Color32[] pix1 = result.GetPixels32();
            Color32[] pix2 = originTexture.GetPixels32();
            int W = originTexture.width;
            int H = originTexture.height;
            int x = 0;
            int y = 0;
            Color32[] pix3 = rotateSquare(pix2, (Math.PI / 180 * (double)angle), originTexture);
            for (int j = 0; j < H; j++)
            {
                for (var i = 0; i < W; i++)
                {
                    //pix1[result.width/2 - originTexture.width/2 + x + i + result.width*(result.height/2-originTexture.height/2+j+y)] = pix2[i + j*originTexture.width];
                    pix1[result.width / 2 - W / 2 + x + i + result.width * (result.height / 2 - H / 2 + j + y)] = pix3[i + j * W];
                }
            }
            result.SetPixels32(pix1);
            result.Apply();
            return result;
        }
        static Color32[] rotateSquare(Color32[] arr, double phi, Texture2D originTexture)
        {
            int x;
            int y;
            int i;
            int j;
            double sn = Math.Sin(phi);
            double cs = Math.Cos(phi);
            Color32[] arr2 = originTexture.GetPixels32();
            int W = originTexture.width;
            int H = originTexture.height;
            int xc = W / 2;
            int yc = H / 2;
            for (j = 0; j < H; j++)
            {
                for (i = 0; i < W; i++)
                {
                    arr2[j * W + i] = new Color32(0, 0, 0, 0);
                    x = (int)(cs * (i - xc) + sn * (j - yc) + xc);
                    y = (int)(-sn * (i - xc) + cs * (j - yc) + yc);
                    if ((x > -1) && (x < W) && (y > -1) && (y < H))
                    {
                        arr2[j * W + i] = arr[y * W + x];
                    }
                }
            }
            return arr2;
        }
    }
}