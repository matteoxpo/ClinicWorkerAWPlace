using System.Text.Json.Serialization;

namespace Domain.Entities.People;

public class Client
{
    public Client(string name, string surname, DateTime birthTime,  string id, IEnumerable<ReferenceForAnalysis>? analyses = null,
        IEnumerable<Appointment>? appointments = null, string? complaints = null, DateTime? meetTime = null)
    {
        MeetTime = meetTime ?? new DateTime();
        Name = new string(name);
        Surname = new string(surname);
        DateOfBirth = birthTime;
        Analyzes = analyses is not null ? new List<ReferenceForAnalysis>(analyses) : new List<ReferenceForAnalysis>();
        Appointments = appointments is not null ? new List<Appointment>(appointments) : new List<Appointment>();
        Id = id;
        Complaints = new string(complaints ?? "complaints");
    }


    public string Name { get; }
    public string Surname { get; }
    public DateTime DateOfBirth { get; }
    public string Id { get; }

    [JsonIgnore] public DateTime MeetTime { get; }
    [JsonIgnore] public string Complaints { get; }
    [JsonIgnore] public IEnumerable<ReferenceForAnalysis> Analyzes { get; }
    [JsonIgnore] public IEnumerable<Appointment> Appointments { get; }

    public override string ToString()
    {
        return string.Join(" ",
            Name,
            Surname,
            MeetTime.Equals(new DateTime(0))
                ? "\nДата рождения: " + DateOfBirth.ToString("MM/dd/yyyy")
                : "\nВремя записи: " + MeetTime);
    }
}