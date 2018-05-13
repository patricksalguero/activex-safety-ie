using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.IO;
using tmicliente;
using System.Data;
using System.Data.SqlClient;

namespace transactor
{
    [ProgId("tmicliente.TMICliente")]
    [ClassInterface(ClassInterfaceType.None)]
    [Guid("18532365-74B7-4430-92BD-CCF823ECE327")]
    [ComDefaultInterface(typeof(ITMICliente))]
    [ComVisible(true)]
    public class TMICliente : IObjectSafety, ITMICliente
    {
        #region [IObjectSafety implementation]
        private ObjectSafetyOptions m_options =
            ObjectSafetyOptions.INTERFACESAFE_FOR_UNTRUSTED_CALLER |
            ObjectSafetyOptions.INTERFACESAFE_FOR_UNTRUSTED_DATA;

        public long GetInterfaceSafetyOptions(ref Guid iid, out int pdwSupportedOptions, out int pdwEnabledOptions)
        {
            pdwSupportedOptions = (int)m_options;
            pdwEnabledOptions = (int)m_options;
            return 0;
        }

        public long SetInterfaceSafetyOptions(ref Guid iid, int dwOptionSetMask, int dwEnabledOptions)
        {
            return 0;
        }
        #endregion

        #region [ITMICliente implementation]
        public string mensaje()
        {
            return "Mensaje";
        }

        public int ShowDialog(string msg)
	    {
            return 0;
        }

        public int CrearArchivo(string rutacompleta, string dataArchivo)
        {
            string rutaArchivo = @rutacompleta;
            int resultado = 0;
            try
            {

                // Delete the file if it exists.
                if (File.Exists(rutaArchivo))
                {
                    // Note that no lock is put on the
                    // file and the possibility exists
                    // that another process could do
                    // something with it between
                    // the calls to Exists and Delete.
                    File.Delete(rutaArchivo);
                }

                // Create the file.
                using (FileStream fs = File.Create(rutaArchivo))
                {
                    Byte[] info = new UTF8Encoding(true).GetBytes(dataArchivo.Trim());
                    // Add some information to the file.
                    fs.Write(info, 0, info.Length);
                    resultado = 1;
                }

            }

            catch (Exception ex)
            {
                resultado = 0;
            }

            return resultado;
            
        }
        #endregion




        public int MostrarDialogo(string data)
        {
            var dialogo = new FrmDialogo();
            dialogo.Show();
            return 1;
        }

        #region [Proceso de Desencriptacion]
        public string RetornarNUDOCIDE(string log)
        {
            DataSet ds = new DataSet();
            SqlConnection c = new SqlConnection("Data Source=W7ITDATA01\\BDT;Initial Catalog=tm41tjtot;User ID=sa;password=transactor");
            c.Open();
            SqlCommand command = new SqlCommand("SELECT TxnDetail ,HubResp FROM  MainTxnJournal WHERE [NroOperacionHost]='" + log + "' ", c);
            SqlDataReader sdr = command.ExecuteReader();

            sdr.Read();

            byte[] result = (byte[])sdr["TxnDetail"];

            if (result != null || result.Length <= 0)
            {
                string result2 = Util.fConvertirString(result);

                string[] arr = result2.Split('.');

                string result3 = string.Empty;

                for (var i = 0; i < arr.Length; i++)
                {

                    if (arr[i].Trim().StartsWith("NUDOCIDE"))
                    {
                        result3 = arr[i].Trim();
                        break;
                    }
                }
                return result3;
            }
            return "";
        }
        #endregion
    }
}

