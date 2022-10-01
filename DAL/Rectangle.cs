using System;
using System.Text.RegularExpressions;

namespace DAL
{
    public class Rectangle : IComparable, IComparable<Rectangle>
    {
        public string FillColor { get; }
        public string BorderColor { get; }
        public double SideAB { get; }
        public double SideBC { get; }

        private static readonly Random Random = new Random();


        public Rectangle()
        {
            FillColor = RandomHexColor();
            BorderColor = RandomHexColor();
            SideAB = RandomSideSize();
            SideBC = RandomSideSize();
        }

        public Rectangle(string fillColor, string borderColor, double sideAB, double sideBC)
        {
            CheckHexCode(fillColor);
            CheckHexCode(borderColor);
            CheckSide(sideAB);
            CheckSide(sideBC);

            FillColor = fillColor;
            BorderColor = borderColor;
            SideAB = sideAB;
            SideBC = sideBC;
        }

        public double CalculateArea()
        {
            return SideAB * SideBC;
        }

        public double CalculatePerimeter()
        {
            return SideAB * 2 + SideBC * 2;
        }

        private static void CheckHexCode(string code)
        {
            var regex = new Regex(@"^#[0-9A-F]{6}$");
            if (!regex.IsMatch(code))
            {
                throw new Exception($"HEX код неправильний! {code}");
            }
        }

        private static void CheckSide(double side)
        {
            if (side <= 0)
            {
                throw new Exception("Сторона повинна бути додатньою!");
            }
        }

        public static string RandomHexColor()
        {
            var hex = "#";

            for (var i = 0; i < 3; i++)
            {
                var temp = Random.Next(0, 255);
                temp = temp < 10 ? temp + 10 : temp;
                hex += Convert.ToString(Convert.ToInt32((int)temp), 16).ToUpper();
            }

            if (hex.Length < 7)
            {
                var numberOfMissingCharacters = 7 - hex.Length;
                for (var i = 0; i < numberOfMissingCharacters; i++)
                {
                    hex += "0";
                }
            }

            return hex;
        }

        public static double RandomSideSize()
        {
            var temp = Random.NextDouble() * Random.Next(1, 99);
            return Math.Round((double)temp, 1) + 0.1;
        }

        public override string ToString()
        {
            return
                $"Прямокутник зі сторонами {SideAB,-6:F2} та {SideBC,-6:F2}, " +
                $"кольором заповнення - {FillColor} та контуру - {BorderColor}";
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Rectangle rectangle)) return false;
            return SideAB == rectangle.SideAB &&
                   SideBC == rectangle.SideBC &&
                   BorderColor == rectangle.BorderColor &&
                   FillColor == rectangle.FillColor;
        }

        public int CompareTo(object obj)
        {
            return CompareTo_Help(obj);
        }

        public int CompareTo(Rectangle other)
        {
            return CompareTo_Help(other);
        }

        private int CompareTo_Help(object obj)
        {
            var temp = this.CalculateArea() - ((Rectangle)obj).CalculateArea();

            if (temp < 0)
            {
                return -1;
            }

            return temp == 0 ? 0 : 1;
        }
    }
}
