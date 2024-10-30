using Common.Mappers;

namespace HandlerTemplate.Services.AddCommand;

public class AddCommandMapper : IMapper<AddCommandUnverifiedData, AddCommandVerifiedData>
{
    public AddCommandVerifiedData Map(AddCommandUnverifiedData input)
    {
        throw new NotImplementedException();
    }
}