namespace Academy.Events;

public abstract class DomainEvent
{
    // Events treten immer zu einem bestimmten Zeitpunkt auf
    public DateTime CreatedAtUtc { get; set; }

    // Abfolge von Events als Stream eindeutig identifizieren
    public abstract Guid StreamId { get; }
}

public class StudentRegisteredEvent : DomainEvent
{
    public required Guid StudentId { get; init; }

    public required string FullName { get; init; }

    public required string Email { get; init; }

    public required DateTime DateOfBirth { get; init; }

    public override Guid StreamId => StudentId;
}

public class StudentEmailUpdatedEvent: DomainEvent
{
    public required Guid StudentId { get; init; }

    public required string Email { get; init; }

    public override Guid StreamId => StudentId;
}

public class StudentCourseEnrolledEvent : DomainEvent
{
    public required Guid StudentId { get; init; }
    public required string CourseName { get; init; }
    public override Guid StreamId => StudentId;
}

public class StudentCourseDisenrolledEvent : DomainEvent
{
    public required Guid StudentId { get; init; }
    public required string CourseName { get; init; }
    public override Guid StreamId => StudentId;
}