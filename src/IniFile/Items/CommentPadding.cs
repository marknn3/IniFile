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
    ///     Represents the padding details for an INI comment.
    /// </summary>
    public sealed class CommentPadding : Padding
    {
        /// <summary>
        ///     The amount of space to the right of the comment text.
        /// </summary>
        public PaddingValue Right { get; set; }

        /// <summary>
        ///     Amount of space between the comment ; and the start of the comment text.
        /// </summary>
        public PaddingValue Inside { get; set; } = 1;

        /// <inheritdoc/>
        public override void Reset()
        {
            base.Reset();
            Left = 0;
            Inside = 1;
        }
    }
}