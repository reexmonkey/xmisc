using System.Collections.Generic;
using FluentValidation;
using reexjungle.xmisc.infrastructure.concretes.extensions;
using reexmonkey.xmisc.core.linq;
using reexmonkey.xmisc.core.linq.extensions;
using Xunit;

namespace xmisc.tests.infrastructure
{

    public class CourseValidator : AbstractValidator<Course>
    {
        public CourseValidator(CascadeMode mode = CascadeMode.StopOnFirstFailure)
        {
            CascadeMode = mode;
            RuleFor(x => x.Code).Length(3, 7).When(x => !string.IsNullOrEmpty(x.Code));
            RuleFor(x => x.Title).Length(10, 20).When(x => !string.IsNullOrEmpty(x.Title));
        }
    }

    public class StudentValidatorA : AbstractValidator<Student>
    {
        public StudentValidatorA(CascadeMode mode = CascadeMode.StopOnFirstFailure)
        {
            CascadeMode = mode;
            RuleFor(x => x.Salary).Equal(0);
            RuleFor(x => x.Courses).SetCollectionValidator(new CourseValidator()).Unless(x => x.Courses.NullOrEmpty());
        }
    }

    public class StudentValidatorB : AbstractValidator<Student>
    {
        public StudentValidatorB(CascadeMode mode = CascadeMode.StopOnFirstFailure)
        {
            CascadeMode = mode;
            RuleFor(x => x.MatriculationId).Length(7, 12).Unless(x => string.IsNullOrEmpty(x.MatriculationId));
            RuleFor(x => x.Courses).SetCollectionValidator(new CourseValidator()).Unless(x => x.Courses.NullOrEmpty());
        }
    }


    public class TeacherValidator : AbstractValidator<Teacher>
    {
        public TeacherValidator(CascadeMode mode = CascadeMode.StopOnFirstFailure)
        {
            CascadeMode = mode;
            RuleFor(x => x.Salary).Equal(1000);
            RuleFor(x => x.Courses).SetCollectionValidator(new CourseValidator()).Unless(x => x.Courses.NullOrEmpty());
        }
    }

    public class UniversityValidator : AbstractValidator<University>
    {
        public UniversityValidator(CascadeMode mode = CascadeMode.StopOnFirstFailure)
        {
            CascadeMode = mode;
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
        public void TestUniversityValidation(CascadeMode mode = CascadeMode.StopOnFirstFailure)
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
