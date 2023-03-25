using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace BAYSOFT.Abstractions.Crosscutting.InheritStringLocalization
{
    public class MultiStringLocalizer : IStringLocalizer
    {
        private List<IStringLocalizer> Localizers { get; set; }
        private ILogger<MultiStringLocalizer> Logger { get; set; }
        public MultiStringLocalizer(List<IStringLocalizer> localizers, ILogger<MultiStringLocalizer> logger)
        {
            if (localizers == null)
            {
                throw new ArgumentNullException(nameof(localizers));
            }
            if (localizers.Count == 0)
            {
                throw new ArgumentException("Empty not supported", nameof(localizers));
            }

            Localizers = localizers;
            Logger = logger ?? NullLogger<MultiStringLocalizer>.Instance;
        }

        public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures)
        {
            var result = new Dictionary<string, LocalizedString>();
            foreach (var localizer in Localizers)
            {
                foreach (var entry in localizer.GetAllStrings(includeParentCultures))
                {
                    if (!result.ContainsKey(entry.Name))
                    {
                        result.Add(entry.Name, entry);
                    }
                }
            }
            return result.Values;
        }

        private void OnLogAttempt(IStringLocalizer localizer, LocalizedString result)
        {
            if (!result.ResourceNotFound)
            {
                Logger.LogDebug($"{localizer.GetType()} found '{result.Name}' in '{result.SearchedLocation}'");
            }
            else
            {
                Logger.LogDebug($"{localizer.GetType()} searched for '{result.Name}' in '{result.SearchedLocation}'");
            }
        }

        public LocalizedString this[string name]
        {
            get
            {
                LocalizedString s = null;
                foreach (var localizer in Localizers)
                {
                    s = localizer[name];
                    OnLogAttempt(localizer, s);
                    if (!s.ResourceNotFound)
                        break;
                }
                Debug.Assert(s != null);
                return s;
            }
        }

        public LocalizedString this[string name, params object[] arguments]
        {
            get
            {
                LocalizedString s = null;
                foreach (var localizer in Localizers)
                {
                    s = localizer[name, arguments];
                    OnLogAttempt(localizer, s);
                    if (!s.ResourceNotFound)
                        break;
                }
                Debug.Assert(s != null);
                return s;
            }
        }
    }
}
