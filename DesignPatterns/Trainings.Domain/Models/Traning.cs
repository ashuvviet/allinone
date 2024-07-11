using System;

namespace Trainings.Domain
{
    public class Training
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public Course CourseName { get; set; }

    }

    public class Course
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
