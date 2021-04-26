﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace TexturePlugin
{
    public class PInvoke
    {
        [DllImport("textoolwrap.dll")]
        public static extern uint DecodeByCrunch(IntPtr data, IntPtr buf, int mode, uint width, uint height);

        [DllImport("textoolwrap.dll")]
        public static extern uint DecodeByPVRTexLib(IntPtr data, IntPtr buf, int mode, uint width, uint height);

        [DllImport("textoolwrap.dll")]
        public static extern uint EncodeByCrunch(IntPtr data, IntPtr buf, int mode, int level, uint width, uint height);

        [DllImport("textoolwrap.dll")]
        public static extern uint EncodeByPVRTexLib(IntPtr data, IntPtr buf, int mode, int level, uint width, uint height);
    }
}
