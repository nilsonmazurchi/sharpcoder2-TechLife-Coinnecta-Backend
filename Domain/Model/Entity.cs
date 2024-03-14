using System.ComponentModel.DataAnnotations;

namespace sharpcoder2_TechLife_Coinnecta_Backend.Domain.Model;

public interface IEntity
{
   public int Id { get; set; }
}

public abstract class Entity : IEntity
{
  [Key]
  [Required]
  public int Id { get; set; }
}
