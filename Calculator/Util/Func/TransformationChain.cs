using System;
using System.Collections.Generic;

namespace Calculator.Util.Func
{
    public class TransformationChain<T> : ITransformationChain<T>
    {

        private readonly IList<Func<T, T>> chain;

        public TransformationChain()
        {
            this.chain = new List<Func<T, T>>();
        }

        public void Add(Func<T, T> transf)
        {
            chain.Add(transf);
        }

        public T Transform(T t)
        {
            foreach (Func<T, T> transf in chain)
            {
                t = transf.Invoke(t);
            }

            return t;
        }
    }
}
