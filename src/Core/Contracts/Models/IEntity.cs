namespace Core.Contracts.Models;
public interface IEntity<TId>
{
    public TId Id { get; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime? DeletedAt { get; set; }
}