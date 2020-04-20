using System;
using System.Data.Entity.Validation;
using System.Text;
using System.Threading.Tasks;

namespace SimplePayment.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        int Commit();
        Task<int> CommitAsync();
    }

    /// <summary>
    /// The Entity Framework implementation of IUnitOfWork
    /// </summary>
    public sealed class UnitOfWork : IUnitOfWork
    {
        /// <summary>
        /// The DbContext
        /// </summary>
        private SimplePaymentContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the UnitOfWork class.
        /// </summary>
        /// <param name="context">The object context</param>
        public UnitOfWork(SimplePaymentContext context)
        {
            _dbContext = context;
        }



        /// <summary>
        /// Saves all pending changes
        /// </summary>
        /// <returns>The number of objects in an Added, Modified, or Deleted state</returns>
        public int Commit()
        {
            // Save changes with the default options
            return _dbContext.SaveChanges();
        }

        public async Task<int> CommitAsync()
        {
            try
            {
                var commitResult = await _dbContext.SaveChangesAsync();

                return commitResult;
            }
            catch (DbEntityValidationException e)
            {
                var dbErrors = new StringBuilder();
                foreach (var eve in e.EntityValidationErrors)
                {
                    dbErrors.AppendFormat(
                        "Entity of type \"{0}\" in state \"{1}\" has the following validation errors:\r\n",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        dbErrors.AppendFormat("\t- Property: \"{0}\", Error: \"{1}\"\r\n", ve.PropertyName,
                            ve.ErrorMessage);
                    }
                }
                throw new Exception(dbErrors.ToString());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        /// <summary>
        /// Disposes the current object
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Disposes all external resources.
        /// </summary>
        /// <param name="disposing">The dispose indicator.</param>
        private void Dispose(bool disposing)
        {
            if (!disposing) return;
            if (_dbContext == null) return;
            _dbContext.Dispose();
            _dbContext = null;
        }
    }
}
