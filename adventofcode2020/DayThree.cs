using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace adventofcode2020
{
    public static class DayThree
    {
        public static void Execute()
        {
            var traverser = new ForestMapTraverser();

            var Right1Down1 = new ForestMapPathTracker("Right1Down1", 1, 1);
            Right1Down1.Subscribe(traverser);

            var Right3Down1 = new ForestMapPathTracker("Right3Down1", 3, 1);
            Right3Down1.Subscribe(traverser);

            var Right5Down1 = new ForestMapPathTracker("Right5Down1", 5, 1);
            Right5Down1.Subscribe(traverser);

            var Right7Down1 = new ForestMapPathTracker("Right7Down1", 7, 1);
            Right7Down1.Subscribe(traverser);

            var Right1Down2 = new ForestMapPathTracker("Right1Down2", 1, 2);
            Right1Down2.Subscribe(traverser);

            traverser.IterateOnMap();
            traverser.GetCounts();
        }
    }

    public class ForestMap
    {
        public int CurrentY { get; internal set; } = 0;
        public int XLengthUntilRepeat { get; internal set; }
        public bool EndOfMap { get; internal set; } = false;

        private List<string> _treeInput;

        public ForestMap()
        {
            if(_treeInput is null)
            {
                _treeInput = File.ReadAllLines(Path.Combine(Environment.CurrentDirectory, "input", "DayThree.txt")).ToList();
                XLengthUntilRepeat = _treeInput[0].Length;
            }
        }

        public void NextMapLine()
        {
            CurrentY++;
            if (CurrentY == _treeInput.Count) { EndOfMap = true; }
        }

        public string GetCurrentMapLine()
        {
            return _treeInput[CurrentY];
        }
    }

    public class ForestMapPathTracker : IObserver<ForestMap>
    {
        private IDisposable _unsubscriber;
        public string Name { get; internal set; }
        public int TreeCount { get; internal set; }
        private readonly int _xMovement = 0;
        private readonly int _yMovement = 0;
        private int _currentX = 0;

        public ForestMapPathTracker(string name, int xMovement, int yMovement)
        {
            Name = name;
            _xMovement = xMovement;
            _yMovement = yMovement;
        }

        public virtual void Subscribe(IObservable<ForestMap> provider)
        {
            if (provider != null)
            {
                _unsubscriber = provider.Subscribe(this);
            }
        }

        public void OnCompleted()
        {
            Console.WriteLine($"Number of trees on forest path for '{Name}': {TreeCount}");
            Unsubscribe();
        }

        public void OnError(Exception error)
        {
            throw error;
        }

        public void OnNext(ForestMap value)
        {
            if(value.CurrentY % _yMovement == 0)
            {
                if (value.GetCurrentMapLine()[_currentX] == '#')
                {
                    TreeCount++;
                }

                _currentX += _xMovement;

                if (_currentX >= value.XLengthUntilRepeat)
                {
                    _currentX -= value.XLengthUntilRepeat;
                }
            }
        }

        public virtual void Unsubscribe()
        {
            _unsubscriber.Dispose();
        }
    }

    public class ForestMapTraverser : IObservable<ForestMap>
    {
        private List<IObserver<ForestMap>> _observers = new List<IObserver<ForestMap>>();
        private ForestMap _forestMap;

        public ForestMapTraverser()
        {
            _forestMap = new ForestMap();
        }

        public IDisposable Subscribe(IObserver<ForestMap> observer)
        {
            if (!_observers.Contains(observer))
            {
                _observers.Add(observer);
            }
            return new Unsubscriber(_observers, observer);
        }

        private class Unsubscriber : IDisposable
        {
            private List<IObserver<ForestMap>> _observers;
            private IObserver<ForestMap> _observer;

            public Unsubscriber(List<IObserver<ForestMap>> observers, IObserver<ForestMap> observer)
            {
                _observers = observers;
                _observer = observer;
            }

            public void Dispose()
            {
                if (_observer != null && _observers.Contains(_observer))
                {
                    _observers.Remove(_observer);
                }
            }
        }

        public void IterateOnMap()
        {
            while (!_forestMap.EndOfMap)
            {
                foreach (var observer in _observers)
                {
                    observer.OnNext(_forestMap);
                }

                _forestMap.NextMapLine();
            }
        }

        public void GetCounts()
        {
            Int64 treeCountProduct = 0;

            foreach (var observer in _observers.ToArray())
            {
                if (_observers.Contains(observer))
                {
                    if (treeCountProduct == 0)
                    {
                        treeCountProduct = ((ForestMapPathTracker)observer).TreeCount;
                    }
                    else
                    {
                        treeCountProduct *= ((ForestMapPathTracker)observer).TreeCount;
                    }
                    observer.OnCompleted();
                }
            }

            Console.WriteLine($"Product of trees for all trackers: {treeCountProduct}");
            _observers.Clear();
        }
    }
}
