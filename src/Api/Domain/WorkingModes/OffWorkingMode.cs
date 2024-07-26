﻿using Api.Services;

namespace Api.Domain.WorkingModes;

public class OffWorkingMode(
    IRelayControlService relayControlService,
    ILogger<OffWorkingMode> logger) : IWorkingMode
{
    public Task<bool> Setup(CancellationToken cancellationToken = default)
    {
        relayControlService.TurnOutputRelayOff();
        return Task.FromResult(true);
    }

    public Task Do(CancellationToken cancellationToken = default) => Task.CompletedTask;

    public const string IdentifierKey = nameof(OffWorkingMode);
    public string Identifier => IdentifierKey;
    public string DisplayName => "Off";
}