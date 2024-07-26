﻿using Api.Domain;

namespace Api.Sensors;

public interface IDs18B20
{
    Task<Temperature?> Read(int pinNumber, CancellationToken cancellationToken = default);
}