using ConsolidatingKnowledge.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsolidatingKnowledge.DI
{
    public interface IStudentService
    {
        Student Get(int Id);
    }

   
}
