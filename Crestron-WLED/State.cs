using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crestron.SimplSharp;
using Newtonsoft.Json;


namespace Crestron_WLED
{
    // Crestron prefixed structs for use in SIMPL+, since no bool available
    public struct CrestronWLEDState
    {
        public ushort on;
        public ushort bri;
        public CrestronWLEDSegment[] seg;
    }

    public struct CrestronWLEDSegment
    {
        public string col;
        public ushort bri;
        public ushort fx;
    }

    public struct WLEDState
    {
        public bool on;
        public ushort bri;
        public WLEDSegment[] seg;

        public WLEDState(ref CrestronWLEDState crestronState)
        {
            on = crestronState.on > 0;
            bri = crestronState.bri;
            if (crestronState.seg.Length == 0)
            {
                seg = new WLEDSegment[1];
            }
            else
            {
                seg = new WLEDSegment[crestronState.seg.Length];
                for (int i = 0; i < crestronState.seg.Length; i++)
                {
                    seg[i] = new WLEDSegment(ref crestronState.seg[i]);
                }
            }
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

        public WLEDSegment(ref CrestronWLEDSegment crestronSegment)
        {
            col = crestronSegment.col;
            bri = crestronSegment.bri;
            fx = crestronSegment.fx;
        }
    }

    public class WLEDStructHelper
    {

        public int InitStateStruct(ref CrestronWLEDState state, ushort numberOfSegments)
        {

            state.seg = new CrestronWLEDSegment[numberOfSegments];
            return 0;
        }

    }
}
