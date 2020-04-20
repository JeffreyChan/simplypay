using System;


namespace SimplePayment.Service
{
    public class BusinessException : Exception
    {
        public BusinessException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }

    public class SystemFailureException : BusinessException
    {
        public SystemFailureException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }

    public class RuntimeException : BusinessException
    {
        public RuntimeException(string message, Exception innerException)
            : base(message, innerException)
        { }

        public RuntimeException(string message)
            : base(message, null)
        { }
    }
}
