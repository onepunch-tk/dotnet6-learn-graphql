using HotChocolate;
using HotChocolate.AspNetCore.Authorization;
using HotChocolate.Subscriptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace GrephQL_Web.API.Schema
{
    public class TestMutation
    {
        private readonly List<CourseResult> _courses;

        public TestMutation()
        {
            _courses = new List<CourseResult>();
        }

        
        public async Task<CourseResult> CreateCourse(CourseRequest request, [Service] ITopicEventSender topicEventSender)
        {
            CourseResult result = new CourseResult
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Subject = request.Subject,
                InstructorId = request.InstructorId
            };

            _courses.Add(result);
            await topicEventSender.SendAsync(nameof(Subscription.CourseCreate), result);

            return result;
        }

        
        public async Task<CourseResult> UpdateCourse(Guid id, CourseRequest request, [Service] ITopicEventSender topicEventSender)
        {
            var course = _courses.Find(c => c.Id == id);

            if (course == null)
            {
                throw new GraphQLException(new Error("Course not found", "COURSE_NOT_FOUND"));
            }

            course.Name = request.Name;
            course.Subject = request.Subject;
            course.InstructorId = request.InstructorId;

            string updateCourseTopic = $"{course.Id}_{nameof(Subscription.CourseUpdate)}";
            await topicEventSender.SendAsync(updateCourseTopic, course);

            return course;
        }

        public bool DeleteCourese(Guid id)
        {
            return _courses.RemoveAll(c => c.Id == id) >= 1;
        }

    }
}
