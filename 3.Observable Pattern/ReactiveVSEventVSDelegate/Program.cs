using System;
using System.Reactive.Subjects;

namespace ReactiveVSEventVSDelegate
{
    class Program
    {
        static void Main(string[] args)
        {
            // 1. delegate sample
            var counterWithDelegate = new CounterWithDelegate();
            counterWithDelegate.RegisterCallBack(DelegateOnTargetReached);
            // 2. event sample
            var counterWithEvent = new CounterWithEvent();
            counterWithEvent.OnTargetReached += DelegateOnTargetReached;
            // 3 observer pattern sample
            var counterWithRx = new CounterWithRx();
            counterWithRx
                .OnTargetReached
                .Subscribe(x =>
                {
                    DelegateOnTargetReached(x.name, x.count);
                });

            for (var i = 0; i < 10; i++)
            {
                counterWithDelegate.CountOne();
                counterWithEvent.CountOne();
                counterWithRx.CountOne();
            }

            // function expression
            // 1. Function in class
            var foo = new Foo();
            foo.Function();
            // 2. Static function in static class
            Foo1.Function();
            // 3. Local function
            Function();
            // 4. Anonymous functions
            // lamda expression
            // Action
            Action action1 = () =>
            {
                Console.WriteLine($"Function owner:{nameof(Foo)}");
            };

            Action<int> action2 = x =>
            {
                Console.WriteLine($"Function owner:{nameof(Foo)}");
            };

            action1.Invoke();
            action2.Invoke(123);
            // Func
            Func<bool> func1 = () =>
            {
                return false;
            };
            Func<int, bool> func2 = x =>
            {
                return false;
            };
            var retVal1 = func1.Invoke();
            var retVal2 = func2.Invoke(123);


            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();

            // Local function
            void Function()
            {
                Console.WriteLine($"Function owner:{nameof(Program)}");
            }
        }

        private static void DelegateOnTargetReached(string counterName, int count)
        {
            Console.WriteLine($"Counter[{counterName}] reach target[{count}]!");
            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();
        }
    }

    #region Delegate vs Event vs Rx


    public interface ICounter
    {
        void CountOne();
        string Name { get; set; }
        int Target { get; set; }
    }

    public delegate void DelegateOnTargetReached(string counterName, int count);

    public class CounterWithDelegate : ICounter
    {
        private DelegateOnTargetReached _delegateOnTargetReached;
        private int _count = 0;

        public void CountOne()
        {
            _count++;
            if (_count == Target)
            {
                _delegateOnTargetReached?.Invoke(Name, _count);
            }
        }

        public string Name { get; set; } = nameof(CounterWithDelegate);

        public int Target { get; set; } = 10;

        public void RegisterCallBack(DelegateOnTargetReached delegateOnTargetReached)
        {
            _delegateOnTargetReached = delegateOnTargetReached;
        }
    }

    public class CounterWithEvent : ICounter
    {
        public event DelegateOnTargetReached OnTargetReached;

        private int _count = 0;
        public void CountOne()
        {
            _count++;
            if (_count == Target)
            {
                OnTargetReached?.Invoke(Name, _count);
            }
        }

        public string Name { get; set; } = nameof(CounterWithEvent);

        public int Target { get; set; } = 10;

    }

    public class CounterWithRx : ICounter
    {
        private readonly ISubject<(string, int)> _onTargetReached = new Subject<(string, int)>();
        public IObservable<(string name, int count)> OnTargetReached => _onTargetReached;

        private int _count = 0;
        public void CountOne()
        {
            _count++;
            if (_count == Target)
            {
                _onTargetReached.OnNext((Name, _count));
            }
        }

        public string Name { get; set; } = nameof(CounterWithRx);

        public int Target { get; set; } = 10;

    }

    #endregion

    #region Function Expression

    public class Foo
    {
        public void Function()
        {
            Console.WriteLine($"Function owner:{nameof(Foo)}");
        }
    }

    public static class Foo1
    {
        public static void Function()
        {
            Console.WriteLine($"Function owner:{nameof(Foo1)}");
        }
    }

    #endregion

}
