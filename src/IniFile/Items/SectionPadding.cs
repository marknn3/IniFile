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


namespace IniFile.Items
{
    /// <summary>
    ///     Represents the padding details for an INI section.
    /// </summary>
    public sealed class SectionPadding : Padding
    {
        /// <summary>
        ///     The amount of space between the left brace of the section and the section text.
        /// </summary>
        public PaddingValue InsideLeft { get; set; }

        /// <summary>
        ///     The amount of space between the right brace of the section and the section text.
        /// </summary>
        public PaddingValue InsideRight { get; set; }

        /// <inheritdoc/>
        public override void Reset()
        {
            base.Reset();
            InsideLeft = 0;
            InsideRight = 0;
        }
    }
}