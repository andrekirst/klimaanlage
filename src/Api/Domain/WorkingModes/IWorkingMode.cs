namespace Api.Domain.WorkingModes;

public interface IWorkingMode
{
    Task<bool> Setup(CancellationToken cancellationToken = default);
    Task Do(CancellationToken cancellationToken = default);
    string Identifier { get; }
    string DisplayName { get; }
}