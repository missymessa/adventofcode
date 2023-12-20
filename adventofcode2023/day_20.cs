using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Data;
using adventofcode.util;

namespace adventofcode2023
{
    public class day_20 : DayBase<long>
    {
        public day_20() : base("day_20.txt")
        {
            Console.WriteLine("Advent of Code - Day Twenty");
        }

        public day_20(string fileName) : base(fileName) { }

        Dictionary<string, (Type moduleClass, List<string> destinations)> relationships = new Dictionary<string, (Type moduleClass, List<string> destinations)>();
        ModuleManager moduleManager = new ModuleManager();

        public override long Problem1()
        {
            SetUp();

            // Do work in ModuleManager
            for (int i = 0; i < 1000; i++)
            {
                moduleManager.PushButton();
            }

            return moduleManager.PulseProduct;
        }

        public override long Problem2()
        {
            if (moduleManager.Modules.Count == 0)
            {
                SetUp();
            }

            while (moduleManager.LCM == 0)
            {
                moduleManager.PushButton();
            }

            return moduleManager.LCM;
        }

        public void SetUp()
        {
            // parse input
            foreach (var line in _input)
            {
                var lineSplit = line.Split(" -> ");
                var destinationModules = lineSplit[1].Split(",").Select(x => x.Trim()).ToList();
                if (lineSplit[0].StartsWith('%'))
                {
                    var name = lineSplit[0].Substring(1);
                    relationships.Add(name, (typeof(FlipFlopModule), destinationModules));
                }
                else if (lineSplit[0].StartsWith('&'))
                {
                    var name = lineSplit[0].Substring(1);
                    relationships.Add(name, (typeof(ConjunctionModule), destinationModules));
                }
                else
                {
                    var name = lineSplit[0];
                    relationships.Add(name, (typeof(BroadcasterModule), destinationModules));
                }
            }

            // put into ModuleManager
            foreach (var mod in relationships)
            {
                if (mod.Value.moduleClass == typeof(FlipFlopModule))
                {
                    var name = mod.Key;
                    FlipFlopModule f = new FlipFlopModule(name, mod.Value.destinations);
                    moduleManager.AddModule(name, f);
                }
                else if (mod.Value.moduleClass == typeof(ConjunctionModule))
                {
                    List<string> sourceModules = new List<string>();
                    // Need to populate this
                    foreach (var r in relationships)
                    {
                        if (r.Value.destinations.Contains(mod.Key))
                        {
                            sourceModules.Add(r.Key);
                        }
                    }

                    var name = mod.Key;
                    ConjunctionModule c = new ConjunctionModule(name, mod.Value.destinations, sourceModules);
                    moduleManager.AddModule(name, c);
                }
                else
                {
                    var name = mod.Key;
                    moduleManager.AddModule(name, new BroadcasterModule(name, mod.Value.destinations));
                }
            }

            // make sure all destination modules are in the ModuleManager. If not, just add a generic type
            foreach (var mod in relationships)
            {
                foreach (var dest in mod.Value.destinations)
                {
                    if (!moduleManager.Modules.ContainsKey(dest))
                    {
                        moduleManager.AddModule(dest, new BroadcasterModule(dest, new List<string>()));
                    }
                }
            }
        }
    }

    public enum PulseType
    {
        Low,
        High
    }

    public abstract class AModule
    {
        public string Name { get; init; }
        public List<string> DestinationModules { get; init; }
        public abstract List<(string name, PulseType pulseType)> ReceivePulse(string name, PulseType pulseType, string? source);

        public virtual List<(string name, PulseType pulseType)> SendPulse(PulseType pulseType)
        {
            List<(string name, PulseType pulseType)> send = new List<(string name, PulseType pulseType)>();
            foreach (var m in DestinationModules)
            {
                send.Add((m, pulseType));
            }
            return send;
        }
    }

    // Flip-flop modules (prefix %) are either on or off; they are initially off. If a flip-flop module receives a high pulse,
    // it is ignored and nothing happens. However, if a flip-flop module receives a low pulse, it flips between on and off.
    // If it was off, it turns on and sends a high pulse. If it was on, it turns off and sends a low pulse.
    public class FlipFlopModule : AModule
    {
        public bool State { get { return _state; } }
        private bool _state = false;

        public FlipFlopModule(string name, List<string> destinationModules)
        {
            Name = name;
            DestinationModules = destinationModules;
        }

        public override List<(string name, PulseType pulseType)> ReceivePulse(string name, PulseType pulseType, string? source)
        {
            if (pulseType == PulseType.Low)
            {
                bool previousState = _state;
                _state = !_state;

                if (previousState)
                {
                    return SendPulse(PulseType.Low);
                }
                else
                {
                    return SendPulse(PulseType.High);
                }
            }

            return null;
        }
    }

