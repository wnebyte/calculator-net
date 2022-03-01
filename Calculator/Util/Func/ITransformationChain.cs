using System;

namespace Calculator.Util.Func
{
    public interface ITransformationChain<T>
    {
        public void Add(Func<T, T> t);

        public T Transform(T t);
    }
}
