﻿namespace GymNexus.Core.Models;

public class ProfileUpdateResponseDto
{
    public string Email { get; set; } = string.Empty;

    public string ImageUrl { get; set; } = string.Empty;

    public IEnumerable<string> Roles { get; set; } = new List<string>();
}