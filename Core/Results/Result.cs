using System.Collections.Generic;

namespace Core.Results
{
    namespace Application.Common
    {
        public class Result
        {
            public bool IsSuccess { get; }
            public bool IsFailure => !IsSuccess;
            public Error Error { get; }

            protected Result(bool isSuccess, Error error)
            {
                if (isSuccess && error != null)
                    throw new InvalidOperationException("Successful result must not have an error.");
                if (!isSuccess && error == null)
                    throw new InvalidOperationException("Failed result must have an error.");

                IsSuccess = isSuccess;
                Error = error;
            }

            public static Result Success() => new(true, null);
            public static Result Failure(Error error) => new(false, error);
        }

        public class Result<T> : Result
        {
            public T Value { get; }

            protected Result(T value, bool isSuccess, Error error)
                : base(isSuccess, error)
            {
                Value = value;
            }

            public static Result<T> Success(T value) => new(value, true, null);

            public static new Result<T> Failure(Error error) => new(default, false, error);
        }
    }

}