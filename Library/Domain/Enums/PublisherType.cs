namespace Domain.Enums;

/// <summary>
/// Defines the category of a publisher based on its primary focus: educational, commercial, or academic.
/// Used to classify books and manage library collections by content type.
/// </summary>
public enum PublisherType
{
    /// <summary>
    /// A publisher specializing in textbooks and educational materials for schools and universities.
    /// </summary>
    Educational,

    /// <summary>
    /// Large commercial publishers releasing fiction and popular literature.
    /// </summary>
    Commercial,

    /// <summary>
    /// Publishers focusing on scientific, scholarly, and academic works.
    /// </summary>
    Academic
}
