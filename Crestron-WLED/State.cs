using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crestron.SimplSharp;
using Newtonsoft.Json;


namespace Crestron_WLED
{
    public struct WLEDState
    {
        public bool on;
        public WLEDSegment[] seg;

        public WLEDState(bool _on, string _color, ushort _bri, ushort _fx)
        {
            on = _on;
            seg = new WLEDSegment[1];
            seg[0] = new WLEDSegment(_color, _bri, _fx);
        }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }
    }

    public struct WLEDSegment
    {
        public string col;
        public ushort bri;
        public ushort fx;

        public WLEDSegment(string _color, ushort _bri, ushort _fx)
        {
            col = _color;
            bri = _bri;
            fx = _fx;
        }
    }

    public class WLEDStructHelper
    {

        public WLEDState CreateStateStruct(bool _on, string _color, ushort _bri, ushort _fx)
        {
            return new WLEDState(_on, _color, _bri, _fx);
        }

        public int InitStateStruct(ref WLEDState state)
        {
            return 0;
        }
    }
}
