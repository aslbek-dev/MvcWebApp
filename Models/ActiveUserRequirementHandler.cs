using Microsoft.AspNetCore.Authorization;
public class ActiveUserRequirementHandler : AuthorizationHandler<ActiveUserRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ActiveUserRequirement requirement)
    {
        if (context.User.Identity?.IsAuthenticated == true)
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}
