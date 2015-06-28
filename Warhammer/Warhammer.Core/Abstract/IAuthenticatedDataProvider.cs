﻿using System;
using System.Collections.Generic;
using Warhammer.Core.Entities;

namespace Warhammer.Core.Abstract
{
    public interface IAuthenticatedDataProvider
    {
        ICollection<Person> MyPeople();
        int AddSessionLog(int sessionId, int personId, string title, string description);
        int AddSession(string title, string name, string description, DateTime dateTime);
        int AddPerson(string shortName, string longName, string description);
        void ChangePicture(int id, byte[] data, string mimeType);
        Page UpdatePageDetails(int id, string shortName, string fullName, string description);    
        Page GetPage(int id);
    }
}
