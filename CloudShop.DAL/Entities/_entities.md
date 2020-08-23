# Entities

## Goals for Entity-Design

- Entities should contain the minimum amount of fields required to represent what needs to be done
- Prevent duplicated Entities that just differ from a few fields - reuse them as much as possible
- Entities should be working on different Database-Systems (Sqlite, Postgresql, etc. ...)
- Entities are just POCO-Objects (no logic, functions etc. ...) should be inside an Entity

## Creating new Entities

- 1) Verify the Entity you want to create doesn't exist already
- 2) Create a new Entity ending with "Entity" Postfix inside the "Entities"-directory according to the below rules
- 3) Create an AutoMapper-mapping from the newly created Entity to an according Dto-Object if its used in the public API
- 4) Create a Migration-SQL Script that creates the Table in the Database 


## Rules for Entities

- Entities MUST be `internal` (usage outside of DAL must not be possible)
- Entities MUST be `sealed` (inheriting from an Entity is forbidden) - Exception: IndentifiedEntity as a base-class
- Entities MUST consist out of native types only (string, int, long, double, decimal, float) - Exception: DateTime
- Entities MUST NOT have an Id on their own, Entities requiring an Id must inherit from the `IdentifiedEntity`-class
- Entities MUST NOT contain class-references to other Entities - use the corresponding Ids for creating Mappings
- Entities MUST NOT contain anything else than Properties (Entites are POCOs)
