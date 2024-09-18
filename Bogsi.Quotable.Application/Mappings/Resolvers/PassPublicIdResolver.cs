using AutoMapper;

namespace Bogsi.Quotable.Application.Mappings.Resolvers;

public sealed class PassPublicIdResolver : IValueResolver<object, object, Guid>
{
    public Guid Resolve(object source, object destination, Guid destMember, ResolutionContext context)
    {
        var publicId = Guid.Parse(context.Items["Id"].ToString()!);

        return publicId;
    }
}
