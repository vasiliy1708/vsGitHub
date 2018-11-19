using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vsGitHub
{
    class MagicValue
    {
        public int Left { get; set; }
        public int Right { get; set; }

        public MagicValue(int left, int right)
        {
            Left = left;
            Right = right;
        }

        public static void Apply(MagicValue magicValue)
        {
            magicValue.Left += 3;
            magicValue.Right += 4;
            magicValue = new MagicValue(5, 6);
        }

        public static void ApplyRef(ref MagicValue magicValue)
        {
            magicValue.Left += 7;
            magicValue.Right += 8;
            magicValue = new MagicValue(9, 10);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var magicValue = new MagicValue(1, 2);
            MagicValue.ApplyRef(ref magicValue);
            MagicValue.Apply(magicValue);

            Console.WriteLine(magicValue.Left * magicValue.Right);
            Console.ReadKey();
        }
    }
}
