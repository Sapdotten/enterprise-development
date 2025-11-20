using AutoMapper;
using Library.Application.Contracts.Dtos;
using Library.Application.Contracts.Dtos.AnalyticsDtos;
using Library.Domain.Entities;
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
        CreateMap<BookCreateDto, Book>().ReverseMap();
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
