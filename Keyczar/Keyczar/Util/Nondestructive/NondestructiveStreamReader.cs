﻿/*  Copyright 2012 James Tuley (jay+code@tuley.name)
 * 
 *  Licensed under the Apache License, Version 2.0 (the "License");
 *  you may not use this file except in compliance with the License.
 *  You may obtain a copy of the License at
 * 
 *      http://www.apache.org/licenses/LICENSE-2.0
 * 
 *  Unless required by applicable law or agreed to in writing, software
 *  distributed under the License is distributed on an "AS IS" BASIS,
 *  WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 *  See the License for the specific language governing permissions and
 *  limitations under the License.
 */

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Keyczar.Util
{
    /// <summary>
    /// Streamreader that won't close the stream when finished
    /// </summary>
    public class NondestructiveStreamReader : StreamReader
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NondestructiveStreamReader"/> class.
        /// </summary>
        /// <param name="stream">The stream.</param>
        public NondestructiveStreamReader(Stream stream) : base(stream)
        {
        }

        /// <summary>
        /// Closes the underlying stream, releases the unmanaged resources used by the <see cref="T:System.IO.StreamReader"/>, and optionally releases the managed resources.
        /// </summary>
        /// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            base.Dispose(false);
        }
    }
}