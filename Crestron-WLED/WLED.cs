using System;
using System.Text;
using Crestron.SimplSharp;                          				// For Basic SIMPL# Classes
using Crestron.SimplSharp.Net.Http;

namespace Crestron_WLED
{

    public delegate void DelegateTemplate(ushort Value);  // Create our delegate template.  you do not use this in S+
    // The delegate definition is it's own class and can exist outside of your library class that you will use.
    // NOTE: the S+ API will show this as if it is a part of your class.  This will trip up a lot of programmers.
    // Do not ty and use this in your S+  it will throw ambigous errors that do not point at you using the wrong delegate name.

    public class WLED
    {
        // Connection information
        public string Host { private get; set; }
        public ushort Port { private get; set; }
        
        public bool On { private get; set; }
        public UInt16 Brightness { private get; set; }
        public string Color { private get; set; }

        public DelegateTemplate WLEDDelegate { get; set; }


        /// <summary>
        /// SIMPL+ can only execute the default constructor. If you have variables that require initialization, please
        /// use an Initialize method
        /// </summary>
        public WLED()
        {
        }

        /// <summary>
        /// Retreive current WLED state
        /// </summary>
        public void GetState()
        {
        }

        /// <summary>
        /// Set WLED state
        /// </summary>
        public void SetState(ushort _on, string _color, ushort _bri, ushort _fx)
        {
            WLEDState state = new WLEDState(_on == 1 ? true : false, _color, _bri, _fx);
            HttpClientRequest req = new HttpClientRequest();
            req.Header.ContentType = "application/json";
            req.RequestType = RequestType.Post;
            req.Url.Parse("http://" + Host + ":" + Port + "/json");
            req.ContentString = state.ToJson();
 
            HttpClient client = new HttpClient();

            try {
                var response = client.Dispatch(req);
                WLEDDelegate(1);
            }
            catch (Exception ex)
            {
                ErrorLog.Error("Failed to set LED state: {0}", ex.Message);
                WLEDDelegate(0);
            }
            finally
            {
                client.Dispose();
            }
        }

    }
}