//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Robot_P16 {
    using Gadgeteer;
    using GTM = Gadgeteer.Modules;
    using Microsoft.SPOT;
    
    public partial class Program : Gadgeteer.Program {
        
        /// <summary>This property provides access to the Mainboard API. This is normally not necessary for an end user program.</summary>
        public new static Gadgeteer.Mainboard Mainboard {
            get {
                return ((Gadgeteer.Mainboard)(Gadgeteer.Program.Mainboard));
            }
            set {
                Debug.Print("Setting mainboard to : " + value.MainboardVersion);
                Gadgeteer.Program.Mainboard = value;
            }
        }
        
        /// <summary>This method runs automatically when the device is powered, and calls ProgramStarted.</summary>
        public static void Main() {
            // Important to initialize the Mainboard first
            //throw new System.Exception("No mainboard is defined. Please add a mainboard in the Gadgeteer Designer.");
            Program.Mainboard = new GHIElectronics.Gadgeteer.FEZSpider();
            Program p = new Program();
            p.InitializeModules();
            p.ProgramStarted();
            // Starts Dispatcher
            p.Run();
        }
        
        private void InitializeModules() {
        }
    }
}
