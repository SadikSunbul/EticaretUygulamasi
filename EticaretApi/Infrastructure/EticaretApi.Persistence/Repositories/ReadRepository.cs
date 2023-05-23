using EticaretApi.Application.Repositories;
using EticaretApi.Domain.Entities.Common;
using EticaretApi.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EticaretApi.Persistence.Repositories
{
    public class ReadRepository<T> : IReadRepository<T> where T : BaseEntity
    {

        private readonly EticaretApiDbContext _context; //IoC den gelicek 
        public ReadRepository(EticaretApiDbContext context)
        {
            context = _context;
        }
        //burası IRepository den geldi
        public DbSet<T> Table => _context.Set<T>();

        public IQueryable<T> GetAll()
            => Table; //tum tanloları getırıcek

        public IQueryable<T> GetWhere(Expression<Func<T, bool>> method)
            => Table.Where(method);

        public async Task<T> GetSingleAsync(Expression<Func<T, bool>> method)
            => await Table.FirstOrDefaultAsync(method);


        public async Task<T> GetByIdAsync(string id)
            =>await Table.FirstOrDefaultAsync(data => data.Id == Guid.Parse(id)); //Id yı guid e pars ettık esıtse dondur dedık 
    }
}
