using AutoMapper;
using AutoMapper.QueryableExtensions;
using BShop.Domain.Interfaces.Service;
using BShop.Models;
using HotChocolate.Data;

namespace BShop.GraphQL.Queries;

[ExtendObjectType(Name = "Query")]
public class WorkerQuery
{
    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<WorkerApi> GetAllWorkers(
        [Service] IWorkerService workerService,
        [Service] IMapper mapper,
        CancellationToken cancellationToken)
    {
        return workerService.GetAllWorkers(cancellationToken)
            .ProjectTo<WorkerApi>(mapper.ConfigurationProvider);
    }

    [UseFirstOrDefault]
    [UseProjection]
    public IQueryable<WorkerApi> GetWorkerById(
        [Service] IWorkerService workerService,
        [Service] IMapper mapper,
        Guid workerId,
        CancellationToken cancellationToken)
    {
        return workerService.GetAllWorkers(cancellationToken).Where(x => x.Id == workerId)
            .ProjectTo<WorkerApi>(mapper.ConfigurationProvider);
    }
}