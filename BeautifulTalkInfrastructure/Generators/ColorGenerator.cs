using BeautifulTalkInfrastructure.Logger;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace BeautifulTalkInfrastructure.Generators
{
    public class ColorGenerator
    {
        private static readonly object locker = new object();
        private static ColorGenerator m_ColorGenerator = null;
        private Random m_RandomPicker;
        private BrushConverter m_BrushConverter;
        private List<string> m_Window8Palette;

        public static ColorGenerator Instance
        {
            get
            {
                lock (locker)
                {
                    if (null == m_ColorGenerator) m_ColorGenerator = new ColorGenerator();
                    return m_ColorGenerator;
                }
            }
        }
        
        private ColorGenerator()
        { 
            this.m_RandomPicker = new Random(DateTime.Now.Second);
            this.m_BrushConverter = new BrushConverter();

            this.InitializeWindow8Palette();
        }

        private void InitializeWindow8Palette()
        {
            this.m_Window8Palette = new List<string>();
            this.m_Window8Palette.AddRange(new string[] 
            { 
                "#F3B200","#77B900","#2572EB","#AD103C","#632F00","#B01E00","#C1004F","#7200AC","#4617B4","#006AC1","#008287",
                "#199900","#00C13F","#FF981D","#FF2E12","#FF1D77","#AA40FF","#1FAEFF","#56C5FF","#00D8CC","#91D100","#E1B700",
                "#FF76BC","#00A3A3","#FE7C22"
            });
        }

        public Brush GetRandomBrush()
        {
            try
            {
                int nRandomNumber = this.m_RandomPicker.Next(0, this.m_Window8Palette.Count - 1);
                return (Brush)this.m_BrushConverter.ConvertFrom(this.m_Window8Palette[nRandomNumber]);
            }
            catch (Exception unExpectedException)
            {
                GlobalLogger.Log(unExpectedException.Message);
            }

            return null;
        }
    }
}
