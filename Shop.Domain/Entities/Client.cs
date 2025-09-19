using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;
using System.Text;

namespace Shop.Domain.Entities;

[Table("Clients", Schema = "client")]
public class Client : BaseEntity
{
    [Required]
    [Column(TypeName = "character varying(200)")]
    public string Name { get; private set; }
    
    [Required]
    [Column(TypeName = "character varying(200)")]
    public string PasswordHash { get; private set; }
    
    [Required]
    [Column(TypeName = "character varying(200)")]
    public string Email { get; private set; }
    
    public Client() {}

    public Client(string name, string password, string email)
    {
        var hash = MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(password.ToCharArray()));
        string passwordHash = String.Empty;
        foreach(byte h in hash)
        {
            passwordHash += h.ToString("x2");
        }
        
        Id = Guid.NewGuid();
        Name = name;
        PasswordHash = passwordHash;
        Email = email;
    }
}