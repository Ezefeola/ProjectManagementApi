# 🚀 .NET 9 API Challenge — Project Management with Authentication

## Overview

Build a **RESTful API** for managing projects, collaborators, and assignments, featuring:

- Authentication & Authorization (JWT)  
- EF Core with strongly typed IDs (`Guid.CreateVersion7()` based)  
- Clean domain modeling & validation  
- LINQ querying with filtering, sorting, and pagination  
- Unit tests for core business logic  

---

## Domain Entities

| **Entity**        | **Description**                                                        |
|-------------------|------------------------------------------------------------------------|
| **User**          | App users with roles (`Admin`, `Manager`, `Collaborator`). Linked optionally to a Collaborator profile. |
| **Collaborator**   | People who can be assigned to projects and assignments.                |
| **Project**       | Contains assignments and collaborators working on it.                  |
| **Assignment**    | Work items within a project assigned to collaborators.                 |

---

## Entities and Strongly Typed IDs

- Use **strongly typed IDs** implemented as `record`s with GUID values (using `Guid.CreateVersion7()`).
- IDs must have:
  - Static factory methods: `NewId()`, `NewEfId(Guid)`, and `Create(Guid)` with validation.
  - EF Core compatibility (parameterless private constructor).
- Use these ID types for respective entities: `UserId`, `CollaboratorId`, `ProjectId`, `AssignmentId`.

---

## Entity Details

### User

- Properties:
```csharp
UserId Id;
string Username;
string Email;
byte[] PasswordHash;
byte[] PasswordSalt;
UserRole Role;
CollaboratorId? CollaboratorId; // optional
```

---

### Collaborator

- Properties:
```csharp
CollaboratorId Id;
string FullName;
string Email;
```

- Relationships:
  - Has many `Assignments`
  - Many-to-many with `Projects` via `ProjectCollaborator`

---

### Project

- Properties:
```csharp
ProjectId Id;
string Name;
DateTime StartDate;
DateTime? EndDate;
ProjectStatus Status;
```

- Relationships:
  - Has many `Assignments`
  - Many-to-many with `Collaborators` via `ProjectCollaborator`

---

### Assignment

- Properties:
```csharp
AssignmentId Id;
string Title;
string? Description;
decimal EstimatedHours;
decimal LoggedHours;
AssignmentStatus Status;
```

- Relationships:
  - Belongs to one `Project`
  - Optionally assigned to one `Collaborator`

---

### Enums

```csharp
public enum UserRole { Admin, Manager, Collaborator }

public enum ProjectStatus { Planned, InProgress, Completed, Cancelled }

public enum AssignmentStatus { Todo, InProgress, Done }
```


## Requirements

### Authentication & Authorization
- Implement JWT-based login and registration.
- Store passwords securely (hash + salt).
- Role-based authorization:
  - Only **Admin** can create users.
  - **Manager** can create projects and assign collaborators.
  - **Collaborator** can update their own assignments.

### CRUD Operations
- Users (Admin only)
- Collaborators (Manager+)
- Projects (Manager+)
- Assignments (Manager and assigned Collaborator can update)

### Querying with LINQ
- List Projects with filters: status, date range
- List Assignments with filters: status, assigned collaborator, project
- Support pagination and sorting on lists

### Strongly Typed IDs
- Use your `StronglyTypedGuidId<TSelf>` base class pattern.
- Use `Guid.CreateVersion7()` for generating new IDs.

### Validation
- Validate inputs on entity creation (e.g., non-empty names, valid dates).
- Domain rules such as: Cannot assign an assignment to a collaborator who is not part of the project.

### Unit Tests
- Cover creation, assignment, and business rules for `Assignment` and `Project`.

---

## Bonus (Optional)
- Soft deletion for entities
- Refresh tokens for JWT
- Integration tests with in-memory EF Core provider

---

## Deliverables
- Fully working .NET 9 Web API project
- EF Core code-first migrations
- Unit tests project
- Postman collection or Swagger docs
- Readme with assumptions & instructions
