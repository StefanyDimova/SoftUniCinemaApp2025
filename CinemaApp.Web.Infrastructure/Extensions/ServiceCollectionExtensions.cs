﻿using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using static CinemaApp.GCommon.ExceptionMessages;

namespace CinemaApp.Web.Infrastructure.Extensions
{

    // метод , който намира сървиси и интерфейси и ги регистрира 
    public static class ServiceCollectionExtensions
    {
        private static readonly string InterfacePrefix = "I";
        private static readonly string ServiceTypeSuffix = "Service";
        private static readonly string RepositoryTypeSuffix = "Repository";
        public static IServiceCollection AddUserDefinedServices(this IServiceCollection serviceCollection, Assembly serviceAssembly)
        {
            Type[] serviceClasses = serviceAssembly.GetTypes()
                .Where(t => !t.IsInterface &&
                    t.Name.EndsWith(ServiceTypeSuffix))
                .ToArray();

            foreach (Type serviceClass in serviceClasses)
            {
                Type? serviceInterface = serviceClass.GetInterfaces()
                    .FirstOrDefault(i => i.Name == $"{InterfacePrefix}{serviceClass.Name}");

                if(serviceInterface == null)
                {
                    throw new ArgumentException(InterfaceNotFoundMessage, serviceClass.Name);
                }

                    serviceCollection.AddScoped(serviceInterface, serviceClass);

            }

            return serviceCollection;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection serviceCollection, Assembly repositoryAssembly)
        {
            // тук взимаме всички , освен baseRepo (само той е абстрактен)
            Type[] repositoryClasses = repositoryAssembly.GetTypes()
                .Where(t => t.Name.EndsWith(RepositoryTypeSuffix) &&
                            !t.IsInterface && !t.IsAbstract)
                .ToArray();

            foreach(Type repositoryClass in repositoryClasses)
            {
                Type? repositoryInterface = repositoryClass.GetInterfaces()
                    .FirstOrDefault(i => i.Name == $"{InterfacePrefix}{repositoryClass.Name}");
                if(repositoryInterface == null)
                {
                    throw new ArgumentException(String.Format(InterfaceNotFoundMessage, repositoryClass.Name));
                }
                
                serviceCollection.AddScoped(repositoryInterface, repositoryClass);
            }

            return serviceCollection;
        }
    }
}
