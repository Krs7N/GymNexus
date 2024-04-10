﻿namespace GymNexus.Core.Models;

public class ProfileUpdateDto
{
    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string ImageUrl { get; set; } = string.Empty;
}