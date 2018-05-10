using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace transactor
{
    [Guid("E86A9038-368D-4e8f-B389-FDEF38935B2F"),
    InterfaceType(ComInterfaceType.InterfaceIsDual),
    ComVisible(true)]
    public interface ITMICliente
    {
        [DispId(1)]
        string mensaje();

        [DispId(2)]
        int ShowDialog(string msg);

        [DispId(3)]
        int CrearArchivo(string nombrearchivo, string data);
    }
}
