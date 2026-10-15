using AutoMapper;
using E_Commerce.Application.DTOs;
using E_Commerce.Domain.Entities;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Profiles
{
    internal class PictureUrlResolver : IValueResolver<Product, ProductDTO, string>
    {
        private readonly IOptions<UrlSettings> _imageResolver;

        public PictureUrlResolver(IOptions<UrlSettings> imageResolver)
        {
            _imageResolver = imageResolver;
        }
        public string Resolve(Product source, ProductDTO destination, string destMember, ResolutionContext context)
        {
        var baseUrl = _imageResolver.Value.BaseUrl.TrimEnd('/');
            var path = source.PictureUrl.TrimStart('/');
            return $"{baseUrl}/Files/{path}";
        }
    }
    public class UrlSettings
    {
        public string BaseUrl { get; set; }
    }
}
