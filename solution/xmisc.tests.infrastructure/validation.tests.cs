using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using reexjungle.xmisc.foundation.concretes;
using reexjungle.xmisc.infrastructure.concretes.extensions;
using Xunit;

namespace xmisc.tests.infrastructure
{

    public class CourseValidator : AbstractValidator<Course>
    {
        public CourseValidator()
        {
            CascadeMode mode = CascadeMode.StopOnFirstFailure;
            RuleFor(x => x.Code).Length(3, 7).When(x => !string.IsNullOrEmpty(x.Code));
            RuleFor(x => x.Title).Length(10, 20).When(x => !string.IsNullOrEmpty(x.Title));
        }
    }

    public class StudentValidatorA : AbstractValidator<Student>
    {
        public StudentValidatorA()
        {
            RuleFor(x => x.Salary).Equal(0);
            RuleFor(x => x.Courses).SetCollectionValidator(new CourseValidator()).Unless(x => x.Courses.NullOrEmpty());
        }
    }

    public class StudentValidatorB : AbstractValidator<Student>
    {
        public StudentValidatorB()
        {
            RuleFor(x => x.MatriculationId).Length(7, 12).Unless(x => string.IsNullOrEmpty(x.MatriculationId));
            RuleFor(x => x.Courses).SetCollectionValidator(new CourseValidator()).Unless(x => x.Courses.NullOrEmpty());
        }
    }


    public class TeacherValidator : AbstractValidator<Teacher>
    {
        public TeacherValidator()
        {
            RuleFor(x => x.Salary).Equal(1000);
            RuleFor(x => x.Courses).SetCollectionValidator(new CourseValidator()).Unless(x => x.Courses.NullOrEmpty());
        }
    }

    public class UniversityValidator : AbstractValidator<University>
    {
        public UniversityValidator()
        {
            RuleFor(x => x.Members).SetCollectionValidators(
                new StudentValidatorA(), 
                new StudentValidatorA(),
                new StudentValidatorB(),
                new TeacherValidator());
        }
    }

    public class ValidationTests
    {
        [Fact]
        public void TestUniversityValidation()
        {
            var university = new University
            {
                Members = new List<Person>
                {
                    { new Teacher {Salary = 3000} },
                    { new Student {Salary = 100, MatriculationId = "123"} }
                }
            };

            var result = new UniversityValidator().Validate(university);
            Assert.NotEmpty(result.Errors);
            Assert.Equal(result.Errors.Count, 3);
           
        }
    }
}
