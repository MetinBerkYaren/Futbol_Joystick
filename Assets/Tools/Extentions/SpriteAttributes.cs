using System.Collections.Generic;
using System.Security.Permissions;
using UnityEngine;
using UnityEngine.UI;

namespace CappTools
{
    public static class SpriteAttributes
    {
        public static void FitHeight(this GameObject gameObject, float height)
        {
            if (gameObject.GetComponent<SpriteRenderer>() != null)
            {
                gameObject.transform.localScale = new Vector3(
                    height / gameObject.GetHeightPixel() * gameObject.transform.localScale.x,
                    height / gameObject.GetHeightPixel() * gameObject.transform.localScale.y,
                    gameObject.transform.localScale.z);
            }
        }

        public static void FitWidth(this GameObject gameObject, float width)
        {
            if (gameObject.GetComponent<SpriteRenderer>() != null)
            {
                gameObject.transform.localScale =
                    new Vector3(width / gameObject.GetWidthPixel() * gameObject.transform.localScale.x,
                        width / gameObject.GetWidthPixel() * gameObject.transform.localScale.y,
                        gameObject.transform.localScale.z);
            }
        }

        public static void FitWidthAndHeight(this GameObject gameObject, float width, float height)
        {
            if (gameObject.GetComponent<SpriteRenderer>() != null)
            {
                gameObject.FitWidth(width);
                gameObject.FitHeight(height);
            }
        }


        /*
         * Eski sistemdeki texture'nin yüksekliğini veya genişliğini alıp pixelsPerUnit'e bölmek ve lossyScale ile
         * çarpmak sağlıklı değil çünkü spriteAtlas kullanılıyorsa texture olarak atlas geliyor. Onun yerine
         * sprite'ın bound'unu lossyScale ile ve pixelsPerUnit ile çarpmak daha mantıklı
         */
        public static float GetWidthPixel(this GameObject gameObject)
        {
            if (gameObject.GetComponent<SpriteRenderer>() == null) return 0;

            var sprite = gameObject.GetComponent<SpriteRenderer>().sprite;
            return sprite.bounds.size.x * sprite.pixelsPerUnit * gameObject.transform.lossyScale.x;
            //return sprite.texture.width * gameObject.transform.lossyScale.x;
        }

        public static float GetHeightPixel(this GameObject gameObject)
        {
            if (gameObject.GetComponent<SpriteRenderer>() == null) return 0;

            var sprite = gameObject.GetComponent<SpriteRenderer>().sprite;
            return sprite.bounds.size.y * sprite.pixelsPerUnit * gameObject.transform.lossyScale.y;
            //return sprite.texture.height * gameObject.transform.lossyScale.y;
        }

        public static float GetWidthUnit(this GameObject gameObject)
        {
            if (gameObject.GetComponent<SpriteRenderer>() == null) return 0;

            var sprite = gameObject.GetComponent<SpriteRenderer>().sprite;
            return sprite.bounds.size.x * gameObject.transform.lossyScale.x;
            //return (sprite.texture.width / sprite.pixelsPerUnit) * gameObject.transform.lossyScale.x;
        }

        public static float GetHeightUnit(this GameObject gameObject)
        {
            if (gameObject.GetComponent<SpriteRenderer>() == null) return 0;

            var sprite = gameObject.GetComponent<SpriteRenderer>().sprite;
            return sprite.bounds.size.y * gameObject.transform.lossyScale.y;
            //return (sprite.texture.height / sprite.pixelsPerUnit) * gameObject.transform.lossyScale.y;
        }

        public static Rect GetBoundingBox(this GameObject gameObject)
        {
            if (gameObject.GetComponent<SpriteRenderer>() == null) return Rect.zero;

            var offset = gameObject.GetComponent<SpriteRenderer>().sprite.pivot;

            offset = new Vector2(offset.x / gameObject.GetWidthPixel(), offset.y / gameObject.GetHeightPixel());

            var widthUnit = gameObject.GetWidthUnit();
            var heightUnit = gameObject.GetHeightUnit();

            var minX = gameObject.transform.position.x - offset.x * widthUnit;
            var minY = gameObject.transform.position.y - offset.y * heightUnit;

            return new Rect(minX, minY, widthUnit, heightUnit);
        }

