//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Robot_P16
{
    
    internal partial class Resources
    {
        private static System.Resources.ResourceManager manager;
        internal static System.Resources.ResourceManager ResourceManager
        {
            get
            {
                if ((Resources.manager == null))
                {
                    Resources.manager = new System.Resources.ResourceManager("Robot_P16.Resources", typeof(Resources).Assembly);
                }
                return Resources.manager;
            }
        }
        internal static string GetString(Resources.StringResources id)
        {
            return ((string)(Microsoft.SPOT.ResourceUtility.GetObject(ResourceManager, id)));
        }
        internal static byte[] GetBytes(Resources.BinaryResources id)
        {
            return ((byte[])(Microsoft.SPOT.ResourceUtility.GetObject(ResourceManager, id)));
        }
        [System.SerializableAttribute()]
        internal enum StringResources : short
        {
            fenetreChoixType = -25913,
            fenetreInformation = -18393,
            fenetreChoixCouleur = -15503,
            fenetreErreur = -7980,
            fenetreModeCompetition = 26815,
            fenetreChoixMode = 26824,
        }
        [System.SerializableAttribute()]
        internal enum BinaryResources : short
        {
            logoTest = -14936,
            logoCompetition = 1895,
            logoHomologation = 17594,
            exit = 19374,
        }
    }
}
