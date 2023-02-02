using Sarafi.Domain.Common;
using Sarafi.Domain.Enums;

namespace Sarafi.Domain.Entities; 
public class User : AuditableEntity
{
    public string Firstname { set; get; }
    public string Lastname { set; get; }
    public string Fathername { set; get; }
    public string? Email { set; get; }
    public string Username { set; get; }
    public string Password { set; get; }
    public string Salt { set; get; }
    public bool IsLocked { set; get; } = false;
    public string RefreshToken { get; set; }
    public DateTime DateOfBirth { set; get; }
    public DateTime ActivationDate { set; get; } = DateTime.Now;
    public DateTime ExpirationDate { set; get; }
    public string? Mobile1 { set; get; }
    public string? Mobile2 { set; get; }
    public string? NationalIDNo { set; get; }
    public UserType UserType { set; get; }
    public Country Country { set; get; }
    public long ProvinceId { set; get; }
    public virtual Province Province { set; get; }
    public byte[]? Signature { set; get; }
    public byte[]? FingerPrint { set; get; }
    public string? Photo { set; get; }
    public string? NationalIDPhoto { set; get; }
    public virtual ICollection<UserRole> UserRoles { set; get; }
    public virtual ICollection<Account> Accounts { set; get; }
    public virtual ICollection<Connection> ConnectionsFrom { set; get; }
    public virtual ICollection<Connection> ConnectionsTo { set; get; }
    public virtual ICollection<Activity> Activities { set; get; }
    public virtual ICollection<Notification> Notifications { set; get; }

    public override string ToString() => $"{Firstname} {Lastname}";
}
