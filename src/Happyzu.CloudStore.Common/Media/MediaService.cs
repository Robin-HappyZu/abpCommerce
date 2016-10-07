using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using Happyzu.CloudStore.Common.Validation;

namespace Happyzu.CloudStore.Common.Media
{
    public class MediaService : IMediaService
    {
        private readonly IStorageProvider _storageProvider;
        public MediaService(string dirName)
        {
            _storageProvider = new FileSystemStorageProvider(dirName);
        }

        /// <summary>
        /// Retrieves the public path based on the relative path within the media directory.
        /// </summary>
        /// <example>
        /// "/Media/Default/InnerDirectory/Test.txt" based on the input "InnerDirectory/Test.txt"
        /// </example>
        /// <param name="relativePath">The relative path within the media directory.</param>
        /// <returns>The public path relative to the application url.</returns>
        public string GetPublicUrl(string relativePath)
        {
            Argument.ThrowIfNullOrEmpty(relativePath, "relativePath");

            return _storageProvider.GetPublicUrl(relativePath);
        }

        /// <summary>
        /// Returns the public URL for a media file.
        /// </summary>
        /// <param name="mediaPath">The relative path of the media folder containing the media.</param>
        /// <param name="fileName">The media file name.</param>
        /// <returns>The public URL for the media.</returns>
        public string GetMediaPublicUrl(string mediaPath, string fileName)
        {
            return GetPublicUrl(Path.Combine(mediaPath, fileName));
        }

        /// <summary>
        /// Retrieves the media folders within a given relative path.
        /// </summary>
        /// <param name="relativePath">The path where to retrieve the media folder from. null means root.</param>
        /// <returns>The media folder in the given path.</returns>
        public IEnumerable<MediaFolder> GetMediaFolders(string relativePath)
        {
            return _storageProvider
                .ListFolders(relativePath)
                .Select(BuildMediaFolder)
                .Where(f => !f.Name.Equals("RecipeJournal", StringComparison.OrdinalIgnoreCase))
                .Where(f => !f.Name.StartsWith("_"))
                .ToList();
        }

        private static MediaFolder BuildMediaFolder(IStorageFolder folder)
        {
            return new MediaFolder
            {
                Name = folder.GetName(),
                Size = folder.GetSize(),
                LastUpdated = folder.GetLastUpdated(),
                MediaPath = folder.GetPath()
            };
        }

        /// <summary>
        /// Retrieves the media files within a given relative path.
        /// </summary>
        /// <param name="relativePath">The path where to retrieve the media files from. null means root.</param>
        /// <returns>The media files in the given path.</returns>
        public IEnumerable<MediaFile> GetMediaFiles(string relativePath)
        {
            return _storageProvider.ListFiles(relativePath).Select(file =>
                BuildMediaFile(relativePath, file)).ToList();
        }

        private MediaFile BuildMediaFile(string relativePath, IStorageFile file)
        {
            return new MediaFile
            {
                Name = file.GetName(),
                Size = file.GetSize(),
                LastUpdated = file.GetLastUpdated(),
                Type = file.GetFileType(),
                FolderName = relativePath,
                MediaPath = GetMediaPublicUrl(relativePath, file.GetName())
            };
        }

        /// <summary>
        /// Creates a media folder.
        /// </summary>
        /// <param name="relativePath">The path where to create the new folder. null means root.</param>
        /// <param name="folderName">The name of the folder to be created.</param>
        public void CreateFolder(string relativePath, string folderName)
        {
            Argument.ThrowIfNullOrEmpty(folderName, "folderName");

            _storageProvider.CreateFolder(relativePath == null ? folderName : _storageProvider.Combine(relativePath, folderName));
        }

        /// <summary>
        /// Deletes a media folder.
        /// </summary>
        /// <param name="folderPath">The path to the folder to be deleted.</param>
        public void DeleteFolder(string folderPath)
        {
            Argument.ThrowIfNullOrEmpty(folderPath, "folderPath");

            _storageProvider.DeleteFolder(folderPath);
        }

        /// <summary>
        /// Renames a media folder.
        /// </summary>
        /// <param name="folderPath">The path to the folder to be renamed.</param>
        /// <param name="newFolderName">The new folder name.</param>
        public void RenameFolder(string folderPath, string newFolderName)
        {
            Argument.ThrowIfNullOrEmpty(folderPath, "folderPath");
            Argument.ThrowIfNullOrEmpty(newFolderName, "newFolderName");

            _storageProvider.RenameFolder(folderPath, _storageProvider.Combine(Path.GetDirectoryName(folderPath), newFolderName));
        }

