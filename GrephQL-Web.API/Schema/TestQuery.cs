using Bogus;
using HotChocolate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrephQL_Web.API.Schema
{
    public class TestQuery
    {
        private Faker<InstructorType> _instructorFaker;
        private Faker<StudentType> _studentFaker;
        private Faker<CourseType> _courseFaker;

        public TestQuery()
        {
            _instructorFaker = new Faker<InstructorType>()
                .RuleFor(c => c.Id, f => Guid.NewGuid())
                .RuleFor(c => c.FirstName, f => f.Name.FirstName())
                .RuleFor(c => c.LastName, f => f.Name.LastName())
                .RuleFor(c => c.Salary, f => f.Random.Double(0, 100000));

            _studentFaker = new Faker<StudentType>()
                .RuleFor(c => c.Id, f => Guid.NewGuid())
                .RuleFor(c => c.FirstName, f => f.Name.FirstName())
                .RuleFor(c => c.LastName, f => f.Name.LastName())
                .RuleFor(c => c.GPA, f => f.Random.Double(1, 4));

            _courseFaker = new Faker<CourseType>()
                .RuleFor(c => c.Id, f => Guid.NewGuid())
                .RuleFor(c => c.Name, f => f.Name.JobTitle())
                .RuleFor(c => c.Subject, f => f.PickRandom<Subject>())
                .RuleFor(c => c.Instructor, f => _instructorFaker.Generate())
                .RuleFor(c => c.Students, f => _studentFaker.Generate(3));
        }



        public IEnumerable<CourseType> GetCourses()
        {
            return _courseFaker.Generate(5);

            //return new List<CourseType>
            //{
            //    new CourseType
            //    {
            //        Id = Guid.NewGuid(),
            //        Name = "taekyung",
            //        Subject = Subject.Mathematics,
            //        Instructor = new InstructorType
            //        {
            //            Id = Guid.NewGuid(),
            //        Name = "taekyung",
            //        Subject = Subject.Mathematics,
            //        }
            //    }
            //};
        }

        [GraphQLName("courseById")]
        public async Task<CourseType> GetCourseTypeByIdAsync(Guid id)
        {
            await Task.Delay(1000);
            
            CourseType course = _courseFaker.Generate();

            course.Id = id;

            return course;
        }
        
        [GraphQLDeprecated("This query is deprecated")]
        public string Descriptions { get; set; } = "Learn GraphQL API in .Net 5 with Hot Chocolate";
    }
}
