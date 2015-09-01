using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Eagle.Domain.EF;
using Eagle.Domain.EF.DataContext;
using Eagle.Infrastructrue.Aop.Attribute;
using Eagle.Infrastructrue.Utility;
using Eagle.Model;
using Eagle.ViewModel;

namespace Eagle.Server.Services
{
    [Injection(typeof(IJournalServices))]
    public class JournalServices : ApplicationServices, IJournalServices
    {
        public List<ShowJournal> GetJournals(int pageNum)
        {
            var showJournals = new List<ShowJournal>();
            using (var content = new MonitorContext())
            {
                var journals = content.Journals.AsNoTracking().OrderByDescending(x => x.CreateTime).Pageing(pageNum, PageSize, ref _pageCount).ToList();

                foreach (var journal in journals)
                {
                    showJournals.Add(ShowJournal.CreateShowJournal(journal));
                }
            }
            return showJournals;
        }

        public UpdateJournal Get(Guid journalId)
        {
            var updateJournal = new UpdateJournal();
            using (var monitorContext = new MonitorContext())
            {
                var journal = monitorContext.Journals.FirstOrDefault(x => x.ID == journalId);
                if (journal.Null())
                {
                    Message = "未找到要修改的日志";
                    return updateJournal;
                }
                updateJournal.ID = journal.ID;
                updateJournal.Path = journal.Path;
                updateJournal.ProjectName = journal.ProjectName;
                Flag = true;
                return updateJournal;
            }

        }

        private void Edit(UpdateJournal updateJournal)
        {
            using (var monitorContext = new MonitorContext())
            {
                var journal = monitorContext.Journals.FirstOrDefault(x => x.ID == updateJournal.ID);
                if (journal.Null())
                {
                    Message = "请选择要修改的日志";
                    return;
                }
                journal.ProjectName = updateJournal.ProjectName;
                journal.Path = updateJournal.Path;
                monitorContext.ModifiedModel(journal);
                monitorContext.SaveChanges();
            }
            Flag = true;
        }

        private void Add(UpdateJournal updateJournal)
        {
            var journal = new Journal();
            journal.ID = Guid.NewGuid();
            journal.ProjectName = updateJournal.ProjectName;
            journal.Path = updateJournal.Path;
            journal.CreateTime = DateTime.Now;
            using (var monitorContext = new MonitorContext())
            {
                monitorContext.Journals.Add(journal);
                monitorContext.SaveChanges();
            }
            Flag = true;
        }

        public void Update(UpdateJournal updateJournal)
        {
            if (updateJournal.ID == Guid.Empty)
            {
                Add(updateJournal);
            }
            else
            {
                Edit(updateJournal);
            }
        }

        public void Delete(List<Guid> journalList)
        {
            using (var defalutContent = new MonitorContext())
            {
                var journals = defalutContent.Journals.Where(x => journalList.Contains(x.ID));
                if (!journals.Any())
                {
                    Message = "请选择要删除的数据";
                    return;
                }
                defalutContent.Journals.RemoveRange(journals);
                defalutContent.SaveChanges();
                Flag = true;
            }
        }

        public List<string> GetFiles(Guid journalId, string path)
        {
            string basePath;
            var fileList = new List<string>();
            using (var monitorContext = new MonitorContext())
            {
                var journal = monitorContext.Journals.FirstOrDefault(x => x.ID == journalId);
                if (journal.Null())
                {
                    Message = "查找日志路径不正确";
                    return new List<string>();
                }
                basePath = journal.Path;
            }
            var fullPath = basePath;
            if (!string.IsNullOrEmpty(path))
            {
                fullPath = string.Format(@"{0}\{1}", basePath, path);
            }
            string[] dirs = Directory.GetDirectories(fullPath);//得到子目录
            IEnumerator iter = dirs.GetEnumerator();
            while (iter.MoveNext())
            {
                string str = ((string)(iter.Current));
                var index = str.LastIndexOf(@"\", StringComparison.Ordinal);
                str = str.Remove(0, index + 1);
                fileList.Add(str);
            }
            string[] files = Directory.GetFiles(fullPath, "*.txt");
            if (files.Length > 0)
            {
                foreach (var file in files)
                {
                    var index = file.LastIndexOf(@"\", StringComparison.Ordinal);
                    var str = file.Remove(0, index + 1);
                    fileList.Add(str);
                }

            }
            Flag = true;
            return fileList;
        }
        public string GetFile(Guid journalId, string fileName)
        {
            Message = "查找日志文件不存在";
            string basePath;
            using (var monitorContext = new MonitorContext())
            {
                var journal = monitorContext.Journals.FirstOrDefault(x => x.ID == journalId);
                if (journal.Null())
                {
                    return string.Empty;
                }
                basePath = journal.Path;
            }
            var fullPath = basePath;
            if (!string.IsNullOrEmpty(fileName))
            {
                fullPath = string.Format(@"{0}\{1}", basePath, fileName);
            }
            else
            {
                return string.Empty;
            }
            StringBuilder fileString = new StringBuilder();
            try
            {
                FileStream fs = new FileStream(fullPath, FileMode.Open, FileAccess.Read);//读取文件设定
                StreamReader m_streamReader = new StreamReader(fs, System.Text.Encoding.GetEncoding("GB2312"));//设定读写的编码
                m_streamReader.BaseStream.Seek(0, SeekOrigin.Begin);
                string strLine = m_streamReader.ReadLine();
                while (strLine != null)
                {
                    if (string.IsNullOrEmpty(strLine))
                    {
                        fileString.Append("<br>");
                    }
                    else
                    {
                        fileString.Append(strLine);
                        fileString.AppendLine();
                    }
                    strLine = m_streamReader.ReadLine();
                }
                m_streamReader.Close();
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
            return fileString.ToString();
        }
    }
}