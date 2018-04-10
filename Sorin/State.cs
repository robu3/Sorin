using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sorin
{
    /// <summary>
    /// # State
    /// 
    /// An individual state in a Markov chain.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class State<T>
    {
        private List<Link<T>> _links;
        private Random _rand;
        private double _totalProbability;
        private bool _isDirty;

        /// <summary>
        /// ## Data
        /// 
        /// The piece of data associated with this state.
        /// </summary>
        public T Data
        {
            get;
            set;
        }

        /// <summary>
        /// ## AddLink
        /// 
        /// Adds a link to another state.
        /// </summary>
        /// <param name="link"></param>
        public void AddLink(Link<T> link)
        {
            if (_totalProbability + link.Probability > 1)
            {
                throw new Exception("Total probability of all links added is > 1.");
            }

            _links.Add(link);
            _totalProbability += link.Probability;
            _isDirty = true;
        }

        /// <summary>
        /// ## Transition
        /// 
        /// Transitions to the next state.
        /// </summary>
        /// <returns></returns>
        public State<T> Transition()
        {
            // if necessary, sort by probability value ascending
            if (_isDirty)
            {
                _links.OrderBy(x => x.Probability);
            }

            // generate a random value
            // iterate through links until value is less than the probability 
            // and return the corresponding state
            double p = _rand.NextDouble();

            State<T> next = null;
            double total = 0;
            foreach (Link<T> link in _links)
            {
                if (p <= link.Probability + total)
                {
                    next = link.ToState;
                    break;
                }
                total += link.Probability;
            }

            return next;
        }

        public State(T data, int? seed = null)
        {
            this.Data = data;

            _links = new List<Link<T>>();
            _isDirty = false;

            _rand = seed.HasValue ? new Random(seed.Value) : new Random();
        }
    }
}
