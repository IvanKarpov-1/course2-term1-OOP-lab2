using BLL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using Rectangle = DAL.Rectangle;

namespace PL
{
    public class TreePainter
    {
        private static readonly Bitmap NodeBackground = new Bitmap(300, 100);
        private static readonly Size FreeSpace = new Size(NodeBackground.Width / 16, (int)(NodeBackground.Height * 3f));
        private static readonly float Coefficient = NodeBackground.Width / 80f;
        public static readonly Font Font = new Font("Calibri", 14f * Coefficient);

        public Image Draw(out int center, TreeNode<Rectangle> treeNode)
        {
            if (treeNode == null)
            {
                center = 0;
                return new Bitmap(500, 500);
            }

            var leftCenter = 0;
            var rightCenter = 0;

            Image leftNodeImage = null;
            Image rightNodeImage = null;

            if (treeNode.LeftNode != null) leftNodeImage = Draw(out leftCenter, treeNode.LeftNode);
            if (treeNode.RightNode != null) rightNodeImage = Draw(out rightCenter, treeNode.RightNode);

            var leftSize = new Size();
            var rightSize = new Size();
            var under = (leftNodeImage != null) || (rightNodeImage != null);
            if (leftNodeImage != null) leftSize = leftNodeImage.Size;
            if (rightNodeImage != null) rightSize = rightNodeImage.Size;

            var maxHeight = leftSize.Height;
            if (maxHeight < rightSize.Height) maxHeight = rightSize.Height;

            if (leftSize.Width <= 0) leftSize.Width = (int)((NodeBackground.Width - FreeSpace.Width) / 1.4f);
            if (rightSize.Width <= 0) rightSize.Width = (int)((NodeBackground.Width - FreeSpace.Width) / 1.4f);

            var resSize = new Size
            {
                Width = leftSize.Width + rightSize.Width + FreeSpace.Width,
                Height = NodeBackground.Size.Height + (under ? maxHeight + FreeSpace.Height : 0)
            };

            var result = new Bitmap(resSize.Width, resSize.Height);
            var graphic = Graphics.FromImage(result);
            graphic.SmoothingMode = SmoothingMode.HighQuality;
            //graphic.FillRectangle(Brushes.White, new System.Drawing.Rectangle(new Point(0, 0), resSize));
            var str = $"{treeNode.Data.CalculateArea()}";
            graphic.FillRectangle(GetColorFromHex(treeNode.Data.FillColor), leftSize.Width - NodeBackground.Width / 2 + FreeSpace.Width / 2, 0, NodeBackground.Width + str.Length * Coefficient, NodeBackground.Height);
            graphic.DrawRectangle(new Pen(GetColorFromHex(treeNode.Data.BorderColor), 3f * Coefficient), leftSize.Width - NodeBackground.Width / 2 + FreeSpace.Width / 2, 0, NodeBackground.Width + str.Length * Coefficient, NodeBackground.Height);
            graphic.DrawString(str, Font, Brushes.Black, leftSize.Width - NodeBackground.Width / 2 + FreeSpace.Width / 2 + (2 + (str.Length == 1 ? 10 : str.Length == 2 ? 5 : 0)) * Coefficient, NodeBackground.Height / 2f - 12 * Coefficient);

            center = leftSize.Width + FreeSpace.Width / 2;
            var pen = new Pen(Brushes.Black, 3f * Coefficient)
            {
                EndCap = LineCap.ArrowAnchor,
                StartCap = LineCap.Round
            };

            float x1 = center;
            float x2 = leftCenter;
            float y1 = NodeBackground.Height + 10;
            float y2 = NodeBackground.Height + FreeSpace.Height;
            var h = Math.Abs(y2 - y1);
            var w = Math.Abs(x2 - x1);

            if (leftNodeImage != null)
            {
                graphic.DrawImage(leftNodeImage, 0, NodeBackground.Size.Height + FreeSpace.Height);

                var points = new List<PointF>
                {
                    new PointF(x1, y1),
                    new PointF(x1 - w / 6, y1 + h / 3.5f),
                    new PointF(x2 + w / 6, y2 - h / 3.5f),
                    new PointF(x2, y2),
                };

                graphic.DrawCurve(pen, points.ToArray(), 0.5f);
            }
            if (rightNodeImage != null)
            {
                graphic.DrawImage(rightNodeImage, leftSize.Width + FreeSpace.Width, NodeBackground.Size.Height + FreeSpace.Height);
                x2 = rightCenter + leftSize.Width + FreeSpace.Width;
                w = Math.Abs(x2 - x1);

                var points = new List<PointF>
                {
                    new PointF(x1, y1),
                    new PointF(x1 + w/6, y1 + h/3.5f),
                    new PointF(x2 - w/6, y2 - h/3.5f),
                    new PointF(x2, y2)
                };

                graphic.DrawCurve(pen, points.ToArray(), 0.5f);
            }

            return result;
        }

        private SolidBrush GetColorFromHex(string hex)
        {
            var hexColor = $"{hex}".Replace("#", "");
            var a = byte.Parse(hexColor.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
            var r = byte.Parse(hexColor.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
            var g = byte.Parse(hexColor.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);
            return new SolidBrush(Color.FromArgb(a, r, g, 0));
        }
    }
}
