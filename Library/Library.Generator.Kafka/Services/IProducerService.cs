using Library.Application.Contracts.Dtos;

namespace Library.Generator.Kafka.Services;

public interface IProducerService
{
    public Task SendAsync(IList<LoanRecordCreateDto> batch);
}