    // Conjunction modules(prefix &) remember the type of the most recent pulse received from each of their connected
    // input modules; they initially default to remembering a low pulse for each input.When a pulse is received, the
    // conjunction module first updates its memory for that input. Then, if it remembers high pulses for all inputs,
    // it sends a low pulse; otherwise, it sends a high pulse.
    public class ConjunctionModule : AModule
    {
        public Dictionary<string, PulseType> SourceModules { get; init; }

        public ConjunctionModule(string name, List<string> destinationModules, List<string> sourceModules)
        {
            Name = name;
            DestinationModules = destinationModules;
            SourceModules = new Dictionary<string, PulseType>();
            sourceModules.ForEach(key => SourceModules[key] = PulseType.Low);
        }

        public override List<(string name, PulseType pulseType)> ReceivePulse(string name, PulseType pulseType, string? source)
        {
            SourceModules[source] = pulseType;
            if (SourceModules.Values.All(v => v == PulseType.High))
            {
                return SendPulse(PulseType.Low);
            }
            else
            {
                return SendPulse(PulseType.High);
            }
        }
    }

    // There is a single broadcast module (named broadcaster). When it receives a pulse, it sends the same pulse to all of its destination modules.
    public class BroadcasterModule : AModule
    {
        public BroadcasterModule(string name, List<string> destinationModules)
        {
            Name = name;
            DestinationModules = destinationModules;
        }
        public override List<(string name, PulseType pulseType)> ReceivePulse(string name, PulseType pulseType, string? source)
        {
            return SendPulse(pulseType);
        }
    }

    public class ModuleManager
    {
        private Dictionary<string, AModule> modules = new Dictionary<string, AModule>();
        public Dictionary<string, AModule> Modules { get { return modules; } }
        private int HighPulseCount { get; set; } = 0;
        private int LowPulseCount { get; set; } = 0;
        public int PulseProduct { get { return HighPulseCount * LowPulseCount; } }
        public int ButtonPressCount { get { return _buttonPressCount; } }
        private int _buttonPressCount = 0;

        public long LCM { get { return _lcm; } }
        private long _lcm = 0;
        private Dictionary<string, int> modulesToRx = new Dictionary<string, int>() { { "ct", 0 }, { "kp", 0 }, { "ks", 0 }, { "xc", 0 } };
        

        public Queue<(string name, PulseType pulseType, string? source)> work = new Queue<(string name, PulseType pulseType, string? source)>();
        public ModuleManager()
        {

        }

        public void AddModule(string name, AModule module)
        {
            modules.Add(name, module);
        }

        public void PushButton()
        {
            // Here at Desert Machine Headquarters, there is a module with a single button on it called, aptly, the button module.
            // When you push the button, a single low pulse is sent directly to the broadcaster module.
            work.Enqueue(("broadcaster", PulseType.Low, "button"));
            _buttonPressCount++;
            ProcessWork();
        }

        private void ProcessWork()
        {
            while(work.Any())
            {
                var workItem = work.Dequeue();
                if(workItem.pulseType == PulseType.High)
                {
                    HighPulseCount++;
                }
                else
                {
                    LowPulseCount++;
                }

                // ** Start Part 2 Logic **
                // Looking at the dataset, we have four modules that feed into bb which sends a pulse to rx. bb is a conjunction module, 
                // so we need to look at when one of it's source modules sends in a high pulse. Once we've tracked when all of the four modules
                // sends a high pulse, we can calculate the LCM of the four values.
                if(modulesToRx.ContainsKey(workItem.source) && workItem.pulseType == PulseType.High && modulesToRx[workItem.source] == 0)
                {
                    modulesToRx[workItem.source] = _buttonPressCount;
                }

                if(modulesToRx.Values.All(x => x > 0))
                {
                    // determine LCM, return. 
                    _lcm = modulesToRx.Values.First();

                    // Iterate through the values in the dictionary
                    foreach (long value in modulesToRx.Values)
                    {
                        // Calculate the LCM of the current value and the current lcm
                        _lcm = CalculateLCM(_lcm, value);
                    }
                }
                // ** End Part 2 Logic **

                var sendList = modules[workItem.name].ReceivePulse(workItem.name, workItem.pulseType, workItem.source);
                if (sendList != null)
                {
                    foreach (var m in sendList)
                    {
                        work.Enqueue((m.name, m.pulseType, workItem.name));
                    }
                }
            }
        }

        static long CalculateLCM(long a, long b)
        {
            // Use GCD (Greatest Common Divisor) to find LCM
            return (a * b) / CalculateGCD(a, b);
        }

        // Function to calculate GCD using Euclidean algorithm
        static long CalculateGCD(long a, long b)
        {
            while (b != 0)
            {
                long temp = b;
                b = a % b;
                a = temp;
            }
            return a;
        }
    }
}