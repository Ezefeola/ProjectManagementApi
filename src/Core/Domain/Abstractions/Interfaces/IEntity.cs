namespace Core.Domain.Abstractions.Interfaces;
public interface IEntity
{
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime? DeletedAt { get; set; }
}
public interface IEntity<TId> : IEntity
{
    public TId Id { get; }
}