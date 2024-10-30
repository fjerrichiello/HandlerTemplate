using Common.Mappers;

namespace HandlerTemplate.Services.AddCommand;

public class AddCommandMapper : IMapper<AddCommandUnverifiedData, AddCommandVerifiedData>
{
    public AddCommandVerifiedData Map(AddCommandUnverifiedData input)
    {
        ArgumentNullException.ThrowIfNull(input.Value1);

        return new AddCommandVerifiedData(input.Value1.Value);
    }
}