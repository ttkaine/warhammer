﻿using System;
using System.Collections.Generic;
using System.Linq;
using Sample.Core.Abstract;
using Warhammer.Core.Abstract;
using Warhammer.Core.Entities;

namespace Warhammer.Core.Concrete
{
    public class AuthenticatedDataProvider : IAuthenticatedDataProvider
    {
        private readonly IAuthenticatedUserProvider _authenticatedUser;
        private readonly IRepository _repository;
        private readonly IModelFactory _factory;

        public AuthenticatedDataProvider(IAuthenticatedUserProvider authenticatedUser, IRepository repository, IModelFactory factory)
        {
            _authenticatedUser = authenticatedUser;
            _repository = repository;
            _factory = factory;
        }

        public Player CurrentPlayer
        {
            get
            {

                if (!_authenticatedUser.UserIsAuthenticated)
                {
                    throw new Exception("User is not Authenticated");
                }
                return _repository.Players().FirstOrDefault(p => p.UserName == _authenticatedUser.UserName);
            }
        }

        public ICollection<Person> MyPeople()
        {
            return _repository.People().Where(p => p.Player.UserName == _authenticatedUser.UserName).ToList();
        }

        public int AddSessionLog(int sessionId, int personId, string title, string description)
        {
            SessionLog session = new SessionLog
            {
                ShortName = title,
                FullName = title,
                Description = description,
                SessionId = sessionId,
                PersonId = personId
            };
            return Save(session);
        }

        public int AddSession(string title, string name, string description, DateTime date)
        {
            Session session = new Session
            {
                ShortName = name,
                FullName = title,
                Description = description,
                DateTime = date
            };
            return Save(session);
        }

        public int AddPerson(string shortName, string longName, string description)
        {
            Person session = new Person
            {
                ShortName = shortName,
                FullName = longName,
                Description = description,
            };
            return Save(session);
        }


        public void ChangePicture(int id, byte[] data, string mimeType)
        {
            Page page = _repository.Pages().FirstOrDefault(p => p.Id == id);
            if (page != null)
            {
                page.ImageData = data;
                page.ImageMime = mimeType;
                Save(page);
            }
        }

        public Page UpdatePageDetails(int id, string shortName, string fullName, string description)
        {
            Page existingPage = _repository.Pages().FirstOrDefault(p => p.Id == id);
            if (existingPage != null)
            {
                existingPage.ShortName = shortName;
                existingPage.FullName = fullName;
                existingPage.Description = description;
            }
            Save(existingPage);
            return existingPage;
        }

        public int Save(Page page)
        {
            Page existingPage = _repository.Pages().FirstOrDefault(p => p.Id == page.Id);

            if (existingPage == null)
            {
                page.Created = DateTime.Now;
                page.CreatedById = CurrentPlayer.Id;
            }

            page.Modified = DateTime.Now;
            page.ModifedById = CurrentPlayer.Id;

            return _repository.Save(page);
        }

        public Page GetPage(int id)
        {
            return _repository.Pages().FirstOrDefault(p => p.Id == id);
        }

        public ICollection<Page> RecentPages()
        {
            return _repository.Pages().OrderByDescending(p => p.Modified).Take(20).ToList();
        }

        public ICollection<Page> MyStuff()
        {
            return _repository.Pages().Where(p => p.CreatedById == CurrentPlayer.Id).OrderByDescending(p => p.Created).ToList();
        }

        public ICollection<Session> Sessions()
        {
            return _repository.Pages().OfType<Session>().ToList();
        }

        public ICollection<Person> People()
        {
            return _repository.Pages().OfType<Person>().ToList();
        }

        public ICollection<SessionLog> Logs()
        {
            return _repository.Pages().OfType<SessionLog>().ToList();
        }
    }
}
