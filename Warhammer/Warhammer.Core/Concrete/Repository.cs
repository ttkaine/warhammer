﻿using System;
using System.Data.Entity;
using System.Linq;
using Warhammer.Core.Abstract;
using Warhammer.Core.Entities;

namespace Warhammer.Core.Concrete
{
    /// <summary>
    /// No business logic or filtering should happen in an implemntation of IRepository
    /// Well understood boiler plate code only.
    /// </summary>
    public class Repository : IRepository, IDisposable
    {
        private readonly WarhammerDataEntities _entities = new WarhammerDataEntities();

        #region Accessors

        //public IQueryable<ChangeLog> ChangeLogs()
        //{
        //    return _entities.ChangeLogs;
        //}

        public IQueryable<Person> People()
        {
            return _entities.Pages.OfType<Person>();
        }

        public IQueryable<Player> Players()
        {
            return _entities.Players;
        }

        public int Save(Page page)
        {
            if (page.Id == 0)
            {
                _entities.Pages.Add(page);
            }
            else
            {
                _entities.Entry(page).State = EntityState.Modified;
            }
            _entities.SaveChanges();

            return page.Id;
        }

        public IQueryable<Page> Pages()
        {
            return _entities.Pages;
        }

        #endregion

        #region Save

        //public int Save(Example example)
        //{
        //    if (example.Id == 0)
        //    {
        //        _entities.Examples.Add(example);
        //    }
        //    else
        //    {
        //        _entities.Entry(example).State = EntityState.Modified;       
        //    } 
        //    _entities.SaveChanges();

        //    return example.Id;
        //}

        #endregion

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_entities != null)
                {
                    _entities.Dispose();
                }
            }
        }
    }
}
