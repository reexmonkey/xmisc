using System;
using System.Collections.Generic;
using reexjungle.xmisc.foundation.concretes;

namespace xmisc.tests.infrastructure
{
    public class Person : IEquatable<Person>
    {
        public Person(string name, uint age, decimal salary)
        {
            Name = name;
            Age = age;
            Salary = salary;
        }

        public Person(string name, uint age)
        {
            Name = name;
            Age = age;
        }

        public Person(string name, decimal salary)
        {
            Name = name;
            Salary = salary;
        }

        public Person()
        {
        }

        public Guid LicenseKey { get; set; }

        public Fpi Code { get; set; }

        public string Name { get; set; }

        public uint Age { get; set; }

        public decimal Salary { get; set; }

        public bool Equals(Person other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(Name, other.Name) && Age == other.Age && Salary == other.Salary;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Person)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (Name != null ? Name.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (int)Age;
                hashCode = (hashCode * 397) ^ Salary.GetHashCode();
                return hashCode;
            }
        }

        public static bool operator ==(Person left, Person right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Person left, Person right)
        {
            return !Equals(left, right);
        }
    }

    public class Teacher : Person
    {
        public string School { get; set; }

        public List<Course> Courses { get; set; }

        public Teacher()
        {
            Salary = 1000;
            Courses = new List<Course>();
        }
    }

    public class Student : Person
    {
        public string School { get; set; }

        public string MatriculationId { get; set; }

        public double Gpa { get; set; }

        public List<Course> Courses;

        public Student()
        {
            Salary = 0;
            Courses = new List<Course>();
        }

    }

    public class Course
    {
        public string Code { get; set; }

        public string Title { get; set; }
    }

    public class University
    {
        public List<Person> Members { get; set; }

        public University()
        {
            Members =new List<Person>();
        }
    }
}