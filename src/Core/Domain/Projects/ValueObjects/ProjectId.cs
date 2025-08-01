﻿using Core.Domain.Abstractions.StronglyTypedIds;
namespace Core.Domain.Projects.ValueObjects;
public sealed record ProjectId : StronglyTypedGuidId<ProjectId>
{
    public ProjectId() { }
}