using AutoMapper;
using Library.Application.Contracts.Dtos;
using Library.Application.Contracts.Dtos.AnalyticsDtos;
using Library.Domain.Entities;
using Library.Domain.Enums;

namespace Library.Application;

/// <summary>
/// AutoMapper profile defining object-object mappings between domain entities and DTOs.
/// Configures bidirectional conversion rules for Book, Reader, LoanRecord, 
/// and analytics-related DTOs used in the application layer.
/// </summary>
public class MappingProfile : Profile
{
    /// <summary>
    /// Initializes a new instance of the MappingProfile class.
    /// Sets up all required type conversions for CRUD and analytics operations.
    /// </summary>
    public MappingProfile()
    {
        CreateMap<BookCreateDto, Book>()
            .BeforeMap((dto, _, _) =>
            {
                if (!Enum.IsDefined(typeof(PublisherType), dto.PublisherType))
                {
                    var values = string.Join(", ", Enum.GetValues<PublisherType>());
                    throw new ArgumentException(
                        $"Invalid PublisherType '{dto.PublisherType}'. " +
                        $"Allowed values: {values}.");
                }

                if (!Enum.IsDefined(typeof(Publisher), dto.Publisher))
                {
                    var values = string.Join(", ", Enum.GetValues<Publisher>());
                    throw new ArgumentException(
                        $"Invalid Publisher '{dto.Publisher}'. " +
                        $"Allowed values: {values}.");
                }
            })
            .ReverseMap();

        CreateMap<BookGetDto, Book>().ReverseMap();

        CreateMap<ReaderCreateDto, Reader>().ReverseMap();
        CreateMap<ReaderGetDto, Reader>().ReverseMap();

        CreateMap<LoanRecordCreateDto, LoanRecord>().ReverseMap();
        CreateMap<LoanRecordGetDto, LoanRecord>().ReverseMap();

        CreateMap<Book, BookLoanCountDto>().ReverseMap();
        CreateMap<Reader, ReaderLoanCountDto>().ReverseMap();
        CreateMap<Reader, ReaderLoanDurationDto>().ReverseMap();
    }
}