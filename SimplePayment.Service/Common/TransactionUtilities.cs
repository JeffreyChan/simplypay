using System;
using System.Transactions;

namespace SimplePayment.Service
{
    public static class TransactionUtilities
    {
        public static TransactionScope CreateTransactionScopeAsync()
        {
            var options = new TransactionOptions { };
            options.IsolationLevel = IsolationLevel.ReadUncommitted;
            options.Timeout = new TimeSpan(0, 15, 0);
            return new TransactionScope(TransactionScopeOption.Required, options, TransactionScopeAsyncFlowOption.Enabled);
        }

        public static TransactionScope CreateTransactionScope()
        {
            var options = new TransactionOptions { };
            options.IsolationLevel = IsolationLevel.ReadUncommitted;

            return new TransactionScope(TransactionScopeOption.Required, options);
        }

        public static TransactionScope CreateTransactionScope(TransactionScopeOption option)
        {
            var options = new TransactionOptions { };
            options.IsolationLevel = IsolationLevel.ReadUncommitted;

            return new TransactionScope(option, options);
        }
    }
}
