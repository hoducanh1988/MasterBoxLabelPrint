using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MasterBoxLabelPrint_Ver1.MyFunction.Ulti {
    public class GetLatestFileInDirectory {

        string[] file_extensions = null;
        string dir = null;

        public GetLatestFileInDirectory(string _dir, string[] _file_extensions) {
            this.file_extensions = _file_extensions;
            this.dir = _dir;
        }

        FileInfo GetFile(string ext) {
            try {
                return new DirectoryInfo(this.dir).GetFiles("*." + ext).OrderByDescending(f => f.LastWriteTime).First();
            }
            catch {
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string GetFileName() {
            try {
                if (this.file_extensions.Length == 0) return null;
                if (this.file_extensions.Length == 1) {
                    return GetFile(this.file_extensions[0]).Name;
                }
                else {
                    FileInfo f = null;
                    DateTime d = DateTime.MinValue;

                    foreach (var ext in this.file_extensions) {
                        var file = GetFile(ext);
                        if (file != null) {
                            if (DateTime.Compare(d, file.LastWriteTime) < 0) {
                                f = file;
                                d = file.LastWriteTime;
                            }
                        }
                        
                    }
                    if (f != null) return f.Name;
                    else return null;
                }
                
            } catch {
                return null;
            }
        }

    }
}
