v1
Core
    - PosgreSQL (Supabase)
    - Entity Framework
    - Fluent Validations
    - CQRS
    - Result Pattern

Entities
    - Task
        - Id Guid
        - Title Varchar(255)
        - Description Varchar(Max)
        - IsActive Boolean
        - BoardId Guid
        - Category Guid
        - CreatedAt DateTime
        - UpdatedAt DateTime
    - Category
        - Id Guid
        - Name Varchar(128), Unique
        - CreatedAt DateTime
        - UpdatedAt DateTime
    - Board
        - Id Guid
        - Name Varchar(128), Unique

Features
    - CRUD Operations
    - Domain Events
    - Value Object
    - Filtering + Sorting

v2
Features
    - Add images to Task (Supabase Storage)
    - Add Identity (Supabase Auth)

v3
Features
    - Groups of users

v4
Features
    - Recover deleted tasks
    - Job automatically deletes tasks in trash bin

v5
Features
    - Notifications (WebSockets)