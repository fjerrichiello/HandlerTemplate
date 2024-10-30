using Common.Authorization;

namespace HandlerTemplate.Services.AddCommand;

public class AddCommandAuthorizer : Authorizer<AddCommandUnverifiedData>
{
    public AddCommandAuthorizer()
    {
        
    }
}