using ContactsApp.Application.Auth.Commands;
using ContactsApp.Domain.Entities;
using Newtonsoft.Json;
using System.Text;

namespace ContactsApp.Tests.Helpers
{
    public static class HttpContentHelper
    {
        public static HttpContent ToJsonHttpContent(this object obj)
        {
            var json = JsonConvert.SerializeObject(obj);

            var httpContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");

            return httpContent;
        }

        public static HttpContent ToFormDataHttpContent(this Contact contact)
        {
            var formData = new MultipartFormDataContent();

            if (!string.IsNullOrEmpty(contact.Id))
            {
                formData.Add(new StringContent(contact.Id), nameof(contact.Id));
            }

            if (!string.IsNullOrEmpty(contact.FirstName))
            {
                formData.Add(new StringContent(contact.FirstName), nameof(contact.FirstName));
            }

            if (!string.IsNullOrEmpty(contact.LastName))
            {
                formData.Add(new StringContent(contact.LastName), nameof(contact.LastName));
            }

            if (!string.IsNullOrEmpty(contact.Email))
            {
                formData.Add(new StringContent(contact.Email), nameof(contact.Email));
            }

            if (!string.IsNullOrEmpty(contact.NumberPhone))
            {
                formData.Add(new StringContent(contact.NumberPhone), nameof(contact.NumberPhone));
            }

            if (!string.IsNullOrEmpty(contact.UserId))
            {
                formData.Add(new StringContent(contact.UserId), nameof(contact.UserId));
            }

            if (contact.Image != null)
            {
                formData.Add(new ByteArrayContent(contact.Image), nameof(contact.Image), "image.jpg");
            }

            return formData;
        }

        public static HttpContent ToFormDataHttpContent(this RegisterDto registerDto)
        {
            var formData = new MultipartFormDataContent();

            if (!string.IsNullOrEmpty(registerDto.FirstName))
            {
                formData.Add(new StringContent(registerDto.FirstName), nameof(registerDto.FirstName));
            }

            if (!string.IsNullOrEmpty(registerDto.LastName))
            {
                formData.Add(new StringContent(registerDto.LastName), nameof(registerDto.LastName));
            }

            if (!string.IsNullOrEmpty(registerDto.Email))
            {
                formData.Add(new StringContent(registerDto.Email), nameof(registerDto.Email));
            }

            if (!string.IsNullOrEmpty(registerDto.Password))
            {
                formData.Add(new StringContent(registerDto.Password), nameof(registerDto.Password));
            }

            return formData;
        }

        public static HttpContent ToFormDataHttpContent(this LoginDto loginDto)
        {
            var formData = new MultipartFormDataContent();

            if (!string.IsNullOrEmpty(loginDto.Email))
            {
                formData.Add(new StringContent(loginDto.Email), nameof(loginDto.Email));
            }

            if (!string.IsNullOrEmpty(loginDto.Password))
            {
                formData.Add(new StringContent(loginDto.Password), nameof(loginDto.Password));
            }

            return formData;
        }
    }
}
