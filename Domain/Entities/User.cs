using Sarafi.Domain.Common;
using Sarafi.Domain.Enums;

namespace Sarafi.Domain.Entities; 
public class User : AuditableEntity
{
    public User()
    {
        UserRoles = new List<UserRole>();
        Accounts = new List<Account>();
        ConnectionsFrom = new List<Connection>();
        ConnectionsTo = new List<Connection>();
        Activities = new List<Activity>();
        Notifications = new List<Notification>();
    }

    public string Firstname { private set; get; }
    public string Lastname { private set; get; }
    public string Fathername { private set; get; }
    public string? Email { private set; get; }
    public string Username { private set; get; }
    public string Password { private set; get; }
    public string Salt { private set; get; }
    public bool IsLocked { private set; get; } = false;
    public string? RefreshToken { get; private set; }
    public DateTime DateOfBirth { private set; get; }
    public DateTime ActivationDate { private set; get; } = DateTime.Now;
    public DateTime ExpirationDate { private set; get; }
    public string? Mobile1 { private set; get; }
    public string? Mobile2 { private set; get; }
    public string? NationalIDNo { private set; get; }
    public UserType UserType { private set; get; }
    public Country Country { private set; get; }
    public long ProvinceId { private set; get; }
    public virtual Province Province { private set; get; }
    public byte[]? Signature { private set; get; }
    public byte[]? FingerPrint { private set; get; }
    public string? Photo { private set; get; }
    public string? NationalIDPhoto { private set; get; }
    public virtual ICollection<UserRole> UserRoles { private set; get; }
    public virtual ICollection<Account> Accounts { private set; get; }
    public virtual ICollection<Connection> ConnectionsFrom { private set; get; }
    public virtual ICollection<Connection> ConnectionsTo { private set; get; }
    public virtual ICollection<Activity> Activities {   private set; get; }
    public virtual ICollection<Notification> Notifications { private set; get; }

    public override string ToString() => $"{Firstname} {Lastname}";

    public User(long id, string firstname, string lastname, string fathername, string? email, string username, string password, string salt, string? refreshToken, DateTime dateOfBirth, DateTime activationDate, DateTime expirationDate, string? mobile1, string? mobile2, string? nationalIDNo, UserType userType, Country country, long provinceId)
    {
        SetId(id);
        Firstname = firstname;
        Lastname = lastname;
        Fathername = fathername;
        Email = email;
        Username = username;
        Password = password;
        Salt = salt;
        RefreshToken = refreshToken;
        DateOfBirth = dateOfBirth;
        ActivationDate = activationDate;
        ExpirationDate = expirationDate;
        Mobile1 = mobile1;
        Mobile2 = mobile2;
        NationalIDNo = nationalIDNo;
        UserType = userType;
        ProvinceId = provinceId;


        UserRoles = new List<UserRole>();
        Accounts = new List<Account>();
        ConnectionsFrom = new List<Connection>();
        ConnectionsTo = new List<Connection>();
        Activities = new List<Activity>();
        Notifications = new List<Notification>();
    }

}
