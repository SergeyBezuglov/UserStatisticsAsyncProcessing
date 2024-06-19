using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace PIMS.Web.Helpers
{
    /// <summary>
    /// Параметры настройки swagger.
    /// </summary>
    public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
    {
        /// <summary>
        /// Поставщик.
        /// </summary>
        private readonly IApiVersionDescriptionProvider _provider;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="ConfigureSwaggerOptions"/> .
        /// </summary>
        /// <param name="provider">Поставщик.</param>
        public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider) => _provider = provider;

        /// <summary>
        /// Задача (TODO): Добавить сводку
        /// </summary>
        /// <param name="options">Варианты.</param>
        public void Configure(SwaggerGenOptions options)
        {
            foreach (var description in _provider.ApiVersionDescriptions)
                options.SwaggerDoc(description.GroupName, CreateInfoForApiVersion(description));

        }

        /// <summary>
        /// Создает информацию для версии API.
        /// </summary>
        /// <param name="description">Описание.</param>
        /// <returns>Возвращение значения открыть информацию об API (OpenApiInfo).</returns>
        private static OpenApiInfo CreateInfoForApiVersion(ApiVersionDescription description)
        {
            var info = new OpenApiInfo
            {
                Title = "Personal Information Management System",
                Version = description.ApiVersion.ToString(),
                Description = "Web Service for Personal Information Management System",
                Contact = new OpenApiContact
                {
                    Name = "IT Department",
                    Email = "developerm@mail.ru",
                    Url = new Uri("https://pimstmc.ru/support")
                }
            };

            if (description.IsDeprecated)
                info.Description += " <strong>This API version of  Personal Information Management System has been deprecated.</strong>";

            return info;
        }

    }
}
