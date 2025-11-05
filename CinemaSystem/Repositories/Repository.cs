<<<<<<< HEAD
﻿using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System.Linq.Expressions;
using System.Threading;
=======
﻿using CinemaSystem.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
>>>>>>> e4955d5d839d0b832f284b280ce538c034a76a5f
using System.Threading.Tasks;

namespace CinemaSystem.Repositories
{
<<<<<<< HEAD
    public class Repository<T> where T : class
    {


        private ApplicationDbContext _context = new();
        private DbSet<T> _dbSet;

        public Repository()
        {
            _dbSet= _context.Set<T>();
        }


=======
    public class Repository<T> : IRepository<T> where T : class
    {
        private ApplicationDbContext _context;// = new();
        private DbSet<T> _dbSet;

        public Repository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

>>>>>>> e4955d5d839d0b832f284b280ce538c034a76a5f
        // CRUD

        public async Task<T> AddAsync(T entity, CancellationToken cancellationToken = default)
        {
<<<<<<< HEAD
            await _context.AddAsync(entity, cancellationToken);
            return entity;
        }

        //Update
        public void Update(T entity)
        {
            _context.Update(entity);

        }

        //Dellete
        public void Delete(T entity)
        {
            _context.Remove(entity);

        }
        //read Multiaple recordes 
=======
            var result = await _dbSet.AddAsync(entity, cancellationToken);
            return result.Entity;
        }
        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }
        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

>>>>>>> e4955d5d839d0b832f284b280ce538c034a76a5f
        public async Task<IEnumerable<T>> GetAsync(
            Expression<Func<T, bool>>? expression = null,
            Expression<Func<T, object>>[]? includes = null,
            bool tracked = true,
            CancellationToken cancellationToken = default)
        {
            var entities = _dbSet.AsQueryable();

<<<<<<< HEAD

=======
>>>>>>> e4955d5d839d0b832f284b280ce538c034a76a5f
            if (expression is not null)
                entities = entities.Where(expression);

            if (includes is not null)
            {
                foreach (var item in includes)
<<<<<<< HEAD
                {
                    entities = entities.Include(item);
                }
            }
           
            if (!tracked)
                entities = entities.AsNoTracking();


          return  await entities.ToListAsync(cancellationToken);
        }

        //read One recorde
=======
                    entities = entities.Include(item);
            }

            if (!tracked)
                entities = entities.AsNoTracking();

            //entities = entities.Where(e => e.Status);

            return await entities.ToListAsync(cancellationToken);
        }

>>>>>>> e4955d5d839d0b832f284b280ce538c034a76a5f
        public async Task<T?> GetOneAsync(
            Expression<Func<T, bool>>? expression = null,
            Expression<Func<T, object>>[]? includes = null,
            bool tracked = true,
<<<<<<< HEAD
            CancellationToken cancellationToken = default
            )
=======
            CancellationToken cancellationToken = default)
>>>>>>> e4955d5d839d0b832f284b280ce538c034a76a5f
        {
            return (await GetAsync(expression, includes, tracked, cancellationToken)).FirstOrDefault();
        }

<<<<<<< HEAD


        // SaveChanges
        public async Task CommitAsync(CancellationToken cancellationToken=default)
=======
        public async Task CommitAsync(CancellationToken cancellationToken = default)
>>>>>>> e4955d5d839d0b832f284b280ce538c034a76a5f
        {
            try
            {
                await _context.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
<<<<<<< HEAD
                Console.WriteLine($"Error  {ex.Message}");
            }
        }

        internal void Delete(IEnumerable< T> value)
        {
            throw new NotImplementedException();
        }

        //internal void Delete(IEnumerable<T?> values)
        //{
        //    throw new NotImplementedException();
        //}

        //internal void Delete(Task<MovieSubImage?> values)
        //{
        //    throw new NotImplementedException();
        //}
    }
}

=======
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

    }
}
>>>>>>> e4955d5d839d0b832f284b280ce538c034a76a5f
