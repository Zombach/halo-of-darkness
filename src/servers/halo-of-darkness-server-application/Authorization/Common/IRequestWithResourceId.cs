using MediatR;

namespace HaloOfDarkness.Server.Application.Authorization.Common;

public interface IRequestWithResourceId
    : IRequest
{
    string ResourceId { get; }
}
