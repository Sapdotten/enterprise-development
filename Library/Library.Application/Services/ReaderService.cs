using AutoMapper;
using Library.Application.Contracts.Dtos;
using Library.Application.Contracts.Interfaces;
using Library.Domain.Interfaces;
using Library.Domain.Entities;

namespace Library.Application.Services;

/// <summary>
/// Application service for managing reader operations.
/// </summary>
public class ReaderService(
    IRepository<Reader, int> readerRepository, IMapper mapper) : IApplicationService<ReaderGetDto, ReaderCreateDto, int>
{
    /// <summary>
    /// Creates a new reader from the provided DTO.
    /// </summary>
    /// <param name="dto">The DTO containing reader data. Must not be null.</param>
    /// <returns>The created reader as a ReaderGetDTO.</returns>
    public ReaderGetDto Create(ReaderCreateDto dto)
    {
        var newReader = mapper.Map<Reader>(dto);
        readerRepository.Create(newReader);
        return mapper.Map<ReaderGetDto>(newReader);
    }

    /// <summary>
    /// Retrieves a reader by its unique identifier.
    /// </summary>
    /// <param name="dtoId">The ID of the reader to retrieve.</param>
    /// <returns>The reader as a ReaderGetDTO if found.</returns>
    /// <exception cref="InvalidOperationException">Thrown when no reader exists with the given ID.</exception>
    public ReaderGetDto Get(int dtoId)
    {
        var reader = readerRepository.Read(dtoId) ?? throw new InvalidOperationException($"Reader with ID {dtoId} was not found");
        return mapper.Map<ReaderGetDto>(reader);
    }

    /// <summary>
    /// Retrieves a list of all readers.
    /// </summary>
    /// <returns>A list of all readers represented as ReaderGetDTOs. Returns empty list if none exist.</returns>
    public List<ReaderGetDto> GetAll()
    {
        var readers = readerRepository.ReadAll();
        return mapper.Map<List<ReaderGetDto>>(readers);
    }

    /// <summary>
    /// Updates an existing reader with data from the provided DTO.
    /// </summary>
    /// <param name="reader">The DTO containing updated reader data. Must not be null.</param>
    /// <param name="dtoId">The ID of the reader to update.</param>
    /// <returns>The updated reader as a ReaderGetDTO.</returns>
    /// <exception cref="InvalidOperationException">Thrown when no reader exists with the given ID.</exception>
    public ReaderGetDto Update(ReaderCreateDto reader, int dtoId)
    {
        var toUpdateReader = readerRepository.Read(dtoId) ?? throw new InvalidOperationException($"Reader with ID {dtoId} was not found for updating");
        mapper.Map(reader, toUpdateReader);
        readerRepository.Update(toUpdateReader);
        return mapper.Map<ReaderGetDto>(toUpdateReader);
    }

    /// <summary>
    /// Deletes a reader identified by its unique ID.
    /// </summary>
    /// <param name="dtoId">The ID of the reader to delete.</param>
    /// <returns>True, if success, False when no book exists with the given ID.</returns>
    public bool Delete(int dtoId)
    {
        return readerRepository.Delete(dtoId);
    }
}