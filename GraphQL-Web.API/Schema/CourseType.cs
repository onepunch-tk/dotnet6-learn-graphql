using HotChocolate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrephQL_Web.API.Schema
{
    public enum Subject
    {
        Mathematics,
        Science,
        History
    }
    
    public class CourseType
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Subject Subject { get; set; }

        [GraphQLNonNullType]
        public InstructorType Instructor { get; set; }
        public IEnumerable<StudentType> Students { get; set; }

        public string Description()
        {
            return $"{Name}: This is a Course";
        }
    }
}
