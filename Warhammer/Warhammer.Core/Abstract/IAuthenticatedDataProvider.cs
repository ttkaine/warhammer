using System;
using System.Collections;
using System.Collections.Generic;
using Warhammer.Core.Entities;

namespace Warhammer.Core.Abstract
{
    public interface IAuthenticatedDataProvider
    {
        ICollection<Person> MyPeople();
        int AddSessionLog(int sessionId, int personId, string name, string title, string description);
        int AddSession(string title, string name, string description, DateTime dateTime);
        int AddPerson(string shortName, string longName, string description);
        void ChangePicture(int id, byte[] data, string mimeType);
        Page UpdatePageDetails(int id, string shortName, string fullName, string description);    
        Page GetPage(int id);
        ICollection<Page> RecentPages();
        ICollection<Page> MyStuff();
        ICollection<Session> Sessions();
        ICollection<Person> People();
        ICollection<SessionLog> Logs();
        void RemoveProfileImage(int id);
        int AddPage(string shortName, string fullName, string description);
        ICollection<Place> Places();
        int AddPlace(string fullName, string shortName, string description, int? parentId);
        ICollection<Page> PossibleLinks(int id);
        void AddLink(int id, int addLinkTo);
        void RemoveLink(int id, int linkToDeleteId);
        void DeletePage(int id);
    }
}
