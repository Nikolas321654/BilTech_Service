using BShop.Domain.Interfaces.Repository;
using BShop.Domain.Interfaces.Service;

namespace BShop.Application.Services;

public class StorageService(IStorageRepository _storageRepository) : IStorageService
{
    
}