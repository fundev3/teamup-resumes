using System;

namespace test_pipeline
{
    public class Program
    {
        static void Main(string[] args)
        {
            Rectangulo rectangulo = new Rectangulo();
            Console.WriteLine(rectangulo.CalcularArea(1, 2));
        }

        public class Rectangulo
        {
            public int Width { get; set; }
            public int Height { get; set; }

            public int CalcularArea(int Widht, int Height)
            {
                return Widht * Height;
            }
        }



    }
}
