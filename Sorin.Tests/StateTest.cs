using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sorin;
using System.Collections.Generic;

namespace Sorin.Tests
{
    [TestClass]
    public class StateTest
    {
        [TestMethod]
        public void Transition()
        {
            int seed = 2018;
            State<string> sticky = new State<string>("sticky", seed);
            State<string> coin = new State<string>("stateB", seed); 

            sticky.AddLink(new Link<string>(coin, .2));
            sticky.AddLink(new Link<string>(sticky, .8));
            coin.AddLink(new Link<string>(coin, .5));
            coin.AddLink(new Link<string>(sticky, .5));

            Dictionary<State<string>, int> counter = new Dictionary<State<string>, int>();
            for (int i = 0; i < 100; i++)
            {
                State<string> s = sticky.Transition();
                counter[s] = (counter.ContainsKey(s) ? counter[s] : 0) + 1;
            }

            int total = counter[sticky] + counter[coin];

            Assert.AreEqual(100, total);
            Assert.IsTrue(counter[sticky] > counter[coin]);
            Assert.AreEqual(79, counter[sticky]);
            Assert.AreEqual(21, counter[coin]);
        }
    }
}
