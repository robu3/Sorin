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
            State<string> coin = new State<string>("coin", seed); 

            sticky.AddLink(new Link<string>(coin, .2));
            sticky.AddLink(new Link<string>(sticky, .8));
            coin.AddLink(new Link<string>(coin, .5));
            coin.AddLink(new Link<string>(sticky, .5));

            Dictionary<State<string>, int> counter = new Dictionary<State<string>, int>();
            State<string> current = sticky;
            for (int i = 0; i < 100; i++)
            {
                current = current.Transition();
                counter[current] = (counter.ContainsKey(current) ? counter[current] : 0) + 1;
            }

            int total = counter[sticky] + counter[coin];

            Assert.AreEqual(100, total);
            Assert.IsTrue(counter[sticky] > counter[coin]);
            Assert.AreEqual(65, counter[sticky]);
            Assert.AreEqual(35, counter[coin]);
        }
    }
}
