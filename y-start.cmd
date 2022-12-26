pushd %~dp0

start cmd /k "dotnet run --project .\SkillSystem.IdentityServer4\SkillSystem.IdentityServer4.csproj --no-build"
start cmd /k "dotnet run --project .\SkillSystem.WebApi\SkillSystem.WebApi.csproj --no-build"

popd