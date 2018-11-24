﻿#region --- License & Copyright Notice ---
/*
IniFile Library for .NET
Copyright (c) 2018 Jeevan James
All rights reserved.

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

    http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
*/
#endregion

using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using IniFile.Items;

namespace IniFile
{
    public sealed partial class Ini
    {
        [DebuggerDisplay("[{Name}]")]
        public sealed class Section : IReadOnlyList<Property>, ITopLevelIniItem
        {
            private readonly List<ISectionItem> _items = new List<ISectionItem>();

            internal Section()
            {
            }

            public Section(string name)
            {
                Name = name;
            }

            public string Name { get; }

            public T Add<T>(T item)
                where T : ISectionItem
            {
                if (item == null)
                    throw new System.ArgumentNullException(nameof(item));
                _items.Add(item);
                return item;
            }

            public Property AddProperty<T>(string key, T value) =>
                Add(new Property(key, value?.ToString() ?? string.Empty));

            IIniItem IIniItem.TryCreate(string line)
            {
                Match match = SectionPattern.Match(line);
                if (!match.Success)
                    return null;
                return new Section(match.Groups[1].Value);
            }

            private static readonly Regex SectionPattern = new Regex(@"^\[\s*(\w[\w\s]*)\s*\]$");

            async Task IIniItem.Write(TextWriter writer)
            {
                await writer.WriteLineAsync($"[{Name}]");
            }

            public IEnumerator<Property> GetEnumerator() =>
                _items.OfType<Property>().GetEnumerator();

            IEnumerator IEnumerable.GetEnumerator() =>
                GetEnumerator();

            public int Count =>
                _items.OfType<Property>().Count();

            public Property this[int index] =>
                _items.OfType<Property>().ElementAt(index);

            public string this[string key]
            {
                get => this.OfType<Property>().FirstOrDefault(p => p.Key == key)?.Value;
                set
                {
                    Property matchingProperty = this.OfType<Property>().FirstOrDefault(p => p.Key == key);
                    if (matchingProperty == null)
                        _items.Add(new Property(key, value));
                    else
                        matchingProperty.Value = value;
                }
            }
        }
    }
}