
using AutoMapper;

namespace Tools.Service.Mappings.Configuration
{
    public class ProfilesConfiguration
    {
        /// <summary>
        /// Create an AutoMapperConfig class file to register a mapping relation.
        /// </summary>
        public static IMapper MapProfiles()
        {
            // Auto Mapper Configurations            
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AllowNullCollections = true;

                // Agregar los nuevos profiles aqui.
                mc.AddProfile(new ClienteDTOMapping());
                mc.AddProfile(new UserDTOMapping());
                mc.AddProfile(new ProveedorDTOMapping());
                mc.AddProfile(new ProductoDTOMapping());

            });

            return mappingConfig.CreateMapper();
        }
    }
}
