using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace BAYSOFT.Abstractions.Crosscutting.InheritStringLocalization
{
    public class InheritStringLocalizerFactory : IStringLocalizerFactory
    {
        private readonly ConcurrentDictionary<Type, IStringLocalizer> _cache = new ConcurrentDictionary<Type, IStringLocalizer>();
        private readonly ResourceManagerStringLocalizerFactory _factory;
        private readonly ILoggerFactory _loggerFactory;
        public InheritStringLocalizerFactory(IOptions<LocalizationOptions> options, ILoggerFactory loggerFactory)
        {
            _factory = new ResourceManagerStringLocalizerFactory(options, loggerFactory);
            _loggerFactory = loggerFactory;
        }
        public IStringLocalizer Create(Type resourceSource)
        {
            if (resourceSource == null) 
                throw new ArgumentNullException(nameof(resourceSource));

            return CreateStringLocalizer(resourceSource);
        }

        private IStringLocalizer CreateStringLocalizer(Type resourceSource)
        {
            return _cache.GetOrAdd(resourceSource, CreateStringLocalizerDirect);
        }

        private IStringLocalizer CreateStringLocalizerDirect(Type type)
        {
            var attribues = type.GetCustomAttributes<InheritStringLocalizerAttribute>();
            if (attribues.Any())
            {
                var localizers = new List<IStringLocalizer>();
                var localizer = _factory.Create(type);
                localizers.Add(localizer);
                foreach(var attribue in attribues.OrderBy(a => a.Priority))
                {
                    localizer = CreateStringLocalizer(attribue.InheritFrom);
                    localizers.Add(localizer);
                }

                return new MultiStringLocalizer(localizers, _loggerFactory.CreateLogger<MultiStringLocalizer>());
            }
            return _factory.Create(type);
        }

        public IStringLocalizer Create(string baseName, string location) => _factory.Create(baseName, location);
    }
}
