using log4net;
using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace QuartzWeb
{
    public class QuartzJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            string path = SiteConfig.GetQuarzConfigValue("Path");
            string days = SiteConfig.GetQuarzConfigValue("Days");
            int.TryParse(days, out int day);
            DeleteFile(path, day);
            await Task.CompletedTask;
        }
        /// <summary>
        /// 删除文件夹strDir中nDays天以前的文件
        /// </summary>
        /// <param name="dir"></param>
        /// <param name="days"></param>
        public void DeleteFile(string dir, int days)
        {

            try
            {
                if (!Directory.Exists(dir) || days < 1) return;

                var now = DateTime.Now;

                //获取指定路径下所有文件夹
                string[] folderPaths = Directory.GetDirectories(dir);

                foreach (string folderPath in folderPaths)
                {
                    var t = Directory.GetCreationTime(folderPath);

                    var elapsedTicks = now.Ticks - t.Ticks;
                    var elapsedSpan = new TimeSpan(elapsedTicks);

                    if (elapsedSpan.TotalDays > days)
                    {
                        Directory.Delete(folderPath, true);
                        LogHelper.Info("This \"" + folderPath + "\" folder has been deleted");
                    }
                }

                //获取指定路径下所有文件
                string[] filePaths = Directory.GetFiles(dir);

                foreach (string filePath in filePaths)
                {

                    var t = File.GetCreationTime(filePath);

                    var elapsedTicks = now.Ticks - t.Ticks;
                    var elapsedSpan = new TimeSpan(elapsedTicks);

                    if (elapsedSpan.TotalDays > days)
                    {
                        File.Delete(filePath);
                        LogHelper.Info("This \"" + filePath + "\" file has been deleted");
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
