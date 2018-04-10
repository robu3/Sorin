using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sorin
{
    /// <summary>
    /// # MarkovChain
    /// 
    /// The name says it all.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class MarkovChain<T>
    {
        /// <summary>
        /// ## Initial
        /// 
        /// The initial state of the chain.
        /// </summary>
        public State<T> Initial
        {
            get;
            set;
        }

        /// <summary>
        /// ## Current
        /// 
        /// The current state of the chain.
        /// </summary>
        public State<T> Current
        {
            get;
            set;
        }

        /// <summary>
        /// ## Transition
        /// 
        /// Transitions from the current state to the next, returning the new state.
        /// The `Current` property is assigned the new state as well.
        /// </summary>
        /// <returns>The new state.</returns>
        public State<T> Transition()
        {
            if (Current == null)
            {
                throw new Exception("Current is null; please make sure that states in the chain return a valid state.");
            }

            Current = Current.Transition();
            return Current;
        }

        /// <summary>
        /// ## Simulate
        /// 
        /// Transitions `count` times, returning the results.
        /// </summary>
        /// <param name="count"></param>
        public IDictionary<State<T>, int> Simulate(int count)
        {
            Dictionary<State<T>, int> results = new Dictionary<State<T>, int>();

            for (int i = 0; i < count; i++)
            {
                State<T> s = Transition();
                results[s] = results.ContainsKey(s) ? results[s] + 1 : 1;
            }

            return results;
        }


        public MarkovChain(State<T> initial)
        {
            this.Initial = initial;
            this.Current = this.Initial;
        }
    }
}
