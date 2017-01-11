using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace ProjectMato.iOS.Helper
{
    public class ColorHelper
    {

        private List<Color> _colorGroup1;

        public List<Color> ColorGroup1
        {
            get
            {
                if (_colorGroup1 == null)
                {
                    _colorGroup1 = new List<Color>();
                    InitColorGroup1Value();
                }
                return _colorGroup1;
            }
        }

        private List<Color> _colorGroup2;

        public List<Color> ColorGroup2
        {
            get
            {
                if (_colorGroup2 == null)
                {
                    _colorGroup2 = new List<Color>();
                    InitColorGroup2Value();
                }
                return _colorGroup2;
            }
        }

     

        private void InitColorGroup2Value()
        {
            _colorGroup1.Add(Color.FromHex("543A24"));
            _colorGroup1.Add(Color.FromHex("61292B"));
            _colorGroup1.Add(Color.FromHex("662C58"));
            _colorGroup1.Add(Color.FromHex("4C2C66"));
            _colorGroup1.Add(Color.FromHex("423173"));
            _colorGroup1.Add(Color.FromHex("2C4566"));
            _colorGroup1.Add(Color.FromHex("306772"));
            _colorGroup1.Add(Color.FromHex("2D652B"));
            _colorGroup1.Add(Color.FromHex("3A9548"));
            _colorGroup1.Add(Color.FromHex("C27D4F"));
            _colorGroup1.Add(Color.FromHex("AA4344"));
            _colorGroup1.Add(Color.FromHex("AA4379"));
            _colorGroup1.Add(Color.FromHex("7F6E94"));
            _colorGroup1.Add(Color.FromHex("6E7E94"));
            _colorGroup1.Add(Color.FromHex("6BA5E7"));
            _colorGroup1.Add(Color.FromHex("439D9A"));
            _colorGroup1.Add(Color.FromHex("94BD4A"));
            _colorGroup1.Add(Color.FromHex("CEA539"));
            _colorGroup1.Add(Color.FromHex("E773BD"));
        }



        private void InitColorGroup1Value()
        {
            _colorGroup1.Add(Color.FromHex("F3B200"));
            _colorGroup1.Add(Color.FromHex("77B900"));
            _colorGroup1.Add(Color.FromHex("2572EB"));
            _colorGroup1.Add(Color.FromHex("AD103C"));
            _colorGroup1.Add(Color.FromHex("632F00"));
            _colorGroup1.Add(Color.FromHex("B01E00"));
            _colorGroup1.Add(Color.FromHex("C1004F"));
            _colorGroup1.Add(Color.FromHex("7200AC"));
            _colorGroup1.Add(Color.FromHex("4617B4"));
            _colorGroup1.Add(Color.FromHex("006AC1"));
            _colorGroup1.Add(Color.FromHex("8287"));
            _colorGroup1.Add(Color.FromHex("199900"));
            _colorGroup1.Add(Color.FromHex("00C13F"));
            _colorGroup1.Add(Color.FromHex("FF981D"));
            _colorGroup1.Add(Color.FromHex("FF2E12"));
            _colorGroup1.Add(Color.FromHex("FF1D77"));
            _colorGroup1.Add(Color.FromHex("AA40FF"));
            _colorGroup1.Add(Color.FromHex("1FAEFF"));
            _colorGroup1.Add(Color.FromHex("56C5FF"));
            _colorGroup1.Add(Color.FromHex("00D8CC"));
            _colorGroup1.Add(Color.FromHex("91D100"));
            _colorGroup1.Add(Color.FromHex("E1B700"));
            _colorGroup1.Add(Color.FromHex("FF76BC"));
            _colorGroup1.Add(Color.FromHex("00A3A3"));
            _colorGroup1.Add(Color.FromHex("FE7C22"));
        }

        public Color GetRandomColor()
        {
            var cRandom = new Random();
            int s32Color = cRandom.Next(ColorGroup1.Count - 1);
            var cColor = ColorGroup1[s32Color];
            return cColor;

        }
        public Color GetLightRandomColor()
        {
            var cRandom = new Random();
            int s32Color = cRandom.Next(ColorGroup2.Count - 1);
            var cColor = ColorGroup2[s32Color];
            return cColor;

        }
    }
}
