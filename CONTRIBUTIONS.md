# Contributions

Who did what. Each member fills in their own row with their real GitHub account, the features they built, and example commits or pull requests. Keep it accurate: it is graded and it has to match the commit history.

| Member | GitHub | Area / features | Example commits / PRs |
|---|---|---|---|
| Andrick Steven De Vera | @andrickdeveracollege-netizen | Domain models (`Model/Domain`), database context and seeding (`Model/DATA`), mappers, `Program.cs` and app configuration, controllers (Events, Guest), the whole front end (`wwwroot` HTML, CSS, JavaScript: landing page, bulletin board, calendar, dashboard, registration and request forms), bug fixing and UI design fixes | `df4b1a0` first commit; `4d4deba` done all domain; `25175ec` add DBs; `d52f033` done Mapper; `cfbea76` created the front end; `eef6878` fix the style/design; PR #1 and PR #2 (merged) |
| Jacob Marquiz Cagang | @jacobcagang | DTOs (`Model/DTO`), service and repository interfaces (`IXxxService`, `IXxxRepository`), initial repository and controller files, database connection string and departments setup (`appsettings.json`), project README | `9599645` creeated repository; `eb85e70` created DTOs; `d3a98a5` add controller file; `4651215` created service; `bd29b4a` added connection string and departments; `3afa08c` CREATED README; CREATED CONTRIBUTION.md |
| John Benedict Estimada | @johnestimada | Service layer implementations (Auth, Event, Registration, Equipment, UserAdmin), registrants Excel export (`RegistrantExporter`, `XlsxWriter`), `PasswordHasher`, `EmailRoleParser`, service exceptions, repository implementations (User, Registration, Facility, Event, Equipment, DepartmentRequest) | `56d881a` AuthService; `272fbf6` Registration Service added; `6ee978f` Registrants Exporter Added; `65f78af` Passwordhasher created; `df5f28d` Repository classes added; PR #1 from branch `JB` (merged) |

## Working agreement
- One feature per branch (`feature/<name>`), merged through a pull request that another member reviews.
- Every member commits under their own GitHub account (check `git config user.email`).
- Commit messages say what changed, for example "add capacity check on registration".
- Parts of this project were written with an AI assistant (Claude). That help is part of the history and is stated here on purpose.
