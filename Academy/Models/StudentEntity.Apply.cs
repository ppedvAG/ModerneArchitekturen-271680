using Academy.Events;

namespace Academy.Models;

public partial class StudentEntity : IMaterializable<StudentEntity>
{
    private StudentEntity Apply(StudentRegisteredEvent @event)
    {
        Id = @event.StudentId;
        FullName = @event.FullName;
        Email = @event.Email;
        DateOfBirth = @event.DateOfBirth;
        return this;
    }

    private StudentEntity Apply(StudentCourseEnrolledEvent @event)
    {
        if (Id == @event.StudentId)
        {
            EnrolledCourses.Add(@event.CourseName);
        }
        return this;
    }

    private StudentEntity Apply(StudentCourseDisenrolledEvent @event)
    {
        if (Id == @event.StudentId)
        {
            EnrolledCourses.Remove(@event.CourseName);
        }
        return this;
    }

    private StudentEntity Apply(StudentEmailUpdatedEvent @event)
    {
        if (Id == @event.StudentId)
        {
            Email = @event.Email;
        }
        return this;
    }

    public StudentEntity Apply(DomainEvent @event) => @event switch
    {
        StudentRegisteredEvent e => Apply(e),
        StudentCourseEnrolledEvent e => Apply(e),
        StudentCourseDisenrolledEvent e => Apply(e),
        StudentEmailUpdatedEvent e => Apply(e),
        _ => throw new ArgumentException($"Unknown event type: {@event.GetType().Name}")
    };
}
