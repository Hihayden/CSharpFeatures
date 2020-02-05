using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CSharp7Features
{
    /// <summary>
    /// reference 
    /// https://docs.microsoft.com/zh-cn/dotnet/
    /// https://www.cnblogs.com/SavionZhang/p/11197230.html
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Hello World!");
            Test1();
            Test2();
            Test3();
            Test4();
            Test5();
            Console.ReadKey(true);
        }

        /// <summary>
        /// C# 7特性之一，Out variables：out变量直接声明，例如可以out in parameter
        /// </summary>
        static void Test1()
        {
            Console.WriteLine("out变量直接声明");
            var input = "12345";

            if (int.TryParse(input, out int result))
            //支持使用隐式类型的局部变量
            //if (int.TryParse(input, out var result))
            {
                Console.WriteLine(result);
            }
            else
            {
                Console.WriteLine("Could not parse input");
            }
        }

        /// <summary>
        /// C# 7特性之二，Pattern matching：模式匹配，根据对象类型或者其它属性实现方法派发
        /// https://docs.microsoft.com/zh-cn/dotnet/csharp/pattern-matching
        /// </summary>
        static void Test2()
        {
            var childSequence = new List<int>
            {
                -1,
                -2,
                -3,
                7,
                8,
                9
            };

            var sequence = new List<object>
            {
                0,
                childSequence,
                9,
                null,
                ""
            };
            SumPositiveNumbers(sequence);
        }

        public static void SumPositiveNumbers(IEnumerable<object> sequence)
        {
            int sum = 0;
            foreach (var i in sequence)
            {
                switch (i)
                {
                    case 0://常见的常量模式。
                        break;
                    case IEnumerable<int> childSequence://一种类型模式
                        {
                            foreach (var item in childSequence)
                                sum += (item > 0) ? item : 0;
                            break;
                        }
                    case int n when n > 0://具有附加 when 条件的类型模式
                        sum += n;
                        break;
                    case null:
                        Console.WriteLine("Null found in sequence");
                        break;
                    default:
                        Console.WriteLine("Empty found in sequence");
                        break;
                }
            }
            Console.WriteLine($"Sum:{sum}");
        }

        /// <summary>
        /// C# 7特性之三，Tuples：元组
        /// </summary>
        static void Test3()
        {
            var foo = (name: "小明", sex: "男");

            Console.WriteLine(foo.name);

            var str = JsonConvert.SerializeObject(foo);
            //输出的是{"Item1":"小明","sex":"男"}
            Console.WriteLine(str);

            var valueTuple = new ValueTuple<string, string>("小明", "男");
            var str2 = JsonConvert.SerializeObject(valueTuple);
            //输出的是{"Item1":"小明","sex":"男"}
            Console.WriteLine(str2);

            dynamic foo3 = Foo();
            //运行出现 RuntimeBinderException 异常，因为没有发现 name 属性
            try
            {
                Console.WriteLine(foo3.name);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            //匿名类型
            dynamic foo4 = new { name = "小明", site = "男" };
            Console.WriteLine(foo4.name);

            var values1 = GetValues();
            var values2 = GetValuesN();
            Console.WriteLine(values1.Item2.FirstOrDefault());
            Console.WriteLine(values1.Item1);
        }
        static (string name, string site) Foo()
        {
            return (name: "小明", sex: "男");
        }

        /// <summary>
        /// Tuple
        /// </summary>
        /// <returns></returns>
        static Tuple<string, List<int>> GetValues()
        {
            return new Tuple<string, List<int>>("C7", new List<int> { 1, 2, 3 });
        }

        /// <summary>
        /// ValueTuple
        /// </summary>
        /// <returns></returns>
        static (string, List<int>) GetValuesN()
        {
            return ("C7", new List<int> { 1, 2, 3 });
        }

        /// <summary>
        /// C# 7特性之四，Discards：弃元，没有命名的变量，只是占位，后面代码不需要使用其值
        /// </summary>
        static void Test4()
        {
            if (bool.TryParse("TRUE", out bool _))
            {
                Console.WriteLine("");
            }

            bool _ = false, v = false;
            if (bool.TryParse("TRUE", out var _))
            {
                v = _;
                Console.WriteLine(v);
            }
        }

        /// <summary>
        /// C# 7特性之五，Local Functions：局部函数
        /// </summary>
        static void Test5()
        {
            int result = Add(1, 2) + Subtract(3, 4);
            Console.WriteLine("结果：" + result);
            //加
            int Add(int a, int b)
            {
                return a + b;
            }
            //减
            int Subtract(int a, int b)
            {
                return a - b;
            }
        }
    }
}
