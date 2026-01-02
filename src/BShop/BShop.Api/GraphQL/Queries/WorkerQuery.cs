using AutoMapper;
using AutoMapper.QueryableExtensions;
using BShop.Domain.Interfaces.Service;
using BShop.Models;
using HotChocolate.Data;

namespace BShop.GraphQL.Queries;

[ExtendObjectType(Name = "Query")]
public class WorkerQuery(IWorkerService workerService, IMapper mapper)
{
    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<WorkerApi> GetAllWorkers(CancellationToken cancellationToken)
    {
        return workerService.GetAllWorkers(cancellationToken)
            .ProjectTo<WorkerApi>(mapper.ConfigurationProvider);
    }

    [UseFirstOrDefault]
    [UseProjection]
    public IQueryable<WorkerApi> GetWorkerById(Guid workerId, CancellationToken cancellationToken)
    {
        return workerService.GetAllWorkers(cancellationToken).Where(x => x.Id == workerId)
            .ProjectTo<WorkerApi>(mapper.ConfigurationProvider);
    }
}