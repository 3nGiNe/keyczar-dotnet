/*  Copyright 2012 James Tuley (jay+code@tuley.name)
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
extern alias SC;

using System;
using System.Globalization;
using System.IO;
using Keyczar;
using Keyczar.Util;
using Newtonsoft.Json;
using SC::SharpCompress.Writers.Zip;
using SC::SharpCompress.Writers;
using SC::SharpCompress.Common;

namespace Keyczar.Unofficial
{
    /// <summary>
    /// Writes keyset to a single zipped up blob
    /// </summary>
    public class BlobKeySetWriter : IKeySetWriter, IDisposable, INonSeparatedMetadataAndKey
    {
        private Stream _writeStream;
        private ZipWriter _zipFile;

        /// <summary>
        /// Initializes a new instance of the <see cref="BlobKeySetWriter"/> class.
        /// </summary>
        /// <param name="writeStream">The write stream.</param>
        public BlobKeySetWriter(Stream writeStream)
        {
            _writeStream = writeStream;
            _zipFile = new NondestructiveZipFile(_writeStream, new ZipWriterOptions(SC::SharpCompress.Common.CompressionType.Deflate));
        }

        /// <summary>
        /// Writes the specified key data.
        /// </summary>
        /// <param name="keyData">The key data.</param>
        /// <param name="version">The version.</param>
        public void Write(byte[] keyData, int version)
        {
            _zipFile.Write(version.ToString(CultureInfo.InvariantCulture), new MemoryStream(keyData));
        }


        /// <summary>
        /// Writes the specified metadata.
        /// </summary>
        /// <param name="metadata">The metadata.</param>
        public void Write(KeyMetadata metadata)
        {
            var bytes = Keyczar.RawStringEncoding.GetBytes(metadata.ToJson());
            _zipFile.Write("meta",new MemoryStream(bytes));
        }

        /// <summary>
        /// Finishes this writing of the key.
        /// </summary>
        /// <returns></returns>
        public bool Finish()
        {
            return true;
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(false);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="BlobKeySetWriter" /> class.
        /// </summary>
        ~BlobKeySetWriter()
        {
            Dispose(false);
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2213:DisposableFieldsShouldBeDisposed",
            MessageId = "_zipFile")]
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _writeStream.SafeDispose();
            }
            _writeStream = null;

            _zipFile = _zipFile.SafeDispose();
        }
    }
}