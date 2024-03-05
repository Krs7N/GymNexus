using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using GymNexus.Core.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace GymNexus.Core.Services;

public class CloudinaryService : ICloudinaryService
{
    private readonly Cloudinary _cloudinary;

    public CloudinaryService(IConfiguration config)
    {
        var account = new Account(
            config["Cloudinary:CloudName"],
            config["Cloudinary:ApiKey"],
            config["Cloudinary:ApiSecret"]
        );

        _cloudinary = new Cloudinary(account);
        _cloudinary.Api.Secure = true;
    }

    public async Task<string> UploadImageAsync(IFormFile file)
    {
        await using var stream = file.OpenReadStream();

        var uploadParams = new ImageUploadParams
        {
            File = new FileDescription(file.FileName, stream),
        };

        var uploadResult = await _cloudinary.UploadAsync(uploadParams);

        return uploadResult.SecureUrl.ToString();
    }
}