using AutoMapper;
using CleanArchitecture.Application.Templates.DTOs;
using CleanArchitecture.Domain.Entities.TemplateAggregate;

namespace CleanArchitecture.Application.Mappings;

public class TemplateMappingProfile : Profile
{
    public TemplateMappingProfile()
    {
        CreateMap<Template, TemplateDto>();
        CreateMap<TemplateDto, Template>();
    }
}
