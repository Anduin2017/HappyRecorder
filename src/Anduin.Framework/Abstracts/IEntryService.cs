﻿namespace Anduin.Framework.Abstracts;

public interface IEntryService
{
    public Task OnServiceStartedAsync(string path, bool shouldTakeAction);
}