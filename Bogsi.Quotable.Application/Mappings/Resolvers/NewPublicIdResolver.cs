using AutoMapper;

namespace Bogsi.Quotable.Application.Mappings.Resolvers;

internal sealed class NewPublicIdResolver : IValueResolver<object, object, Guid>
{
    public Guid Resolve(object source, object destination, Guid destMember, ResolutionContext context)
    {
        return Guid.NewGuid();
    }
}