        /// <summary>
        /// Deletes a media file.
        /// </summary>
        /// <param name="folderPath">The folder path.</param>
        /// <param name="fileName">The file name.</param>
        public void DeleteFile(string folderPath, string fileName)
        {
            Argument.ThrowIfNullOrEmpty(folderPath, "folderPath");
            Argument.ThrowIfNullOrEmpty(fileName, "fileName");

            _storageProvider.DeleteFile(_storageProvider.Combine(folderPath, fileName));
        }

        /// <summary>
        /// Renames a media file.
        /// </summary>
        /// <param name="folderPath">The path to the file's parent folder.</param>
        /// <param name="currentFileName">The current file name.</param>
        /// <param name="newFileName">The new file name.</param>
        public void RenameFile(string folderPath, string currentFileName, string newFileName)
        {
            Argument.ThrowIfNullOrEmpty(folderPath, "folderPath");
            Argument.ThrowIfNullOrEmpty(currentFileName, "currentFileName");
            Argument.ThrowIfNullOrEmpty(newFileName, "newFileName");

            _storageProvider.RenameFile(_storageProvider.Combine(folderPath, currentFileName), _storageProvider.Combine(folderPath, newFileName));
        }

        /// <summary>
        /// Moves a media file.
        /// </summary>
        /// <param name="currentPath">The path to the file's parent folder.</param>
        /// <param name="filename">The file name.</param>
        /// <param name="newPath">The path where the file will be moved to.</param>
        /// <param name="newFilename">The new file name.</param>
        public void MoveFile(string currentPath, string filename, string newPath, string newFilename)
        {
            Argument.ThrowIfNullOrEmpty(currentPath, "currentPath");
            Argument.ThrowIfNullOrEmpty(newPath, "newPath");
            Argument.ThrowIfNullOrEmpty(filename, "filename");
            Argument.ThrowIfNullOrEmpty(newFilename, "newFilename");

            _storageProvider.RenameFile(_storageProvider.Combine(currentPath, filename), _storageProvider.Combine(newPath, newFilename));
        }

        /// <summary>
        /// Uploads a media file based on a posted file.
        /// </summary>
        /// <param name="folderPath">The path to the folder where to upload the file.</param>
        /// <param name="postedFile">The file to upload.</param>
        /// <returns>The path to the uploaded file.</returns>
        public string UploadMediaFile(string folderPath, HttpPostedFileBase postedFile)
        {
            Argument.ThrowIfNullOrEmpty(folderPath, "folderPath");
            Argument.ThrowIfNull(postedFile, "postedFile");

            return UploadMediaFile(folderPath, Path.GetFileName(postedFile.FileName), postedFile.InputStream);
        }

        /// <summary>
        /// Uploads a media file based on an array of bytes.
        /// </summary>
        /// <param name="folderPath">The path to the folder where to upload the file.</param>
        /// <param name="fileName">The file name.</param>
        /// <param name="bytes">The array of bytes with the file's contents.</param>
        /// <returns>The path to the uploaded file.</returns>
        public string UploadMediaFile(string folderPath, string fileName, byte[] bytes)
        {
            Argument.ThrowIfNullOrEmpty(folderPath, "folderPath");
            Argument.ThrowIfNullOrEmpty(fileName, "fileName");
            Argument.ThrowIfNull(bytes, "bytes");

            return UploadMediaFile(folderPath, fileName, new MemoryStream(bytes));
        }

        /// <summary>
        /// Uploads a media file based on a stream.
        /// </summary>
        /// <param name="folderPath">The folder path to where to upload the file.</param>
        /// <param name="fileName">The file name.</param>
        /// <param name="inputStream">The stream with the file's contents.</param>
        /// <returns>The path to the uploaded file.</returns>
        public string UploadMediaFile(string folderPath, string fileName, Stream inputStream)
        {
            Argument.ThrowIfNullOrEmpty(folderPath, "folderPath");
            Argument.ThrowIfNullOrEmpty(fileName, "fileName");
            Argument.ThrowIfNull(inputStream, "inputStream");

            string filePath = _storageProvider.Combine(folderPath, fileName);
            _storageProvider.SaveStream(filePath, inputStream);

            return _storageProvider.GetPublicUrl(filePath);
        }

        public string GetUniqueFilename(string folderPath, string filename)
        {
            // compute a unique filename
            var uniqueFilename = filename;
            var index = 1;
            while (_storageProvider.FileExists(_storageProvider.Combine(folderPath, uniqueFilename)))
            {
                uniqueFilename = Path.GetFileNameWithoutExtension(filename) + "-" + index++ + Path.GetExtension(filename);
            }

            return uniqueFilename;
        }
    }
}