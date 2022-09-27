# Security

## Status

Accepted

## Context

Authentication and Authorization features have to be implemented, but the question is where and how?

## Decision

There will be an `ISecurityService` interface in the `DotNetCMS.Application` project with an implementation using the
ASP.NET in the infrastructure layer project `DotNetCMS.Security.AspNetCore`. The structure is therefore similar to what
has been decided in [0002-persistence](0002-persistence.md). So there can also be a `DotNetCMS.Security.Test` project,
which contains the tests for `DotNetCMS.Security.AspNetCore` as well as other implementations like
`DotNetCMS.Security.Memory`, which can be used for tests.

Therefore it is important to also have methods that allow to add users and their role assignments, because only this
way a proper in-memory implementation can be built.

## Consequences

- Since security is not included in `DotNetCMS.Application` something other than ASP.NET Core could be used.
- Tests for all possible security implementations can be included in the `DotNetCMS.Security.Test` by inheriting from
the base test class and only implement the construction of the `ISecurityService`.
- The `Authorizer` is a very thin wrapper around the `IAuthorizationService`, but a similiar concept already works well
for persistence.
- There will be a bit more boilerplate code, because most operations have to be implemented in the `ISecurityService`
before they can be used.
- Tests can be implemented a lot faster, because they can make use of a in-memory implementation.
