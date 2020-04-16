using Domain.Contracts;
using Domain.Repositories;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Base
{
    public sealed class UnitOfWork : IUnitOfWork
    {
        private IDbContext _dbContext;

        private IEmpleadoRepository _empleadoRepository;
        private ICreditoRepository _creditooRepository;
        public IEmpleadoRepository EmpleadoRepository { get { return _empleadoRepository ?? (_empleadoRepository = new EmpleadoRepository(_dbContext)); } }
        public ICreditoRepository CreditoRepository { get { return _creditooRepository ?? (_creditooRepository = new CreditoRepository(_dbContext)); } }

        public UnitOfWork(IDbContext context)
        {
            _dbContext = context;
        }
        public int Commit()
        {
            return _dbContext.SaveChanges();
        }
        public void Dispose()
        {
            Dispose(true);
        }
        /// <summary>
        /// Disposes all external resources.
        /// </summary>
        /// <param name="disposing">The dispose indicator.</param>
        private void Dispose(bool disposing)
        {
            if (disposing && _dbContext != null)
            {
                ((DbContext)_dbContext).Dispose();
                _dbContext = null;
            }
        }

    }
}
