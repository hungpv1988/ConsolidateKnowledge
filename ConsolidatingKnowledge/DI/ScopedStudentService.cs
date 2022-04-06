using ConsolidatingKnowledge.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsolidatingKnowledge.DI
{
    public class ScopedStudentService : IStudentService
    {
        private readonly List<Student> _studentList;
        private readonly Random _random;
        private int _defaultStudentId = 0;
        public ScopedStudentService()
        {
            _studentList = new List<Student>()
            {
                new Student()
                {
                    ID = 1,
                    LastName = "Hung"
                },
                new Student()
                {
                    ID = 2,
                    LastName = "Hieu"
                },
                new Student()
                {
                    ID = 3,
                    LastName = "Hoa"
                },
                new Student()
                {
                    ID = 4,
                    LastName = "Huyen"
                },
                new Student()
                {
                    ID = 5,
                    LastName = "Hang"
                },
            };

            _random = new Random();
        }

        public Student Get(int Id)
        {
            var random = new Random();

            if (Id != Constants.NoStudentIdExist) 
            {
                return _studentList[Id];
            }

            // gurantee that during the request scope, no new instance of ScopedStudentService is created and therefore, no change in _defaultStudentId
            if (_defaultStudentId == 0)
            {
                _defaultStudentId = random.Next(0, _studentList.Count-1);
                return _studentList[_defaultStudentId];
            }

            return _studentList[_defaultStudentId];
        }
    }
}
