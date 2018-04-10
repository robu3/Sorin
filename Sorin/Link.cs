using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sorin
{
    /// <summary>
    /// # Link
    /// 
    /// The link between two states in a Markov chain, with the associated probablity of change.
    /// </summary>
    public class Link<T>
    {
        public State<T> ToState
        {
            get;
            set;
        }

        public double Probability
        {
            get;
            set;
        }

        public Link(State<T> s, double p)
        {
            this.ToState = s;
            this.Probability = p;
        }
    }
}
