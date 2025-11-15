using AutoMapper;
using Library.Application.Contracts.DTOs;
using Library.Application.Contracts.DTOs.AnalyticsDTOs;
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
        CreateMap<BookCreateDTO, Book>().ReverseMap();
        CreateMap<BookGetDTO, Book>().ReverseMap();

        CreateMap<ReaderCreateDTO, Reader>().ReverseMap();
        CreateMap<ReaderGetDTO, Reader>().ReverseMap();

        CreateMap<LoanRecordCreateDTO, LoanRecord>().ReverseMap();
        CreateMap<LoanRecordGetDTO, LoanRecord>().ReverseMap();

        CreateMap<Book, BookLoanCountDTO>().ReverseMap();
        CreateMap<Reader, ReaderLoanCountDTO>().ReverseMap();
        CreateMap<Reader, ReaderLoanDurationDTO>().ReverseMap();
    }
}
