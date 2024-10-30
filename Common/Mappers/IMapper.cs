namespace Common.Mappers;

public interface IMapper<in TInput, out TOutput>
{
    TOutput Map(TInput input);
}