using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileRenamer.Decoder;

namespace FileRenamer.Modules
{
    public class FileItem
    {
        public string Directory { get; set; }
        public string OriginFileName { get; set; }

        public string AlterFileName { get; set; }

        public FileInfo OriginFile { get; set; }

        public bool IsRenamed { get; set; } = false;

        public string Message { get; set; }

        public bool CanRename { get; set; } = false;

        public bool HasSameNameFile { get; set; } = false;

        public string[] ReplacerStrings { get; set; }

        public FileItem(string path)
        {
            OriginFile = new FileInfo(path);
            OriginFileName = OriginFile.Name;
            Directory = OriginFile.DirectoryName;

            if (OriginFile.Attributes.ToString().IndexOf("ReadOnly", StringComparison.Ordinal) != -1)
            {
                //File.SetAttributes(file.FullName, FileAttributes.Normal);
                Message = "文件只读";
                return;
            }


        }

        public void RenameReview()
        {
            if (OriginFile.DirectoryName == null)
            {
                return;
            }
            var ext = OriginFile.Extension.ToLower();
            switch (ext)
            {
                case ".torrent":

                    Torrent torrent;
                    try
                    {
                        torrent = new Torrent(OriginFile.FullName);
                    }
                    catch
                    {
                        Message = "Torrent读取错误";
                        break;
                    }

                    if (torrent.OpenFile == false)
                    {
                        Message = "Torrent读取失败";
                        break;
                    }

                    string torrentName;
                    if (!string.IsNullOrWhiteSpace(torrent.NameUtf8)) torrentName = torrent.NameUtf8;
                    else if (!string.IsNullOrWhiteSpace(torrent.Name)) torrentName = torrent.Name;
                    else
                    {
                        Debug.WriteLine($"torrent.FileList.Count: {torrent.FileList.Count} ;\n torrent.OpenFile: {torrent.OpenFile}");
                        if (torrent.FileList.Count > 0)
                        {
                            Debug.WriteLine(torrent.FileList[0].Path);
                            var fileinfo = new FileInfo(torrent.FileList[0].Path);
                            if (!string.IsNullOrWhiteSpace(fileinfo.Name))
                            {
                                torrentName = fileinfo.Name;
                            }
                            else
                            {
                                break;
                            }
                        }
                        else
                        {
                            break;
                        }
                    }

                    var sb = new StringBuilder(torrentName);
                    foreach (var achar in Path.GetInvalidFileNameChars())
                    {
                        sb.Replace(achar.ToString(), "");
                    }
                    AlterFileName = sb + ".torrent";
                    
                    CanRename = true;
                    break;
                case ".jpg":
                case ".jpeg":
                    var jpg = new Jpg(OriginFile);
                    if (jpg.IsOpenError)
                    {
                        Message = "图片读取失败";
                        break;
                    }
                    if (jpg.ShootingDateTime == DateTime.MinValue)
                    {
                        Message = "无拍摄信息";
                        break;
                    }
                    var name = jpg.ShootingDateTime.ToString("yyyyMMdd-HHmmss");
                    AlterFileName = name + ext;
                    CanRename = true; 
                    break;
                default:
                    
                    break;
            }

            if (string.IsNullOrWhiteSpace(AlterFileName))
            {
                CanRename = false;
                return;
            }

            if (string.Equals(AlterFileName, OriginFileName, StringComparison.CurrentCultureIgnoreCase))
            {
                Message = "与原名相同，无需更名";
                CanRename = false;
            }
            if (CanRename && File.Exists(Path.Combine(Directory,AlterFileName)))
            {
                HasSameNameFile = true;
                Message = "同目录有同名文件";
                CanRename = false;
            }
        }
        
    }
}
