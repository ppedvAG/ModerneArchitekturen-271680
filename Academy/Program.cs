using Academy.Events;
using Academy.Models;
using Academy.Store;

namespace Academy
{
    internal class Program
    {
        const string StudentId = "B74DBFC5-35D9-4E5E-B169-C24F9DB04B33";

        static readonly InMemoryEventStore<StudentEntity> _eventStore = new();

        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            var studentId = Guid.Parse(StudentId);

            Console.WriteLine("Persisting some student events...");
            PersistSomeStudentEvents(studentId);

            Console.WriteLine("\nMaterialize student events...");
            MaterializeEntity(studentId);

            Console.WriteLine("\nGet student from projection:");
            var studentProjection = _eventStore.GetEntityProjection(studentId);
            if (studentProjection != null)
            {
                PrintInfo(studentProjection);
            }
        }

        private static void MaterializeEntity(Guid studentId)
        {
            StudentEntity student = _eventStore.GetEntity(studentId);
            PrintInfo(student);
        }

        private static void PrintInfo(StudentEntity student)
        {
            Console.WriteLine($"{student.FullName} ({student.Email}) besucht folgende Kurse: {string.Join(", ", student.EnrolledCourses)}");
        }

        private static void PersistSomeStudentEvents(Guid studentId)
        {
            var registrationEvent = new StudentRegisteredEvent
            {
                StudentId = studentId,
                FullName = "Bugs Bunny",
                Email = "bugs@bunny.com",
                DateOfBirth = new DateTime(1940, 7, 27)
            };
            _eventStore.Append(registrationEvent);
            Console.WriteLine($"Event persisted: {registrationEvent.GetType().Name}");

            var enrollmentEvent = new StudentCourseEnrolledEvent
            {
                StudentId = studentId,
                CourseName = "Animation 101"
            };
            _eventStore.Append(enrollmentEvent);
            Console.WriteLine($"Event persisted: {enrollmentEvent.GetType().Name}");

            var emailUpdateEvent = new StudentEmailUpdatedEvent
            {
                StudentId = studentId,
                Email = "whats.up@doc.de",
            };
            _eventStore.Append(emailUpdateEvent);
            Console.WriteLine($"Event persisted: {emailUpdateEvent.GetType().Name}");
        }
    }
}
