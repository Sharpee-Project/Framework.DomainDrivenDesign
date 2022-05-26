﻿using System;

namespace Sharpee.Framework.DomainModel.Events;

/// <summary>
/// Interface which every event has to implement it to get recognized as an event.
/// </summary>
public interface IEvent
{
    public Guid EventId { get; set; }
}