        public static Rect GetBoundingBoxOf2DObject(this GameObject gameObject2D)
        {
            var boundingBox = gameObject2D.GetBoundingBox();

            var minX = boundingBox.min.x;
            var minY = boundingBox.min.y;
            var maxX = boundingBox.max.x;
            var maxY = boundingBox.max.y;

            if (boundingBox.Equals(Rect.zero))
            {
                minX = minY = 1000000;
                maxX = maxY = -1000000;
            }

            for (var i = 0; i < gameObject2D.transform.childCount; i++)
            {
                var childRect = GetBoundingBoxOf2DObject(gameObject2D.transform.GetChild(i).gameObject);

                if (childRect.Equals(Rect.zero))
                {
                    continue;
                }

                if (childRect.xMax > maxX)
                {
                    maxX = childRect.max.x;
                }

                if (childRect.yMax > maxY)
                {
                    maxY = childRect.yMax;
                }

                if (childRect.xMin < minX)
                {
                    minX = childRect.xMin;
                }

                if (childRect.yMin < minY)
                {
                    minY = childRect.yMin;
                }
            }

            return new Rect(minX, minY, maxX - minX, maxY - minY);
        }

        public static Rect GetBoundingBoxOfUiCanvas(this GameObject canvas)
        {
            var boundingBox = canvas.GetComponent<Image>() == null
                ? new Rect(0, 0, 0, 0)
                : canvas.GetComponent<Image>().rectTransform.rect;

            var minX = boundingBox.min.x;
            var minY = boundingBox.min.y;
            var maxX = boundingBox.max.x;
            var maxY = boundingBox.max.y;

            if (boundingBox.Equals(Rect.zero))
            {
                minX = minY = 1000000;
                maxX = maxY = -1000000;
            }

            for (var i = 0; i < canvas.transform.childCount; i++)
            {
                if (canvas.transform.GetChild(i).GetComponent<Image>())
                {

                    var childRect = GetBoundingBoxOfUiCanvas(canvas.transform.GetChild(i).gameObject);

                    if (childRect.Equals(Rect.zero))
                    {
                        continue;
                    }

                    if (childRect.xMax > maxX)
                    {
                        maxX = childRect.max.x;
                    }

                    if (childRect.yMax > maxY)
                    {
                        maxY = childRect.yMax;
                    }

                    if (childRect.xMin < minX)
                    {
                        minX = childRect.xMin;
                    }

                    if (childRect.yMin < minY)
                    {
                        minY = childRect.yMin;
                    }
                }
            }

            return new Rect(minX, minY, maxX - minX, maxY - minY);
        }

        public static int GetPixelOnPosition(this GameObject gameObject, Rect spriteRect, Color32[,] colors2D,
            Vector2 position)
        {
            //var sprite = gameObject.GetComponent<SpriteRenderer>().sprite;
            //var vertices = sprite.vertices;

            var minX = spriteRect.xMin;
            var maxX = spriteRect.xMax;

          

            var minY = spriteRect.yMin;
            var maxY = spriteRect.yMax;

            var xRatio = (position.x - minX) / (maxX - minX);
            var yRatio = (maxY - position.y) / (maxY - minY);

            if (xRatio < 0 || xRatio > 1 || yRatio < 0 || yRatio > 1)
            {
                return -1;
            }

            var clickedPixel = new int[2];

            clickedPixel[0] = (int) (colors2D.GetLength(1) * xRatio);
            clickedPixel[1] = (int) (colors2D.GetLength(0) * (1 - yRatio));

            return clickedPixel[1] * colors2D.GetLength(1) + clickedPixel[0];
        }

        public static void SetAlpha(this GameObject gameObject, float alpha)
        {
            if (gameObject.GetComponent<SpriteRenderer>() == null) return;

            var color = gameObject.GetComponent<SpriteRenderer>().color;

            gameObject.GetComponent<SpriteRenderer>().color = new Color(color.r, color.g, color.b, alpha);
        }

        public static float GetAlpha(this GameObject gameObject)
        {
            if (gameObject.GetComponent<SpriteRenderer>() == null) return 0;

            return gameObject.GetComponent<SpriteRenderer>().color.a;
        }
         
    }
}
