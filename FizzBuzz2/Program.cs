using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace FizzBuzzPolymorphic
{
    class Program
    {
        static void Main()
        {
            var printer = new Printer(new ListWrapper(Enumerable.Range(1, 100).ToList(),
                                      new NumberFactory()));
            Console.WriteLine(printer.Print());

            Console.ReadLine();
        }
    }


    public class Printer
    {
        private readonly IListWrapper _listWrapper;

        public Printer(IListWrapper listWrapper)
        {
            _listWrapper = listWrapper;
        }

        public string Print()
        {
            return _listWrapper.Wrap()
                .Select(l => l.Print).Aggregate((i, i2) => i + "," + i2);
        }
    }


    public interface IListWrapper
    {
        List<Number> Wrap();
    }

    class ListWrapper : IListWrapper
    {
        private readonly List<int> _numberList;
        private readonly INumberFactory _numberFactory;

        public ListWrapper(List<int> numberList, INumberFactory numberFactory)
        {
            _numberList = numberList;
            _numberFactory = numberFactory;
        }

        public List<Number> Wrap()
        {
            return _numberList.Select(i => _numberFactory.Create(i)).ToList();
        }
    }


    public class Number
    {
        private readonly int _i;

        public Number(int i)
        {
            _i = i;
        }

        public virtual int[] Divisori { get { return new[] { 1 }; } }

        public bool Multiplo
        {
            get { return Divisori.All(d => _i % d == 0); }
        }

        public virtual string Print
        {
            get { return _i.ToString(CultureInfo.InvariantCulture); }
        }
    }

    internal class Fizz : Number
    {
        public Fizz(int i) : base(i) { }

        public override int[] Divisori
        {
            get { return new[] { 3 }; }
        }

        public override string Print { get { return "Fizz"; } }
    }


    internal class Buzz : Number
    {
        public Buzz(int i) : base(i) { }

        public override int[] Divisori { get { return new[] { 5 }; } }

        public override string Print { get { return "Buzz"; } }
    }


    internal class FizzBuzz : Number
    {
        public FizzBuzz(int i) : base(i) { }

        public override int[] Divisori { get { return new[] { 3, 5 }; } }

        public override string Print { get { return "FizzBuzz"; } }
    }


    internal interface INumberFactory
    {
        Number Create(int i);
    }

    internal class NumberFactory : INumberFactory
    {
        public Number Create(int i)
        {
            return new[] { new FizzBuzz(i), 
                            new Fizz(i), 
                            new Buzz(i), 
                            new Number(i) }
                .First(n => n.Multiplo);
        }
    }
}
