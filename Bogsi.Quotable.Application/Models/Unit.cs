﻿namespace Bogsi.Quotable.Application.Models;

// Flyweight, serves as a void response
public readonly struct Unit
{
    public static Unit Instance => default;
}