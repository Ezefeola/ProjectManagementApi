namespace Core.Domain.Abstractions.StronglyTypedIds;
public abstract record StronglyTypedGuidId<TId> : StronglyTypedId<Guid>
    where TId : StronglyTypedGuidId<TId>, new()
{
    protected StronglyTypedGuidId() : base(Guid.Empty) { }

    protected StronglyTypedGuidId(Guid value) : base(value) { }

    public static TId NewId()
    {
        return new TId()
        {
            Value = Guid.CreateVersion7() 
        };
    }

    public static TId NewEfId(Guid value)
    {
        return new TId() 
        {
            Value = value 
        };
    }

    public static DomainResult<TId> Create(Guid value)
    {
        if (value == Guid.Empty)
        {
            return DomainResult<TId>.Failure()
                                    .WithErrors(["Invalid ID"]);
        }

        TId id = new()
        {
            Value = value
        };

        return DomainResult<TId>.Success()
                                .WithValue(id);
    }
}