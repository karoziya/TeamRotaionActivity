using TeamRotationActivity.Domain.Models.Abstractions;

namespace TeamRotationActivity.Domain.Models;

/// <summary>
/// Член команды.
/// </summary>
public class Member : Entity
{
    /// <summary>
    /// Имя.
    /// </summary>
    public string Name { get; set; } = String.Empty;

    /// <summary>
    /// Фамилия.
    /// </summary>
    public string LastName { get; set; } = String.Empty;
}

