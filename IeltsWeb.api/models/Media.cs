using System;
using System.Collections.Generic;

namespace IeltsWeb.api.models
{
    public class Media
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string Type { get; set; } // Image/Audio
        public DateTime UploadedAt { get; set; }
        public int UploadedBy { get; set; }

        public User Uploader { get; set; }
        public ICollection<QuestionMedia> QuestionMedias { get; set; }

        public Media()
        {
            Url = string.Empty;
            Type = string.Empty;
            Uploader = null!;
            QuestionMedias = new List<QuestionMedia>();
        }
    }
}
