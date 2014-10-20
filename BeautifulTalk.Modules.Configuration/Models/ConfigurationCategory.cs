using BeautifulTalkInfrastructure.Generators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace BeautifulTalk.Modules.Configuration.Models
{
    public class ConfigurationCategory
    {
        public ImageSource ThumbnailPath { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Detail { get; set; }
        public Brush CategoryBackground { get; set; }

        public ConfigurationCategory(ImageSource thumbnailPath, string strTitle, string strDescription, string strDetail)
        {
            this.ThumbnailPath = thumbnailPath;
            this.Title = strTitle;
            this.Description = strDescription;
            this.Detail = strDetail;
            this.CategoryBackground = (Brush)new BrushConverter().ConvertFrom("#FF454545");
        }
    }
}
