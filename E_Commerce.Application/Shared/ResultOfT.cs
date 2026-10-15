using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Shared
{
    public class Result<TValue> : Result
    {
        private readonly TValue? _value;

        protected Result(TValue? value, bool isSuccess, Error error)
            : base(isSuccess, error)
        {
            _value = value;
        }

        public TValue Value => IsSuccess
            ? _value!
            : throw new InvalidOperationException("The value of a failure result cannot be accessed.");

        public static Result<TValue> Success(TValue value) => new(value, true, Error.None);
        public new static Result<TValue> Failure(Error error) => new(default, false, error);
        public static Result<IReadOnlyCollection<TValue>> Success<TValue>(IReadOnlyCollection<TValue> value)
        => Result<IReadOnlyCollection<TValue>>.Success(value);

        public static Result<IReadOnlyCollection<TValue>> Failure<TValue>(Error error)
            => Result<IReadOnlyCollection<TValue>>.Failure(error);

        // Implicit conversions for cleaner return statements
        public static implicit operator Result<TValue>(TValue? value) =>
            value is not null ? Success(value) : Failure(Error.NullValue);

        public static implicit operator Result<TValue>(Error error) => Failure(error);
    }
}
